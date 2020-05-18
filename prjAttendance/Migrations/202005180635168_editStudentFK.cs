namespace prjAttendance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editStudentFK : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Students", "TeacherId", "dbo.Teachers");
            DropIndex("dbo.Students", new[] { "TeacherId" });
            AddColumn("dbo.Students", "ClasssId", c => c.Int(nullable: false));
            CreateIndex("dbo.Students", "ClasssId");
            AddForeignKey("dbo.Students", "ClasssId", "dbo.Classes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Students", "ClasssId", "dbo.Classes");
            DropIndex("dbo.Students", new[] { "ClasssId" });
            DropColumn("dbo.Students", "ClasssId");
            CreateIndex("dbo.Students", "TeacherId");
            AddForeignKey("dbo.Students", "TeacherId", "dbo.Teachers", "Id", cascadeDelete: true);
        }
    }
}
