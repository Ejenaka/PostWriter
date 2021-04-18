namespace TestWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditCommentModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Comments", "Post_ID", "dbo.Posts");
            DropIndex("dbo.Comments", new[] { "Post_ID" });
            RenameColumn(table: "dbo.Comments", name: "AuthorID", newName: "UserID");
            RenameColumn(table: "dbo.Comments", name: "Post_ID", newName: "PostID");
            RenameIndex(table: "dbo.Comments", name: "IX_AuthorID", newName: "IX_UserID");
            AlterColumn("dbo.Comments", "PostID", c => c.Int(nullable: false));
            CreateIndex("dbo.Comments", "PostID");
            AddForeignKey("dbo.Comments", "PostID", "dbo.Posts", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "PostID", "dbo.Posts");
            DropIndex("dbo.Comments", new[] { "PostID" });
            AlterColumn("dbo.Comments", "PostID", c => c.Int());
            RenameIndex(table: "dbo.Comments", name: "IX_UserID", newName: "IX_AuthorID");
            RenameColumn(table: "dbo.Comments", name: "PostID", newName: "Post_ID");
            RenameColumn(table: "dbo.Comments", name: "UserID", newName: "AuthorID");
            CreateIndex("dbo.Comments", "Post_ID");
            AddForeignKey("dbo.Comments", "Post_ID", "dbo.Posts", "ID");
        }
    }
}
