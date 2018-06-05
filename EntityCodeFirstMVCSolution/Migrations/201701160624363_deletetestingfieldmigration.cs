namespace EntityCodeFirstMVCSolution.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deletetestingfieldmigration : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Students", "Test");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Students", "Test", c => c.String());
        }
    }
}
