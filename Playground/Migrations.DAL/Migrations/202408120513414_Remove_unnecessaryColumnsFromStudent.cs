namespace Migrations.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Remove_unnecessaryColumnsFromStudent : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Students", "ParentName");
            DropColumn("dbo.Students", "DogName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Students", "DogName", c => c.String());
            AddColumn("dbo.Students", "ParentName", c => c.String());
        }
    }
}
