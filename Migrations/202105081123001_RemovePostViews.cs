namespace TestWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovePostViews : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Posts", "Views");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "Views", c => c.Int(nullable: false));
        }
    }
}
