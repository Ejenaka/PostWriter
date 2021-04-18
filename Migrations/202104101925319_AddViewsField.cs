namespace TestWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddViewsField : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Posts", "AuthorID", "dbo.Users");
            AddColumn("dbo.Posts", "Views", c => c.Int(nullable: false));
            AddColumn("dbo.Posts", "User_ID", c => c.Int());
            AddColumn("dbo.Users", "Post_ID", c => c.Int());
            CreateIndex("dbo.Posts", "User_ID");
            CreateIndex("dbo.Users", "Post_ID");
            AddForeignKey("dbo.Users", "Post_ID", "dbo.Posts", "ID");
            AddForeignKey("dbo.Posts", "User_ID", "dbo.Users", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", "User_ID", "dbo.Users");
            DropForeignKey("dbo.Users", "Post_ID", "dbo.Posts");
            DropIndex("dbo.Users", new[] { "Post_ID" });
            DropIndex("dbo.Posts", new[] { "User_ID" });
            DropColumn("dbo.Users", "Post_ID");
            DropColumn("dbo.Posts", "User_ID");
            DropColumn("dbo.Posts", "Views");
            AddForeignKey("dbo.Posts", "AuthorID", "dbo.Users", "ID", cascadeDelete: true);
        }
    }
}
