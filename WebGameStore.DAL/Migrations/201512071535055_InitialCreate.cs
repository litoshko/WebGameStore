namespace WebGameStore.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Body = c.String(),
                        GameKey = c.String(maxLength: 128),
                        ParentCommentId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Games", t => t.GameKey)
                .ForeignKey("dbo.Comments", t => t.ParentCommentId)
                .Index(t => t.GameKey)
                .Index(t => t.ParentCommentId);
            
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        Key = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Key);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "ParentCommentId", "dbo.Comments");
            DropForeignKey("dbo.Comments", "GameKey", "dbo.Games");
            DropIndex("dbo.Comments", new[] { "ParentCommentId" });
            DropIndex("dbo.Comments", new[] { "GameKey" });
            DropTable("dbo.Games");
            DropTable("dbo.Comments");
        }
    }
}
