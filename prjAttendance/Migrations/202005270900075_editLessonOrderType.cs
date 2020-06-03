namespace prjAttendance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editLessonOrderType : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Records", "LessonOrder", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Records", "LessonOrder", c => c.Int(nullable: false));
        }
    }
}
