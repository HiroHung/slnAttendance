namespace prjAttendance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ContactrecordInsetMethod : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contactrecords", "Method", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Contactrecords", "Method");
        }
    }
}
