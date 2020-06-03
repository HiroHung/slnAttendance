namespace prjAttendance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class insertEnum : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Students", "Gender", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Students", "Gender", c => c.String());
        }
    }
}
