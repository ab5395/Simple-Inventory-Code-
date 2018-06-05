namespace EntityCodeFirstMVCSolution.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addteachertable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Teachers",
                c => new
                    {
                        TeacherId = c.Int(nullable: false, identity: true),
                        TeacherName = c.String(),
                    })
                .PrimaryKey(t => t.TeacherId);
            
            AddColumn("dbo.Students", "Teacher_TeacherId", c => c.Int());
            CreateIndex("dbo.Students", "Teacher_TeacherId");
            AddForeignKey("dbo.Students", "Teacher_TeacherId", "dbo.Teachers", "TeacherId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Students", "Teacher_TeacherId", "dbo.Teachers");
            DropIndex("dbo.Students", new[] { "Teacher_TeacherId" });
            DropColumn("dbo.Students", "Teacher_TeacherId");
            DropTable("dbo.Teachers");
        }
    }
}
