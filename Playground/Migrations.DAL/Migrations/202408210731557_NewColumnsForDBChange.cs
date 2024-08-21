namespace Migrations.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewColumnsForDBChange : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DBChanges", "Type", c => c.String());
            AddColumn("dbo.DBChanges", "CommandText", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DBChanges", "CommandText");
            DropColumn("dbo.DBChanges", "Type");
        }
    }
}
