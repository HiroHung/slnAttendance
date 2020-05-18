namespace prjAttendance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editTimetableFK : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Timetables", "Teacher_Id", "dbo.Teachers");
            DropIndex("dbo.Timetables", new[] { "Teacher_Id" });
            RenameColumn(table: "dbo.Timetables", name: "Teacher_Id", newName: "TeacherId");
            AlterColumn("dbo.Timetables", "TeacherId", c => c.Int(nullable: false));
            CreateIndex("dbo.Timetables", "TeacherId");
            AddForeignKey("dbo.Timetables", "TeacherId", "dbo.Teachers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Timetables", "TeacherId", "dbo.Teachers");
            DropIndex("dbo.Timetables", new[] { "TeacherId" });
            AlterColumn("dbo.Timetables", "TeacherId", c => c.Int());
            RenameColumn(table: "dbo.Timetables", name: "TeacherId", newName: "Teacher_Id");
            CreateIndex("dbo.Timetables", "Teacher_Id");
            AddForeignKey("dbo.Timetables", "Teacher_Id", "dbo.Teachers", "Id");
        }
    }
}
