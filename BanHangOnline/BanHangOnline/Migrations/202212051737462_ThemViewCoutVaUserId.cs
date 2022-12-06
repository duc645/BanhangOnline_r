namespace BanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ThemViewCoutVaUserId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_Order", "UserId", c => c.String());
            AddColumn("dbo.tb_Product", "ViewCout", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tb_Product", "ViewCout");
            DropColumn("dbo.tb_Order", "UserId");
        }
    }
}
