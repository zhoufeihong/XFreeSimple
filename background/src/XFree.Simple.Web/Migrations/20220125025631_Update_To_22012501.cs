using Microsoft.EntityFrameworkCore.Migrations;

namespace XFreeSimpleService.Host.Migrations
{
    public partial class Update_To_22012501 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "multi_tenancy_sides",
                table: "sys_ui_permission",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "");

            migrationBuilder.AlterColumn<int>(
                name: "status",
                table: "sys_tenant",
                type: "int",
                nullable: false,
                comment: "商户状态",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "");

            migrationBuilder.AddColumn<int>(
                name: "initial_data_status",
                table: "sys_tenant",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "数据初始化状态TODO: Job重试");

            migrationBuilder.AddColumn<int>(
                name: "multi_tenancy_sides",
                table: "sys_background_api",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "multi_tenancy_sides",
                table: "sys_ui_permission");

            migrationBuilder.DropColumn(
                name: "initial_data_status",
                table: "sys_tenant");

            migrationBuilder.DropColumn(
                name: "multi_tenancy_sides",
                table: "sys_background_api");

            migrationBuilder.AlterColumn<int>(
                name: "status",
                table: "sys_tenant",
                type: "int",
                nullable: false,
                comment: "",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "商户状态");
        }
    }
}
