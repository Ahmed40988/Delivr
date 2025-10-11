using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Deliver.Dal.Migrations
{
    public partial class updatecategory23 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1️⃣ إضافة العمود SubCategoryId كـ nullable مؤقت
            migrationBuilder.AddColumn<int>(
                name: "SubCategoryId",
                table: "Suppliers",
                type: "int",
                nullable: true);

            // 2️⃣ تحديث الـ Suppliers الموجودة لقيمة صالحة
            // لازم يكون فيه SubCategory موجود بالـ Id = 1 على الأقل
            migrationBuilder.Sql(
                "UPDATE Suppliers SET SubCategoryId = 1 WHERE SubCategoryId IS NULL;"
            );

            // 3️⃣ جعل العمود غير nullable بعد تحديث البيانات
            migrationBuilder.AlterColumn<int>(
                name: "SubCategoryId",
                table: "Suppliers",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true
            );

            // 4️⃣ إنشاء Index على العمود الجديد
            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_SubCategoryId",
                table: "Suppliers",
                column: "SubCategoryId"
            );

            // 5️⃣ إضافة الـ Foreign Key
            migrationBuilder.AddForeignKey(
                name: "FK_Suppliers_subCategories_SubCategoryId",
                table: "Suppliers",
                column: "SubCategoryId",
                principalTable: "subCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // إزالة الـ Foreign Key والعمود في حالة التراجع
            migrationBuilder.DropForeignKey(
                name: "FK_Suppliers_subCategories_SubCategoryId",
                table: "Suppliers");

            migrationBuilder.DropIndex(
                name: "IX_Suppliers_SubCategoryId",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "SubCategoryId",
                table: "Suppliers");
        }
    }
}
