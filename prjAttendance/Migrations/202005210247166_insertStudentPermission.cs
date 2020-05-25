namespace prjAttendance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class insertStudentPermission : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "Permission", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Students", "Permission");
        }
    }
}
