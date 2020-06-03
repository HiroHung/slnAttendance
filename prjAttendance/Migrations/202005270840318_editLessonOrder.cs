namespace prjAttendance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editLessonOrder : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Timetables", "LessonOrder", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Timetables", "LessonOrder", c => c.Int(nullable: false));
        }
    }
}
