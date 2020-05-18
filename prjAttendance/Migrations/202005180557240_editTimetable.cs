namespace prjAttendance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editTimetable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Timetables", "ClassID", "dbo.Classes");
            DropIndex("dbo.Timetables", new[] { "ClassID" });
            RenameColumn(table: "dbo.Timetables", name: "ClassID", newName: "Class_Id");
            AddColumn("dbo.Timetables", "TeacherID", c => c.Int(nullable: false));
            AddColumn("dbo.Timetables", "ClassName", c => c.String());
            AlterColumn("dbo.Timetables", "Class_Id", c => c.Int());
            CreateIndex("dbo.Timetables", "TeacherID");
            CreateIndex("dbo.Timetables", "Class_Id");
            AddForeignKey("dbo.Timetables", "TeacherID", "dbo.Teachers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Timetables", "Class_Id", "dbo.Classes", "Id");
            DropColumn("dbo.Timetables", "StudentID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Timetables", "StudentID", c => c.String());
            DropForeignKey("dbo.Timetables", "Class_Id", "dbo.Classes");
            DropForeignKey("dbo.Timetables", "TeacherID", "dbo.Teachers");
            DropIndex("dbo.Timetables", new[] { "Class_Id" });
            DropIndex("dbo.Timetables", new[] { "TeacherID" });
            AlterColumn("dbo.Timetables", "Class_Id", c => c.Int(nullable: false));
            DropColumn("dbo.Timetables", "ClassName");
            DropColumn("dbo.Timetables", "TeacherID");
            RenameColumn(table: "dbo.Timetables", name: "Class_Id", newName: "ClassID");
            CreateIndex("dbo.Timetables", "ClassID");
            AddForeignKey("dbo.Timetables", "ClassID", "dbo.Classes", "Id", cascadeDelete: true);
        }
    }
}
