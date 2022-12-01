namespace BanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class thembangOrderStatus : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tb_OrderStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.tb_Order", "OrderStatusId", c => c.Int(nullable: false));
            CreateIndex("dbo.tb_Order", "OrderStatusId");
            AddForeignKey("dbo.tb_Order", "OrderStatusId", "dbo.tb_OrderStatus", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tb_Order", "OrderStatusId", "dbo.tb_OrderStatus");
            DropIndex("dbo.tb_Order", new[] { "OrderStatusId" });
            DropColumn("dbo.tb_Order", "OrderStatusId");
            DropTable("dbo.tb_OrderStatus");
        }
    }
}
