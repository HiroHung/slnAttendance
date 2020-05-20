namespace prjAttendance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RecordInsertWeek : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Records", "Week", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Records", "Week");
        }
    }
}
