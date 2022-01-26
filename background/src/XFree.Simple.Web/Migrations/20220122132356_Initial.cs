using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace XFreeSimpleService.Host.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sys_background_api",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false, comment: "数据唯一标识")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    tenant_id = table.Column<Guid>(type: "char(36)", nullable: true, comment: "租户Id", collation: "ascii_general_ci"),
                    primary_node = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "一级节点"),
                    module = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true, comment: "模块")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    parent_permission_code = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, comment: "父节点权限编码")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    permission_code = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, comment: "权限编码")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, comment: "名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    en_name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, comment: "英文名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    path = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, comment: "接口路径")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    method = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true, comment: "方法")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    sort_order = table.Column<int>(type: "int", nullable: false, comment: "排序")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sys_background_api", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sys_database_connection",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false, comment: "数据唯一标识")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    database_provider_type = table.Column<int>(type: "int", nullable: false, comment: "0: NotSpecified1: EntityFrameworkCore2: MongoDb"),
                    name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, comment: "名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    memo = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, comment: "备注")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    status = table.Column<int>(type: "int", nullable: false, comment: "状态"),
                    range_tenant_ids = table.Column<string>(type: "varchar(4000)", maxLength: 4000, nullable: true, comment: "限定商户范围,默认不限定")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    connection_string = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, comment: "数据库连接字符串")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    extra_properties = table.Column<string>(type: "longtext", nullable: true, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    concurrency_stamp = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: true, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    creation_time = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "创建时间"),
                    creator_id = table.Column<Guid>(type: "char(36)", nullable: true, comment: "创建人Id", collation: "ascii_general_ci"),
                    last_modification_time = table.Column<DateTime>(type: "datetime(6)", nullable: true, comment: "最后修改时间"),
                    last_modifier_id = table.Column<Guid>(type: "char(36)", nullable: true, comment: "最后人Id", collation: "ascii_general_ci"),
                    is_deleted = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false, comment: "Used to mark an Entity as 'Deleted'."),
                    deleter_id = table.Column<Guid>(type: "char(36)", nullable: true, comment: "Id of the deleter user.", collation: "ascii_general_ci"),
                    deletion_time = table.Column<DateTime>(type: "datetime(6)", nullable: true, comment: "Deletion time.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sys_database_connection", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sys_depart",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false, comment: "数据唯一标识")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    tenant_id = table.Column<Guid>(type: "char(36)", nullable: true, comment: "租户Id", collation: "ascii_general_ci"),
                    parent_id = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, comment: "父机构ID")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    org_name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, comment: "机构/部门名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    org_name_en = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, comment: "英文名")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    org_category = table.Column<int>(type: "int", nullable: false, comment: "机构类别 1组织机构，2岗位"),
                    org_level_type = table.Column<int>(type: "int", nullable: false, comment: "机构类型 1一级部门 2子部门"),
                    org_type = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, comment: "类型")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    org_code = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "机构编码")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    contact = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, comment: "联系方式")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    memo = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, comment: "备注")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    status = table.Column<int>(type: "int", nullable: false, comment: "状态"),
                    sort_order = table.Column<int>(type: "int", nullable: true, comment: "排序"),
                    address = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, comment: "地址")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    extra_properties = table.Column<string>(type: "longtext", nullable: true, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    concurrency_stamp = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: true, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    creation_time = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "创建时间"),
                    creator_id = table.Column<Guid>(type: "char(36)", nullable: true, comment: "创建人Id", collation: "ascii_general_ci"),
                    last_modification_time = table.Column<DateTime>(type: "datetime(6)", nullable: true, comment: "最后修改时间"),
                    last_modifier_id = table.Column<Guid>(type: "char(36)", nullable: true, comment: "最后人Id", collation: "ascii_general_ci"),
                    is_deleted = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false, comment: "Used to mark an Entity as 'Deleted'."),
                    deleter_id = table.Column<Guid>(type: "char(36)", nullable: true, comment: "Id of the deleter user.", collation: "ascii_general_ci"),
                    deletion_time = table.Column<DateTime>(type: "datetime(6)", nullable: true, comment: "Deletion time.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sys_depart", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sys_depart_role",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false, comment: "数据唯一标识")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    tenant_id = table.Column<Guid>(type: "char(36)", nullable: true, comment: "租户Id", collation: "ascii_general_ci"),
                    depart_id = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    role_id = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sys_depart_role", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sys_dict",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false, comment: "数据唯一标识")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    tenant_id = table.Column<Guid>(type: "char(36)", nullable: true, comment: "租户Id", collation: "ascii_general_ci"),
                    dict_code = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, comment: "字典编码")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    dict_en_name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, comment: "字典名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    dict_name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, comment: "字典名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, comment: "描述")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    extra_properties = table.Column<string>(type: "longtext", nullable: true, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    concurrency_stamp = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: true, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    creation_time = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "创建时间"),
                    creator_id = table.Column<Guid>(type: "char(36)", nullable: true, comment: "创建人Id", collation: "ascii_general_ci"),
                    last_modification_time = table.Column<DateTime>(type: "datetime(6)", nullable: true, comment: "最后修改时间"),
                    last_modifier_id = table.Column<Guid>(type: "char(36)", nullable: true, comment: "最后人Id", collation: "ascii_general_ci"),
                    is_deleted = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false, comment: "Used to mark an Entity as 'Deleted'."),
                    deleter_id = table.Column<Guid>(type: "char(36)", nullable: true, comment: "Id of the deleter user.", collation: "ascii_general_ci"),
                    deletion_time = table.Column<DateTime>(type: "datetime(6)", nullable: true, comment: "Deletion time.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sys_dict", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sys_dict_item",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false, comment: "数据唯一标识")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    tenant_id = table.Column<Guid>(type: "char(36)", nullable: true, comment: "租户Id", collation: "ascii_general_ci"),
                    dict_id = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false, comment: "字典id")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    item_text = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, comment: "字典项文本")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    item_en_text = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, comment: "字典项英文文本")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    item_value = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false, comment: "字典项值")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, comment: "描述")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    sort_order = table.Column<int>(type: "int", nullable: true, comment: "排序"),
                    enabled = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "状态（1启用 0不启用）"),
                    extra_properties = table.Column<string>(type: "longtext", nullable: true, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    concurrency_stamp = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: true, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    creation_time = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "创建时间"),
                    creator_id = table.Column<Guid>(type: "char(36)", nullable: true, comment: "创建人Id", collation: "ascii_general_ci"),
                    last_modification_time = table.Column<DateTime>(type: "datetime(6)", nullable: true, comment: "最后修改时间"),
                    last_modifier_id = table.Column<Guid>(type: "char(36)", nullable: true, comment: "最后人Id", collation: "ascii_general_ci"),
                    is_deleted = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false, comment: "Used to mark an Entity as 'Deleted'."),
                    deleter_id = table.Column<Guid>(type: "char(36)", nullable: true, comment: "Id of the deleter user.", collation: "ascii_general_ci"),
                    deletion_time = table.Column<DateTime>(type: "datetime(6)", nullable: true, comment: "Deletion time.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sys_dict_item", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sys_operation_info",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false, comment: "数据唯一标识")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    user_id = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, comment: "用户Id")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    title = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    content = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true, comment: "内容")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ip = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    address = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, comment: "地址")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    status = table.Column<int>(type: "int", nullable: false, comment: "状态(1-成功 2-失败)"),
                    creation_time = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "时间"),
                    extra_properties = table.Column<string>(type: "longtext", nullable: true, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    concurrency_stamp = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: true, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sys_operation_info", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sys_post",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false, comment: "数据唯一标识")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    tenant_id = table.Column<Guid>(type: "char(36)", nullable: true, comment: "租户Id", collation: "ascii_general_ci"),
                    code = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "编码")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    status = table.Column<int>(type: "int", nullable: false, comment: "状态"),
                    sort_order = table.Column<int>(type: "int", nullable: true, comment: "排序"),
                    memo = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true, comment: "备注")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    extra_properties = table.Column<string>(type: "longtext", nullable: true, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    concurrency_stamp = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: true, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    creation_time = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "创建时间"),
                    creator_id = table.Column<Guid>(type: "char(36)", nullable: true, comment: "创建人Id", collation: "ascii_general_ci"),
                    last_modification_time = table.Column<DateTime>(type: "datetime(6)", nullable: true, comment: "最后修改时间"),
                    last_modifier_id = table.Column<Guid>(type: "char(36)", nullable: true, comment: "最后人Id", collation: "ascii_general_ci"),
                    is_deleted = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false, comment: "Used to mark an Entity as 'Deleted'."),
                    deleter_id = table.Column<Guid>(type: "char(36)", nullable: true, comment: "Id of the deleter user.", collation: "ascii_general_ci"),
                    deletion_time = table.Column<DateTime>(type: "datetime(6)", nullable: true, comment: "Deletion time.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sys_post", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sys_role",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false, comment: "数据唯一标识")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    tenant_id = table.Column<Guid>(type: "char(36)", nullable: true, comment: "租户Id", collation: "ascii_general_ci"),
                    code = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "角色编码")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, comment: "角色名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    memo = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, comment: "备注")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    status = table.Column<int>(type: "int", nullable: false, comment: "状态"),
                    role_access_type = table.Column<int>(type: "int", nullable: false, comment: "可访问权限类型"),
                    access_value = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, comment: "访问权限编码")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    extra_properties = table.Column<string>(type: "longtext", nullable: true, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    concurrency_stamp = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: true, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    creation_time = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "创建时间"),
                    creator_id = table.Column<Guid>(type: "char(36)", nullable: true, comment: "创建人Id", collation: "ascii_general_ci"),
                    last_modification_time = table.Column<DateTime>(type: "datetime(6)", nullable: true, comment: "最后修改时间"),
                    last_modifier_id = table.Column<Guid>(type: "char(36)", nullable: true, comment: "最后人Id", collation: "ascii_general_ci"),
                    is_deleted = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false, comment: "Used to mark an Entity as 'Deleted'."),
                    deleter_id = table.Column<Guid>(type: "char(36)", nullable: true, comment: "Id of the deleter user.", collation: "ascii_general_ci"),
                    deletion_time = table.Column<DateTime>(type: "datetime(6)", nullable: true, comment: "Deletion time.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sys_role", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sys_role_ui_permission",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false, comment: "数据唯一标识")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    tenant_id = table.Column<Guid>(type: "char(36)", nullable: true, comment: "租户Id", collation: "ascii_general_ci"),
                    role_id = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false, comment: "角色Id")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ui_permission_id = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false, comment: "权限Id")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    extra_properties = table.Column<string>(type: "longtext", nullable: true, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    concurrency_stamp = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: true, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sys_role_ui_permission", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sys_tenant",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false, comment: "数据唯一标识")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    code = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "租户编码")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, comment: "名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    memo = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, comment: "备注")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    default_connection_string_name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, comment: "数据库连接配置名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_standalone_database = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "采用独立的数据库"),
                    standalone_database_connection_string = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: false, comment: "采用独立的数据库")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    phone = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true, comment: "手机号/电话")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true, comment: "邮箱")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    language = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, comment: "语言")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    status = table.Column<int>(type: "int", nullable: false, comment: ""),
                    extra_properties = table.Column<string>(type: "longtext", nullable: true, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    concurrency_stamp = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: true, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    creation_time = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "创建时间"),
                    creator_id = table.Column<Guid>(type: "char(36)", nullable: true, comment: "创建人Id", collation: "ascii_general_ci"),
                    last_modification_time = table.Column<DateTime>(type: "datetime(6)", nullable: true, comment: "最后修改时间"),
                    last_modifier_id = table.Column<Guid>(type: "char(36)", nullable: true, comment: "最后人Id", collation: "ascii_general_ci"),
                    is_deleted = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false, comment: "Used to mark an Entity as 'Deleted'."),
                    deleter_id = table.Column<Guid>(type: "char(36)", nullable: true, comment: "Id of the deleter user.", collation: "ascii_general_ci"),
                    deletion_time = table.Column<DateTime>(type: "datetime(6)", nullable: true, comment: "Deletion time.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sys_tenant", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sys_ui_permission",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false, comment: "数据唯一标识")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    tenant_id = table.Column<Guid>(type: "char(36)", nullable: true, comment: "租户Id", collation: "ascii_general_ci"),
                    parent_id = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, comment: "父id")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, comment: "菜单标题")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    en_name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, comment: "菜单英文标题")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    url = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, comment: "路径")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    component_name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, comment: "组件名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    component = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, comment: "组件")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    redirect = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, comment: "一级菜单跳转地址")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ui_menu_type = table.Column<int>(type: "int", nullable: false, comment: "菜单类型(0:一级菜单; 1:子菜单:2:按钮权限)"),
                    perms = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, comment: "菜单权限编码")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    perms_type = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, comment: "权限策略1显示2禁用")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    sort_order = table.Column<int>(type: "int", nullable: false, comment: "菜单排序"),
                    icon = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, comment: "菜单图标")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_route = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "是否路由菜单: 0:不是  1:是（默认值1）"),
                    is_leaf = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "是否叶子节点:    1:是   0:不是"),
                    keep_alive = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "是否缓存该页面:    1:是   0:不是"),
                    hidden = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "是否隐藏路由: 0否,1是"),
                    description = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, comment: "描述")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    enabled = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "按钮权限状态(0无效1有效)"),
                    open_mode = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "菜单打开方式 0/内部打开 1/外部打开"),
                    extra_properties = table.Column<string>(type: "longtext", nullable: true, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    concurrency_stamp = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: true, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    creation_time = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "创建时间"),
                    creator_id = table.Column<Guid>(type: "char(36)", nullable: true, comment: "创建人Id", collation: "ascii_general_ci"),
                    last_modification_time = table.Column<DateTime>(type: "datetime(6)", nullable: true, comment: "最后修改时间"),
                    last_modifier_id = table.Column<Guid>(type: "char(36)", nullable: true, comment: "最后人Id", collation: "ascii_general_ci"),
                    is_deleted = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false, comment: "Used to mark an Entity as 'Deleted'."),
                    deleter_id = table.Column<Guid>(type: "char(36)", nullable: true, comment: "Id of the deleter user.", collation: "ascii_general_ci"),
                    deletion_time = table.Column<DateTime>(type: "datetime(6)", nullable: true, comment: "Deletion time.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sys_ui_permission", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sys_ui_with_api",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false, comment: "数据唯一标识")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    tenant_id = table.Column<Guid>(type: "char(36)", nullable: true, comment: "租户Id", collation: "ascii_general_ci"),
                    ui_permission_id = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    permission_code = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false, comment: "接口资源编码")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sys_ui_with_api", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sys_user",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false, comment: "数据唯一标识")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    tenant_id = table.Column<Guid>(type: "char(36)", nullable: true, comment: "租户Id", collation: "ascii_general_ci"),
                    supper_user = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "管理员用户"),
                    depart_id = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, comment: "部门Id")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    post_id = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, comment: "职务Id")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    login_name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "登录账号")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    nickname = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "昵称/姓名")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    password = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, comment: "密码")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    salt = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, comment: "密码盐")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    avatar = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, comment: "头像")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    employee_id_number = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "工号")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    birthday = table.Column<DateTime>(type: "datetime(6)", nullable: true, comment: "生日"),
                    sex = table.Column<int>(type: "int", nullable: false, comment: "性别(0-默认未知,1-男,2-女)"),
                    user_type = table.Column<int>(type: "int", nullable: false, comment: "账号类型: ( 1: 平台   2:租户用户)"),
                    email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true, comment: "邮箱")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    phone = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "电话")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    memo = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, comment: "备注")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    status = table.Column<int>(type: "int", nullable: false, comment: "状态(1-正常,2-锁定)"),
                    login_ip = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, comment: "最后登录ip")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    lock_login = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "登录锁定"),
                    login_date = table.Column<DateTime>(type: "datetime(6)", nullable: true, comment: "最后登录时间"),
                    pwd_update_date = table.Column<DateTime>(type: "datetime(6)", nullable: true, comment: "最后密码更改时间"),
                    extra_properties = table.Column<string>(type: "longtext", nullable: true, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    concurrency_stamp = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: true, comment: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    creation_time = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "创建时间"),
                    creator_id = table.Column<Guid>(type: "char(36)", nullable: true, comment: "创建人Id", collation: "ascii_general_ci"),
                    last_modification_time = table.Column<DateTime>(type: "datetime(6)", nullable: true, comment: "最后修改时间"),
                    last_modifier_id = table.Column<Guid>(type: "char(36)", nullable: true, comment: "最后人Id", collation: "ascii_general_ci"),
                    is_deleted = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false, comment: "Used to mark an Entity as 'Deleted'."),
                    deleter_id = table.Column<Guid>(type: "char(36)", nullable: true, comment: "Id of the deleter user.", collation: "ascii_general_ci"),
                    deletion_time = table.Column<DateTime>(type: "datetime(6)", nullable: true, comment: "Deletion time.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sys_user", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sys_user_role",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false, comment: "数据唯一标识")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    tenant_id = table.Column<Guid>(type: "char(36)", nullable: true, comment: "租户Id", collation: "ascii_general_ci"),
                    user_id = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false, comment: "员工Id")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    role_id = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false, comment: "角色Id")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sys_user_role", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_sys_background_api_parent_permission_code",
                table: "sys_background_api",
                column: "parent_permission_code");

            migrationBuilder.CreateIndex(
                name: "IX_sys_background_api_permission_code",
                table: "sys_background_api",
                column: "permission_code");

            migrationBuilder.CreateIndex(
                name: "IX_sys_database_connection_name",
                table: "sys_database_connection",
                column: "name");

            migrationBuilder.CreateIndex(
                name: "IX_sys_depart_org_code",
                table: "sys_depart",
                column: "org_code");

            migrationBuilder.CreateIndex(
                name: "IX_sys_depart_org_name",
                table: "sys_depart",
                column: "org_name");

            migrationBuilder.CreateIndex(
                name: "IX_sys_depart_role_depart_id",
                table: "sys_depart_role",
                column: "depart_id");

            migrationBuilder.CreateIndex(
                name: "IX_sys_depart_role_role_id",
                table: "sys_depart_role",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_sys_dict_dict_code",
                table: "sys_dict",
                column: "dict_code");

            migrationBuilder.CreateIndex(
                name: "IX_sys_dict_item_dict_id",
                table: "sys_dict_item",
                column: "dict_id");

            migrationBuilder.CreateIndex(
                name: "IX_sys_dict_item_item_text",
                table: "sys_dict_item",
                column: "item_text");

            migrationBuilder.CreateIndex(
                name: "IX_sys_operation_info_user_id",
                table: "sys_operation_info",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_sys_post_code",
                table: "sys_post",
                column: "code");

            migrationBuilder.CreateIndex(
                name: "IX_sys_role_code",
                table: "sys_role",
                column: "code");

            migrationBuilder.CreateIndex(
                name: "IX_sys_role_name",
                table: "sys_role",
                column: "name");

            migrationBuilder.CreateIndex(
                name: "IX_sys_role_ui_permission_role_id",
                table: "sys_role_ui_permission",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_sys_role_ui_permission_ui_permission_id",
                table: "sys_role_ui_permission",
                column: "ui_permission_id");

            migrationBuilder.CreateIndex(
                name: "IX_sys_tenant_code",
                table: "sys_tenant",
                column: "code");

            migrationBuilder.CreateIndex(
                name: "IX_sys_tenant_name",
                table: "sys_tenant",
                column: "name");

            migrationBuilder.CreateIndex(
                name: "IX_sys_ui_permission_name",
                table: "sys_ui_permission",
                column: "name");

            migrationBuilder.CreateIndex(
                name: "IX_sys_ui_permission_parent_id",
                table: "sys_ui_permission",
                column: "parent_id");

            migrationBuilder.CreateIndex(
                name: "IX_sys_ui_with_api_permission_code",
                table: "sys_ui_with_api",
                column: "permission_code");

            migrationBuilder.CreateIndex(
                name: "IX_sys_ui_with_api_ui_permission_id",
                table: "sys_ui_with_api",
                column: "ui_permission_id");

            migrationBuilder.CreateIndex(
                name: "IX_sys_user_employee_id_number",
                table: "sys_user",
                column: "employee_id_number");

            migrationBuilder.CreateIndex(
                name: "IX_sys_user_login_name",
                table: "sys_user",
                column: "login_name");

            migrationBuilder.CreateIndex(
                name: "IX_sys_user_phone",
                table: "sys_user",
                column: "phone");

            migrationBuilder.CreateIndex(
                name: "IX_sys_user_role_role_id",
                table: "sys_user_role",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_sys_user_role_user_id",
                table: "sys_user_role",
                column: "user_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "sys_background_api");

            migrationBuilder.DropTable(
                name: "sys_database_connection");

            migrationBuilder.DropTable(
                name: "sys_depart");

            migrationBuilder.DropTable(
                name: "sys_depart_role");

            migrationBuilder.DropTable(
                name: "sys_dict");

            migrationBuilder.DropTable(
                name: "sys_dict_item");

            migrationBuilder.DropTable(
                name: "sys_operation_info");

            migrationBuilder.DropTable(
                name: "sys_post");

            migrationBuilder.DropTable(
                name: "sys_role");

            migrationBuilder.DropTable(
                name: "sys_role_ui_permission");

            migrationBuilder.DropTable(
                name: "sys_tenant");

            migrationBuilder.DropTable(
                name: "sys_ui_permission");

            migrationBuilder.DropTable(
                name: "sys_ui_with_api");

            migrationBuilder.DropTable(
                name: "sys_user");

            migrationBuilder.DropTable(
                name: "sys_user_role");
        }
    }
}
