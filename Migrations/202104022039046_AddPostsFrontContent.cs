namespace TestWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPostsFrontContent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "FrontContent", c => c.String(nullable: false));
            AlterColumn("dbo.Posts", "Content", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Posts", "Content", c => c.String());
            DropColumn("dbo.Posts", "FrontContent");
        }
    }
}
