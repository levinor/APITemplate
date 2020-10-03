using Microsoft.EntityFrameworkCore.Migrations;

namespace Levinor.Business.Migrations
{
    public partial class delete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Passwords_PasswordId",
                schema: "dbo",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Roles_RoleId",
                schema: "dbo",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_PasswordId",
                schema: "dbo",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                table: "Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Passwords",
                table: "Passwords");

            migrationBuilder.RenameTable(
                name: "Roles",
                newName: "Role",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "Passwords",
                newName: "Password",
                newSchema: "dbo");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                schema: "dbo",
                table: "Password",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Role",
                schema: "dbo",
                table: "Role",
                column: "RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Password",
                schema: "dbo",
                table: "Password",
                column: "PasswordId");

            migrationBuilder.CreateIndex(
                name: "IX_User_PasswordId",
                schema: "dbo",
                table: "User",
                column: "PasswordId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Password_PasswordId",
                schema: "dbo",
                table: "User",
                column: "PasswordId",
                principalSchema: "dbo",
                principalTable: "Password",
                principalColumn: "PasswordId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Role_RoleId",
                schema: "dbo",
                table: "User",
                column: "RoleId",
                principalSchema: "dbo",
                principalTable: "Role",
                principalColumn: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Password_PasswordId",
                schema: "dbo",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Role_RoleId",
                schema: "dbo",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_PasswordId",
                schema: "dbo",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Role",
                schema: "dbo",
                table: "Role");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Password",
                schema: "dbo",
                table: "Password");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "dbo",
                table: "Password");

            migrationBuilder.RenameTable(
                name: "Role",
                schema: "dbo",
                newName: "Roles");

            migrationBuilder.RenameTable(
                name: "Password",
                schema: "dbo",
                newName: "Passwords");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                table: "Roles",
                column: "RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Passwords",
                table: "Passwords",
                column: "PasswordId");

            migrationBuilder.CreateIndex(
                name: "IX_User_PasswordId",
                schema: "dbo",
                table: "User",
                column: "PasswordId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Passwords_PasswordId",
                schema: "dbo",
                table: "User",
                column: "PasswordId",
                principalTable: "Passwords",
                principalColumn: "PasswordId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Roles_RoleId",
                schema: "dbo",
                table: "User",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "RoleId");
        }
    }
}
