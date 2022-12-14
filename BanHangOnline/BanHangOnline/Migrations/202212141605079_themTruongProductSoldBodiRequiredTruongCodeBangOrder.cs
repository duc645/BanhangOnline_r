namespace BanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class themTruongProductSoldBodiRequiredTruongCodeBangOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_Product", "ProductSold", c => c.Int(nullable: false));
            AlterColumn("dbo.tb_Order", "Code", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tb_Order", "Code", c => c.String(nullable: false));
            DropColumn("dbo.tb_Product", "ProductSold");
        }
    }
}
