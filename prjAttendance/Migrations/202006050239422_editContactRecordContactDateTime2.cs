namespace prjAttendance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editContactRecordContactDateTime2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Contactrecords", "ContactDateTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Contactrecords", "ContactDateTime", c => c.DateTime(nullable: false));
        }
    }
}
