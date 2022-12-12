namespace BanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class themViewCountNews : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_News", "ViewCount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tb_News", "ViewCount");
        }
    }
}
