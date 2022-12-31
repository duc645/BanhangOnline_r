namespace BanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class themtruongIsActiveNXBTacgia : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_Author", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.tb_Publisher", "IsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tb_Publisher", "IsActive");
            DropColumn("dbo.tb_Author", "IsActive");
        }
    }
}
