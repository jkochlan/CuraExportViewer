namespace Migrations.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddChangeLogTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ChangeLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ChangeType = c.Int(nullable: false),
                        ChangeDate = c.DateTime(nullable: false),
                        ChangeContext = c.Guid(nullable: false),
                        Entity = c.String(nullable: false, maxLength: 100),
                        PrimaryKey = c.String(maxLength: 100),
                        IsRelationChange = c.Boolean(nullable: false),
                        Hash = c.String(maxLength: 256),
                        PredecessorHashReference = c.String(maxLength: 256),
                        Property = c.String(nullable: false, maxLength: 100),
                        OldValue = c.String(),
                        NewValue = c.String(),
                        IsIncrementalValidated = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ChangeLogs");
        }
    }
}
