namespace prjAttendance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RecordInsetSubject : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Records", "Subject", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Records", "Subject");
        }
    }
}
