namespace BanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sualan2bangContactThamCommonAbstract : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contacts", "CreateBy", c => c.String());
            AddColumn("dbo.Contacts", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Contacts", "ModifiedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Contacts", "ModifiedBy", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Contacts", "ModifiedBy");
            DropColumn("dbo.Contacts", "ModifiedDate");
            DropColumn("dbo.Contacts", "CreatedDate");
            DropColumn("dbo.Contacts", "CreateBy");
        }
    }
}
