namespace BanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ThemTruongSubaddressBangUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "SubAddress", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "SubAddress");
        }
    }
}
