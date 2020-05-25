namespace prjAttendance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editClassName : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Classes", "ClassName", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Classes", "ClassName", c => c.Int(nullable: false));
        }
    }
}
