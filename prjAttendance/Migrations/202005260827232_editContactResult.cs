namespace prjAttendance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editContactResult : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Contactrecords", "Results", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Contactrecords", "Results", c => c.Int(nullable: false));
        }
    }
}
