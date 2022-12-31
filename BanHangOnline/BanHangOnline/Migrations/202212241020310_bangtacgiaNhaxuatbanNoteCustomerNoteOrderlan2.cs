namespace BanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bangtacgiaNhaxuatbanNoteCustomerNoteOrderlan2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tb_Author",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullName = c.String(nullable: false, maxLength: 150),
                        Description = c.String(maxLength: 150),
                        CreateBy = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tb_Publisher",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullName = c.String(nullable: false, maxLength: 150),
                        Description = c.String(maxLength: 150),
                        CreateBy = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.tb_Order", "Note", c => c.String(maxLength: 250));
            AddColumn("dbo.tb_Order", "CustomerNote", c => c.String(maxLength: 250));
            AddColumn("dbo.tb_Product", "AuthorId", c => c.Int(nullable: false));
            AddColumn("dbo.tb_Product", "PublisherId", c => c.Int(nullable: false));
            AddColumn("dbo.tb_Product", "PublishedYear", c => c.Int(nullable: false));
            CreateIndex("dbo.tb_Product", "AuthorId");
            CreateIndex("dbo.tb_Product", "PublisherId");
            AddForeignKey("dbo.tb_Product", "AuthorId", "dbo.tb_Author", "Id", cascadeDelete: true);
            AddForeignKey("dbo.tb_Product", "PublisherId", "dbo.tb_Publisher", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tb_Product", "PublisherId", "dbo.tb_Publisher");
            DropForeignKey("dbo.tb_Product", "AuthorId", "dbo.tb_Author");
            DropIndex("dbo.tb_Product", new[] { "PublisherId" });
            DropIndex("dbo.tb_Product", new[] { "AuthorId" });
            DropColumn("dbo.tb_Product", "PublishedYear");
            DropColumn("dbo.tb_Product", "PublisherId");
            DropColumn("dbo.tb_Product", "AuthorId");
            DropColumn("dbo.tb_Order", "CustomerNote");
            DropColumn("dbo.tb_Order", "Note");
            DropTable("dbo.tb_Publisher");
            DropTable("dbo.tb_Author");
        }
    }
}
