using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using Volo.Abp;
using Volo.Abp.AspNetCore.MultiTenancy;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Auditing;
using Volo.Abp.Autofac;
using Volo.Abp.Caching.StackExchangeRedis;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.MySQL;
using Volo.Abp.EventBus;
using Volo.Abp.EventBus.RabbitMq;
using Volo.Abp.Json;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.Security.Claims;
using Volo.Abp.Threading;
using XFree.Simple.Application.Contracts.Options;
using XFree.Simple.Application.SystemManage;
using XFree.Simple.Domain.Shared.Common;
using XFree.Simple.EntityFrameworkCore;
using XFree.Simple.EntityFrameworkCore.SystemManagement.EntityFrameworkCore;
using XFree.Simple.HttpApi.SystemManage;
using XFree.SimpleService.Host.Common.Filters;
using XFree.SimpleService.Host.Common.Serializer;
using XFree.SimpleService.Host.Permission;
namespace XFree.SimpleService.Host
{
    /// <summary>
    /// 
    /// </summary>
    [DependsOn(
       typeof(AbpAutofacModule),
       typeof(AbpAspNetCoreMvcModule),
       typeof(AbpEventBusModule),
       typeof(AbpEntityFrameworkCoreMySQLModule),
       typeof(SystemManagementApplicationModule),
       typeof(SystemManagementHttpApiModule),
       typeof(ApplicationEntityFrameworkCoreModule),
       typeof(AbpCachingStackExchangeRedisModule),
       typeof(AbpAspNetCoreMultiTenancyModule)
       )]
    public class SystemServiceHostModule : AbpModule
    {
        /// <summary>
        /// 
        /// </summary>
        private const string DefaultCorsPolicyName = "Default";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();
            var authenticationJwtSection = configuration.GetSection("AuthenticationJwt");
            var authenticationJwtOptions = authenticationJwtSection.Get<AuthenticationJwtOptions>();

            context.Services
                .AddAuthentication(authenticationJwtOptions.BearerAuthenticationScheme)
                .AddJwtBearerR(authenticationJwtOptions.BearerPublicKey,
                    authenticationJwtOptions.BearerAuthenticationScheme,
                    authenticationJwtOptions.SubSystemAuthenticationScheme)
                .AddJwtSystemR(authenticationJwtOptions.SubSystemPublicKey,
                    authenticationJwtOptions.SubSystemAuthenticationScheme,
                    authenticationJwtOptions.SubSystemAuthenticationHeader);

            Configure<AuthenticationJwtOptions>(authenticationJwtSection);

            Configure<StringEncryptionOptions>(configuration.GetSection(nameof(StringEncryptionOptions)));

            // swagger
            context.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "System Service API", Version = "v1" });
                options.DocInclusionPredicate((docName, description) => true);
                options.CustomSchemaIds(type => type.FullName);
                options.AddSecurityDefinition(authenticationJwtOptions.BearerAuthenticationScheme, new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme.",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Scheme = "bearer",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT"
                });
                var securityRequirement = new OpenApiSecurityRequirement
                    {
                        {
                                new OpenApiSecurityScheme
                                {
                                    Reference = new OpenApiReference
                                    {
                                        Type = ReferenceType.SecurityScheme,
                                        Id = authenticationJwtOptions.BearerAuthenticationScheme
                                    }
                                },
                                Array.Empty<string>()
                        }
                    };
                options.AddSecurityRequirement(securityRequirement);
                var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);
                options.IncludeXmlComments(Path.Combine(basePath, "XFree.Simple.Application.Contracts.xml"));
                options.IncludeXmlComments(Path.Combine(basePath, "XFree.Simple.HttpApi.xml"));
                options.IncludeXmlComments(Path.Combine(basePath, "XFreeSimpleService.Host.xml")); 

            });

            // 返回异常返回结果处理
            context.Services.AddMvc(options =>
            {
                options.Filters.Add(typeof(ResultExceptionFilter));
            }).AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new DateTimeJsonConverter()));

            // 多语言支持
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Languages.Add(new LanguageInfo("zh-Hans", "zh-Hans", "简体中文"));
                options.Languages.Add(new LanguageInfo("en", "en", "English"));
            });

            Configure<AbpJsonOptions>(options =>
                options.DefaultDateTimeFormat = Const.DateTimeConfig.DEFAULT_FORMAT
            );

            // 数据库
            Configure<AbpDbContextOptions>(options =>
            {
                options.UseMySQL();
            });

            // 审计配置
            Configure<AbpAuditingOptions>(options =>
            {
                options.IsEnabledForGetRequests = false;
                options.ApplicationName = "TenantService";
            });

            // 跨域支持
            context.Services.AddCors(options =>
            {
                options.AddPolicy(DefaultCorsPolicyName, builder =>
                {
                    builder
                        .WithOrigins(
                            configuration["App:CorsOrigins"]
                                .Split(",", StringSplitOptions.RemoveEmptyEntries)
                                .Select(o => o.RemovePostFix("/"))
                                .ToArray()
                        )
                        .WithExposedHeaders("Content-Disposition")
                        .SetIsOriginAllowedToAllowWildcardSubdomains()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });

            // 加密存储
            var redis = ConnectionMultiplexer.Connect(configuration["Redis:Configuration"]);
            context.Services.AddDataProtection()
                .PersistKeysToStackExchangeRedis(redis, "Free-DataProtection-Keys");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();

            // CorrelationId中间件
            app.UseCorrelationId();
            // 静态文件中间件
            app.UseStaticFiles();
            // 路由中间件
            app.UseRouting();
            // 身份认证中间件
            app.UseAuthentication();
            app.UseAbpClaimsMap();
            // 跨域
            app.UseCors(DefaultCorsPolicyName);
            // 转换到abp身份信息
            app.Use(async (ctx, next) =>
            {
                var currentPrincipalAccessor = ctx.RequestServices.GetRequiredService<ICurrentPrincipalAccessor>();
                var map = new Dictionary<string, string>()
                {
                    { "role", AbpClaimTypes.Role },
                    { "email", AbpClaimTypes.Email },
                    { "loginName", AbpClaimTypes.UserName },
                    { "userId",AbpClaimTypes.UserId },
                    { "name",AbpClaimTypes.Name },
                    { "userTenantId",AbpClaimTypes.TenantId }
                };
                var mapClaims = currentPrincipalAccessor.Principal.Claims.Where(p => map.Keys.Contains(p.Type)).ToList();
                // 转换添加Abp claims
                currentPrincipalAccessor.Principal.AddIdentity(new ClaimsIdentity(mapClaims.Select(p => new Claim(map[p.Type], p.Value, p.ValueType, p.Issuer))));
                await next();
            });

            //多语言配置
            app.UseAbpRequestLocalization(op=> {
                op.SetDefaultCulture("zh-Hans");
            }); 
            // swagger
            app.UseSwagger();
            // swagger ui
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "XFreeSimple Service API");
            });
            // 审计
            app.UseAuditing();

            app.UseMultiTenancy();

            //
            app.UseConfiguredEndpoints();

            //TODO: Problem on a clustered environment
            AsyncHelper.RunSync(async () =>
            {
                using var scope = context.ServiceProvider.CreateScope();
                await scope.ServiceProvider
                    .GetRequiredService<IDataSeeder>()
                    .SeedAsync();
            });
        }

    }
}
