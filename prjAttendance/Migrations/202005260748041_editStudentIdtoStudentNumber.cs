namespace prjAttendance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editStudentIdtoStudentNumber : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "StudentNumber", c => c.String());
            DropColumn("dbo.Students", "StudentId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Students", "StudentId", c => c.String());
            DropColumn("dbo.Students", "StudentNumber");
        }
    }
}
