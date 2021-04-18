namespace TestWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCommentsAndPosts : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "Title", c => c.String(nullable: false));
            AddColumn("dbo.Posts", "Themes", c => c.String());
            AddColumn("dbo.Posts", "Likes", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "Likes");
            DropColumn("dbo.Posts", "Themes");
            DropColumn("dbo.Posts", "Title");
        }
    }
}
