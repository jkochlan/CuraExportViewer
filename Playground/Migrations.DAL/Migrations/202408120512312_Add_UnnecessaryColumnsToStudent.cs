namespace Migrations.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_UnnecessaryColumnsToStudent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "ParentName", c => c.String());
            AddColumn("dbo.Students", "DogName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Students", "DogName");
            DropColumn("dbo.Students", "ParentName");
        }
    }
}
