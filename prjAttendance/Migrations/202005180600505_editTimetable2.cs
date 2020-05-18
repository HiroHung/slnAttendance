namespace prjAttendance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editTimetable2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Timetables", "Class_Id", "dbo.Classes");
            DropIndex("dbo.Timetables", new[] { "Class_Id" });
            DropColumn("dbo.Timetables", "Class_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Timetables", "Class_Id", c => c.Int());
            CreateIndex("dbo.Timetables", "Class_Id");
            AddForeignKey("dbo.Timetables", "Class_Id", "dbo.Classes", "Id");
        }
    }
}
