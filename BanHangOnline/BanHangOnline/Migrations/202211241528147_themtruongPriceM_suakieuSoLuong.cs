namespace BanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class themtruongPriceM_suakieuSoLuong : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_Product", "PriceM", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.tb_Product", "Quantity", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tb_Product", "Quantity", c => c.String(nullable: false));
            DropColumn("dbo.tb_Product", "PriceM");
        }
    }
}
