namespace prjAttendance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editTimetableClassFK : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Classes", "TeacherId", "dbo.Teachers");
            DropForeignKey("dbo.Timetables", "TeacherId", "dbo.Teachers");
            DropIndex("dbo.Classes", new[] { "TeacherId" });
            DropIndex("dbo.Timetables", new[] { "TeacherId" });
            RenameColumn(table: "dbo.Timetables", name: "TeacherId", newName: "Teacher_Id");
            AddColumn("dbo.Timetables", "ClassId", c => c.Int(nullable: false));
            AlterColumn("dbo.Timetables", "Teacher_Id", c => c.Int());
            CreateIndex("dbo.Timetables", "ClassId");
            CreateIndex("dbo.Timetables", "Teacher_Id");
            AddForeignKey("dbo.Timetables", "ClassId", "dbo.Classes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Timetables", "Teacher_Id", "dbo.Teachers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Timetables", "Teacher_Id", "dbo.Teachers");
            DropForeignKey("dbo.Timetables", "ClassId", "dbo.Classes");
            DropIndex("dbo.Timetables", new[] { "Teacher_Id" });
            DropIndex("dbo.Timetables", new[] { "ClassId" });
            AlterColumn("dbo.Timetables", "Teacher_Id", c => c.Int(nullable: false));
            DropColumn("dbo.Timetables", "ClassId");
            RenameColumn(table: "dbo.Timetables", name: "Teacher_Id", newName: "TeacherId");
            CreateIndex("dbo.Timetables", "TeacherId");
            CreateIndex("dbo.Classes", "TeacherId");
            AddForeignKey("dbo.Timetables", "TeacherId", "dbo.Teachers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Classes", "TeacherId", "dbo.Teachers", "Id", cascadeDelete: true);
        }
    }
}
