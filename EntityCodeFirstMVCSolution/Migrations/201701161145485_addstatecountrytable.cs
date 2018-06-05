namespace EntityCodeFirstMVCSolution.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addstatecountrytable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Students", "TeacherId", "dbo.Teachers");
            DropIndex("dbo.Students", new[] { "TeacherId" });
            DropPrimaryKey("dbo.Teachers");
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        CountryName = c.String(nullable: false, maxLength: 50, unicode: false),
                        CountryId = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.CountryName);
            
            CreateTable(
                "dbo.States",
                c => new
                    {
                        StateName = c.String(nullable: false, maxLength: 50, unicode: false),
                        StateId = c.Int(nullable: false, identity: true),
                        CountryId = c.Int(nullable: false),
                        Country_CountryName = c.String(maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.StateName)
                .ForeignKey("dbo.Countries", t => t.Country_CountryName)
                .Index(t => t.Country_CountryName);
            
            AddColumn("dbo.Students", "Teacher_TeacherName", c => c.String(maxLength: 50, unicode: false));
            AlterColumn("dbo.Teachers", "TeacherName", c => c.String(nullable: false, maxLength: 50, unicode: false));
            AddPrimaryKey("dbo.Teachers", "TeacherName");
            CreateIndex("dbo.Students", "Teacher_TeacherName");
            AddForeignKey("dbo.Students", "Teacher_TeacherName", "dbo.Teachers", "TeacherName");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Students", "Teacher_TeacherName", "dbo.Teachers");
            DropForeignKey("dbo.States", "Country_CountryName", "dbo.Countries");
            DropIndex("dbo.Students", new[] { "Teacher_TeacherName" });
            DropIndex("dbo.States", new[] { "Country_CountryName" });
            DropPrimaryKey("dbo.Teachers");
            AlterColumn("dbo.Teachers", "TeacherName", c => c.String());
            DropColumn("dbo.Students", "Teacher_TeacherName");
            DropTable("dbo.States");
            DropTable("dbo.Countries");
            AddPrimaryKey("dbo.Teachers", "TeacherId");
            CreateIndex("dbo.Students", "TeacherId");
            AddForeignKey("dbo.Students", "TeacherId", "dbo.Teachers", "TeacherId", cascadeDelete: true);
        }
    }
}
