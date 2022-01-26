using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.EntityFrameworkCore.ValueConverters;

namespace XFree.Simple.EntityFrameworkCore
{
    /// <summary>
    /// 
    /// </summary>
    public static class DbContextModelCreatingExtensions
    {
        #region SetSnakeCaseNaming

        /// <summary>
        /// 
        /// </summary>
        private readonly static NamingStrategy NamingStrategy = new SnakeCaseNamingStrategy();

        /// <summary>
        ///  将实体属性名称映射到的数据库字段为下划线命名方式名称
        ///  实体属性名称为IsDeleted，则数据库字段名称为is_deleted
        /// </summary>
        /// <typeparam name="Entity"></typeparam>
        /// <param name="modelBuilder"></param>
        public static void SetSnakeCaseNaming<Entity>(this EntityTypeBuilder<Entity> modelBuilder) where Entity : class
        {
            var clrType = modelBuilder.Metadata.ClrType;

            Dictionary<Type, Delegate> tempPropertyFunc = new();

            void AddPropertyFuncDelegate(Type propertyType)
            {
                // 获取Property方法
                var entityTypeBuilderMethods = modelBuilder
                    .GetType()
                    .GetMethods()
                    .FirstOrDefault(w => w.Name == "Property" && w.IsGenericMethod);
                var funcType = typeof(Func<,>).MakeGenericType(clrType, propertyType);
                var expressionType = typeof(Expression<>).MakeGenericType(funcType);
                ParameterExpression parameterExpression = Expression.Parameter(expressionType, "p");
                var bodyTo = Expression.Call(Expression.Constant(modelBuilder), entityTypeBuilderMethods.MakeGenericMethod(propertyType), parameterExpression);
                var lambdaTo = Expression.Lambda(bodyTo, parameterExpression).Compile();
                tempPropertyFunc.Add(propertyType, lambdaTo);
            }

            foreach (var entityProperty in clrType.GetProperties())
            {
                var propertyType = entityProperty.PropertyType;

                if ((propertyType.IsGenericType && propertyType.GenericTypeArguments[0].IsAssignableTo(typeof(Volo.Abp.Domain.Values.ValueObject)) || propertyType.IsAssignableTo(typeof(Volo.Abp.Domain.Values.ValueObject))))
                {
                    continue;
                }

                // 实体则跳过
                if ((propertyType.IsGenericType && propertyType.GetGenericTypeDefinition().IsAssignableTo(typeof(DbSet<>))) || propertyType.IsAssignableTo(typeof(Volo.Abp.Domain.Entities.IEntity)))
                    continue;

                if (!tempPropertyFunc.ContainsKey(propertyType))
                {
                    AddPropertyFuncDelegate(propertyType);
                }

                //1.创建表达式参数（指定参数或变量的类型:p）
                ParameterExpression param = Expression.Parameter(clrType);
                //2.构建表达式体(类型包含指定的属性:p.Name)
                MemberExpression body = Expression.Property(param, entityProperty.Name);
                //3.根据参数和表达式体构造一个lambda表达式
                var lambda = Expression.Lambda(body, param);
                // 调用Property
                var propertyBuilder = tempPropertyFunc[propertyType].DynamicInvoke(lambda);
                var columnName = NamingStrategy.GetPropertyName(entityProperty.Name, false);
                // 调用HasColumnName
                HasColumnNameDelegate.Value.DynamicInvoke(new object[] { propertyBuilder, columnName });
            }

        }

        /// <summary>
        ///  设置Json对象属性
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="b"></param>
        /// <param name="propertyExpression"></param>
        /// <param name="maxLength"></param>
        public static void TryConfigureJsonValueProperties<TEntity, TProperty>(this EntityTypeBuilder<TEntity> b, Expression<Func<TEntity, TProperty>> propertyExpression, int maxLength = 1024) where TEntity : class
        {
            b.Property(propertyExpression)
            .HasConversion(new AbpJsonValueConverter<TProperty>())
            .HasMaxLength(maxLength);
        }

        /// <summary>
        /// 
        /// </summary>
        private static readonly Lazy<Delegate> HasColumnNameDelegate = new(() =>
        {
            var propertyBuilderHasColumnNameMethods = Assembly
            .GetAssembly(typeof(RelationalPropertyBuilderExtensions))
            .GetTypes()
            .Where(type => !type.IsGenericType && !type.IsNested)
            .SelectMany(type => type.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
            .Where(method => method.IsDefined(typeof(System.Runtime.CompilerServices.ExtensionAttribute), false))
            .Where(w => w.Name.Contains("HasColumnName"))
            .FirstOrDefault();
            ParameterExpression parameterExpression1 = Expression.Parameter(typeof(PropertyBuilder), "p1");
            ParameterExpression parameterExpression2 = Expression.Parameter(typeof(string), "p2");
            var bodyTo = Expression.Call(null, propertyBuilderHasColumnNameMethods,
                parameterExpression1, parameterExpression2);
            var func = Expression.Lambda(bodyTo, parameterExpression1, parameterExpression2).Compile();
            return func;
        });

        #endregion
    }
}
