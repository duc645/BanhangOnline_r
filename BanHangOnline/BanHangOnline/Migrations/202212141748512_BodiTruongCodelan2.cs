namespace BanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BodiTruongCodelan2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.tb_Order", "Code");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tb_Order", "Code", c => c.String());
        }
    }
}
