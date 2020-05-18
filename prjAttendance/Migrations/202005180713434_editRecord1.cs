namespace prjAttendance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editRecord1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Records", "RollCallTeacherId", c => c.Int(nullable: false));
            AlterColumn("dbo.Records", "ClassName", c => c.Int(nullable: false));
            DropColumn("dbo.Records", "StudentName");
            DropColumn("dbo.Records", "RollCallTeacher");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Records", "RollCallTeacher", c => c.String());
            AddColumn("dbo.Records", "StudentName", c => c.String());
            AlterColumn("dbo.Records", "ClassName", c => c.String());
            DropColumn("dbo.Records", "RollCallTeacherId");
        }
    }
}
