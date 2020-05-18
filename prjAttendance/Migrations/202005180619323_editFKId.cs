namespace prjAttendance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editFKId : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Classes", new[] { "TeacherID" });
            DropIndex("dbo.Students", new[] { "TeacherID" });
            DropIndex("dbo.Timetables", new[] { "TeacherID" });
            DropIndex("dbo.Records", new[] { "StudentID" });
            AlterColumn("dbo.Records", "Attendance", c => c.Int(nullable: false));
            CreateIndex("dbo.Classes", "TeacherId");
            CreateIndex("dbo.Students", "TeacherId");
            CreateIndex("dbo.Timetables", "TeacherId");
            CreateIndex("dbo.Records", "StudentId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Records", new[] { "StudentId" });
            DropIndex("dbo.Timetables", new[] { "TeacherId" });
            DropIndex("dbo.Students", new[] { "TeacherId" });
            DropIndex("dbo.Classes", new[] { "TeacherId" });
            AlterColumn("dbo.Records", "Attendance", c => c.String());
            CreateIndex("dbo.Records", "StudentID");
            CreateIndex("dbo.Timetables", "TeacherID");
            CreateIndex("dbo.Students", "TeacherID");
            CreateIndex("dbo.Classes", "TeacherID");
        }
    }
}
