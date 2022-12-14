namespace BanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sualaibangLienHe : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Contacts", "Message", c => c.String(nullable: false));
            DropColumn("dbo.Contacts", "Website");
            DropColumn("dbo.Contacts", "IsRead");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Contacts", "IsRead", c => c.Boolean(nullable: false));
            AddColumn("dbo.Contacts", "Website", c => c.String());
            AlterColumn("dbo.Contacts", "Message", c => c.String());
        }
    }
}
