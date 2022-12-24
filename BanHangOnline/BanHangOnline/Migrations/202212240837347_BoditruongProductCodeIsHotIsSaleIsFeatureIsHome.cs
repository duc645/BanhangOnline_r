namespace BanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BoditruongProductCodeIsHotIsSaleIsFeatureIsHome : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tb_Product", "Description", c => c.String(maxLength: 50));
            AlterColumn("dbo.tb_ProductCategory", "Alias", c => c.String(maxLength: 150));
            DropColumn("dbo.tb_Product", "ProductCode");
            DropColumn("dbo.tb_Product", "IsHome");
            DropColumn("dbo.tb_Product", "IsSale");
            DropColumn("dbo.tb_Product", "IsFeature");
            DropColumn("dbo.tb_Product", "IsHot");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tb_Product", "IsHot", c => c.Boolean(nullable: false));
            AddColumn("dbo.tb_Product", "IsFeature", c => c.Boolean(nullable: false));
            AddColumn("dbo.tb_Product", "IsSale", c => c.Boolean(nullable: false));
            AddColumn("dbo.tb_Product", "IsHome", c => c.Boolean(nullable: false));
            AddColumn("dbo.tb_Product", "ProductCode", c => c.String(maxLength: 50));
            AlterColumn("dbo.tb_ProductCategory", "Alias", c => c.String(nullable: false, maxLength: 150));
            AlterColumn("dbo.tb_Product", "Description", c => c.String());
        }
    }
}
