using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace XFreeSimpleService.Host.Migrations
{
    public partial class Update_To_22012601 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "standalone_database_connection_string",
                table: "sys_tenant",
                type: "varchar(512)",
                maxLength: 512,
                nullable: false,
                comment: "采用独立的数据库连接字符串",
                oldClrType: typeof(string),
                oldType: "varchar(512)",
                oldMaxLength: 512,
                oldComment: "采用独立的数据库")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "tenant_id",
                table: "sys_operation_info",
                type: "char(36)",
                nullable: true,
                comment: "",
                collation: "ascii_general_ci");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "tenant_id",
                table: "sys_operation_info");

            migrationBuilder.AlterColumn<string>(
                name: "standalone_database_connection_string",
                table: "sys_tenant",
                type: "varchar(512)",
                maxLength: 512,
                nullable: false,
                comment: "采用独立的数据库",
                oldClrType: typeof(string),
                oldType: "varchar(512)",
                oldMaxLength: 512,
                oldComment: "采用独立的数据库连接字符串")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
