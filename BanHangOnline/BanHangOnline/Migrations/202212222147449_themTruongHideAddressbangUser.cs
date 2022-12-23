namespace BanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class themTruongHideAddressbangUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "HideAddress", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "HideAddress");
        }
    }
}
