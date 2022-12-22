namespace BanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ThemTruongBlockBangUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Block", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Block");
        }
    }
}
