namespace prjAttendance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Records", "ClassId", c => c.Int(nullable: false));
            AlterColumn("dbo.Classes", "ClassName", c => c.Int(nullable: false));
            DropColumn("dbo.Students", "ClassName");
            DropColumn("dbo.Timetables", "ClassName");
            DropColumn("dbo.Records", "ClassName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Records", "ClassName", c => c.Int(nullable: false));
            AddColumn("dbo.Timetables", "ClassName", c => c.String());
            AddColumn("dbo.Students", "ClassName", c => c.String());
            AlterColumn("dbo.Classes", "ClassName", c => c.String());
            DropColumn("dbo.Records", "ClassId");
        }
    }
}
