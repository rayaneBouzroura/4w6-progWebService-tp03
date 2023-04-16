using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SuperGalerieInfinie.Migrations
{
    public partial class seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "11111111-1111-1111-1111-111111111111", 0, "92bb3fc5-7f62-4e59-8fcd-0d19a23f84c0", "fluffy@gmail.com", false, false, null, "FLUFFY@GMAIL.COM", "MISTERFLUFFY", "AQAAAAEAACcQAAAAEEqZgVmKkfHdC6HCipW5HZZJLO0Obf6dwjF4YY8IErUORmUYADW/VbSgazZBlq4rew==", null, false, "f8199bdb-f07f-47d6-954f-1600bd955116", false, "MisterFluffy" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "22222222-2222-2222-2222-222222222222", 0, "ca857351-0d2c-48bc-a975-bb40d39720c9", "nemo@gmail.com", false, false, null, "NEMO@GMAIL.COM", "CAPTAINNEMO", "AQAAAAEAACcQAAAAEIaXxCvxCw83q6My+NcVKS/YYJVrsAjJG4gc+6jLKfUNXtDapFqK348+RHurXb8xbg==", null, false, "ead74957-e427-4824-8823-e3778cbcf74a", false, "CaptainNemo" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "11111111-1111-1111-1111-111111111111");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "22222222-2222-2222-2222-222222222222");
        }
    }
}
