namespace prjAttendance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StudentGuardianPhoneNumber : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "Guardian", c => c.String());
            AddColumn("dbo.Students", "PhoneNumber", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Students", "PhoneNumber");
            DropColumn("dbo.Students", "Guardian");
        }
    }
}
