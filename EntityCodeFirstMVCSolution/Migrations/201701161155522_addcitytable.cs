namespace EntityCodeFirstMVCSolution.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcitytable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.States", "Country_CountryName", "dbo.Countries");
            DropIndex("dbo.States", new[] { "Country_CountryName" });
            DropColumn("dbo.States", "CountryId");
            RenameColumn(table: "dbo.States", name: "Country_CountryName", newName: "CountryId");
            DropPrimaryKey("dbo.Countries");
            DropPrimaryKey("dbo.States");
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        CityId = c.Int(nullable: false, identity: true),
                        CityName = c.String(maxLength: 50, unicode: false),
                        StateId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CityId)
                .ForeignKey("dbo.States", t => t.StateId, cascadeDelete: true)
                .Index(t => t.StateId);
            
            AddColumn("dbo.Students", "CityId", c => c.Int(nullable: false));
            AlterColumn("dbo.Countries", "CountryName", c => c.String(maxLength: 50, unicode: false));
            AlterColumn("dbo.States", "StateName", c => c.String(maxLength: 50, unicode: false));
            AlterColumn("dbo.States", "CountryId", c => c.Int(nullable: false));
            AlterColumn("dbo.Standards", "StandardName", c => c.String(maxLength: 50, unicode: false));
            AlterColumn("dbo.Students", "StudentName", c => c.String(maxLength: 50, unicode: false));
            AddPrimaryKey("dbo.Countries", "CountryId");
            AddPrimaryKey("dbo.States", "StateId");
            CreateIndex("dbo.States", "CountryId");
            CreateIndex("dbo.Students", "CityId");
            AddForeignKey("dbo.Students", "CityId", "dbo.Cities", "CityId", cascadeDelete: true);
            AddForeignKey("dbo.States", "CountryId", "dbo.Countries", "CountryId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.States", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.Students", "CityId", "dbo.Cities");
            DropForeignKey("dbo.Cities", "StateId", "dbo.States");
            DropIndex("dbo.Cities", new[] { "StateId" });
            DropIndex("dbo.Students", new[] { "CityId" });
            DropIndex("dbo.States", new[] { "CountryId" });
            DropPrimaryKey("dbo.States");
            DropPrimaryKey("dbo.Countries");
            AlterColumn("dbo.Students", "StudentName", c => c.String());
            AlterColumn("dbo.Standards", "StandardName", c => c.String());
            AlterColumn("dbo.States", "CountryId", c => c.String(maxLength: 50, unicode: false));
            AlterColumn("dbo.States", "StateName", c => c.String(nullable: false, maxLength: 50, unicode: false));
            AlterColumn("dbo.Countries", "CountryName", c => c.String(nullable: false, maxLength: 50, unicode: false));
            DropColumn("dbo.Students", "CityId");
            DropTable("dbo.Cities");
            AddPrimaryKey("dbo.States", "StateName");
            AddPrimaryKey("dbo.Countries", "CountryName");
            RenameColumn(table: "dbo.States", name: "CountryId", newName: "Country_CountryName");
            AddColumn("dbo.States", "CountryId", c => c.Int(nullable: false));
            CreateIndex("dbo.States", "Country_CountryName");
            AddForeignKey("dbo.States", "Country_CountryName", "dbo.Countries", "CountryName");
        }
    }
}
