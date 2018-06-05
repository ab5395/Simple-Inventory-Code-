namespace EntityCodeFirstMVCSolution.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class foreignkeyinstudent : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Students", "Standard_StandardId", "dbo.Standards");
            DropForeignKey("dbo.Students", "Teacher_TeacherId", "dbo.Teachers");
            DropIndex("dbo.Students", new[] { "Standard_StandardId" });
            DropIndex("dbo.Students", new[] { "Teacher_TeacherId" });
            RenameColumn(table: "dbo.Students", name: "Standard_StandardId", newName: "StandardId");
            RenameColumn(table: "dbo.Students", name: "Teacher_TeacherId", newName: "TeacherId");
            AlterColumn("dbo.Students", "StandardId", c => c.Int(nullable: false));
            AlterColumn("dbo.Students", "TeacherId", c => c.Int(nullable: false));
            CreateIndex("dbo.Students", "StandardId");
            CreateIndex("dbo.Students", "TeacherId");
            AddForeignKey("dbo.Students", "StandardId", "dbo.Standards", "StandardId", cascadeDelete: true);
            AddForeignKey("dbo.Students", "TeacherId", "dbo.Teachers", "TeacherId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Students", "TeacherId", "dbo.Teachers");
            DropForeignKey("dbo.Students", "StandardId", "dbo.Standards");
            DropIndex("dbo.Students", new[] { "TeacherId" });
            DropIndex("dbo.Students", new[] { "StandardId" });
            AlterColumn("dbo.Students", "TeacherId", c => c.Int());
            AlterColumn("dbo.Students", "StandardId", c => c.Int());
            RenameColumn(table: "dbo.Students", name: "TeacherId", newName: "Teacher_TeacherId");
            RenameColumn(table: "dbo.Students", name: "StandardId", newName: "Standard_StandardId");
            CreateIndex("dbo.Students", "Teacher_TeacherId");
            CreateIndex("dbo.Students", "Standard_StandardId");
            AddForeignKey("dbo.Students", "Teacher_TeacherId", "dbo.Teachers", "TeacherId");
            AddForeignKey("dbo.Students", "Standard_StandardId", "dbo.Standards", "StandardId");
        }
    }
}
