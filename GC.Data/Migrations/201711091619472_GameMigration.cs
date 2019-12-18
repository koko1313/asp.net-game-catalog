namespace GC.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GameMigration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Games", "ReleaseYear", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Games", "ReleaseYear", c => c.String(nullable: false));
        }
    }
}
