namespace BanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteproduct_category : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Product_Category", "ProductId", "dbo.tb_Product");
            DropForeignKey("dbo.Product_Category", "ProductCategoryId", "dbo.tb_ProductCategory");
            DropIndex("dbo.Product_Category", new[] { "ProductId" });
            DropIndex("dbo.Product_Category", new[] { "ProductCategoryId" });
            AddColumn("dbo.tb_Product", "CategoryId", c => c.Int(nullable: false));
            AddColumn("dbo.tb_Product", "productcategory_Id", c => c.Int());
            CreateIndex("dbo.tb_Product", "productcategory_Id");
            AddForeignKey("dbo.tb_Product", "productcategory_Id", "dbo.tb_ProductCategory", "Id");
            DropTable("dbo.Product_Category");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Product_Category",
                c => new
                    {
                        ProductId = c.Int(nullable: false),
                        ProductCategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProductId, t.ProductCategoryId });
            
            DropForeignKey("dbo.tb_Product", "productcategory_Id", "dbo.tb_ProductCategory");
            DropIndex("dbo.tb_Product", new[] { "productcategory_Id" });
            DropColumn("dbo.tb_Product", "productcategory_Id");
            DropColumn("dbo.tb_Product", "CategoryId");
            CreateIndex("dbo.Product_Category", "ProductCategoryId");
            CreateIndex("dbo.Product_Category", "ProductId");
            AddForeignKey("dbo.Product_Category", "ProductCategoryId", "dbo.tb_ProductCategory", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Product_Category", "ProductId", "dbo.tb_Product", "Id", cascadeDelete: true);
        }
    }
}
