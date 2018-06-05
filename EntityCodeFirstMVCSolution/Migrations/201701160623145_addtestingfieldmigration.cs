namespace EntityCodeFirstMVCSolution.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtestingfieldmigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "Test", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Students", "Test");
        }
    }
}
