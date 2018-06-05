namespace EntityCodeFirstMVCSolution.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deletesomerowinstudent : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Students", "Photo");
            DropColumn("dbo.Students", "Height");
            DropColumn("dbo.Students", "Weight");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Students", "Weight", c => c.Single(nullable: false));
            AddColumn("dbo.Students", "Height", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Students", "Photo", c => c.Binary());
        }
    }
}
