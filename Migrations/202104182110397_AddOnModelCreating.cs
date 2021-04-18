namespace TestWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOnModelCreating : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "Post_ID", "dbo.Posts");
            DropForeignKey("dbo.Posts", "User_ID", "dbo.Users");
            DropIndex("dbo.Users", new[] { "Post_ID" });
            DropIndex("dbo.Posts", new[] { "User_ID" });
            CreateTable(
                "dbo.UsersLikedPosts",
                c => new
                    {
                        UserID = c.Int(nullable: false),
                        PostID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserID, t.PostID })
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .ForeignKey("dbo.Posts", t => t.PostID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.PostID);
            
            DropColumn("dbo.Users", "Post_ID");
            DropColumn("dbo.Posts", "User_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "User_ID", c => c.Int());
            AddColumn("dbo.Users", "Post_ID", c => c.Int());
            DropForeignKey("dbo.UsersLikedPosts", "PostID", "dbo.Posts");
            DropForeignKey("dbo.UsersLikedPosts", "UserID", "dbo.Users");
            DropIndex("dbo.UsersLikedPosts", new[] { "PostID" });
            DropIndex("dbo.UsersLikedPosts", new[] { "UserID" });
            DropTable("dbo.UsersLikedPosts");
            CreateIndex("dbo.Posts", "User_ID");
            CreateIndex("dbo.Users", "Post_ID");
            AddForeignKey("dbo.Posts", "User_ID", "dbo.Users", "ID");
            AddForeignKey("dbo.Users", "Post_ID", "dbo.Posts", "ID");
        }
    }
}
