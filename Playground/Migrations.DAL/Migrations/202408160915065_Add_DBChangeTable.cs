namespace Migrations.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_DBChangeTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DBChanges",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TableName = c.String(),
                        ColumnName = c.String(),
                        ColumnType = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.DBChanges");
        }
    }
}
