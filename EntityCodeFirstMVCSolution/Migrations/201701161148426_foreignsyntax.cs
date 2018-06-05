namespace EntityCodeFirstMVCSolution.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class foreignsyntax : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Students", "Teacher_TeacherName", "dbo.Teachers");
            DropIndex("dbo.Students", new[] { "Teacher_TeacherName" });
            DropColumn("dbo.Students", "TeacherId");
            RenameColumn(table: "dbo.Students", name: "Teacher_TeacherName", newName: "TeacherId");
            DropPrimaryKey("dbo.Teachers");
            AlterColumn("dbo.Students", "TeacherId", c => c.Int(nullable: false));
            AlterColumn("dbo.Teachers", "TeacherName", c => c.String(maxLength: 50, unicode: false));
            AddPrimaryKey("dbo.Teachers", "TeacherId");
            CreateIndex("dbo.Students", "TeacherId");
            AddForeignKey("dbo.Students", "TeacherId", "dbo.Teachers", "TeacherId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Students", "TeacherId", "dbo.Teachers");
            DropIndex("dbo.Students", new[] { "TeacherId" });
            DropPrimaryKey("dbo.Teachers");
            AlterColumn("dbo.Teachers", "TeacherName", c => c.String(nullable: false, maxLength: 50, unicode: false));
            AlterColumn("dbo.Students", "TeacherId", c => c.String(maxLength: 50, unicode: false));
            AddPrimaryKey("dbo.Teachers", "TeacherName");
            RenameColumn(table: "dbo.Students", name: "TeacherId", newName: "Teacher_TeacherName");
            AddColumn("dbo.Students", "TeacherId", c => c.Int(nullable: false));
            CreateIndex("dbo.Students", "Teacher_TeacherName");
            AddForeignKey("dbo.Students", "Teacher_TeacherName", "dbo.Teachers", "TeacherName");
        }
    }
}
