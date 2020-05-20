namespace prjAttendance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class insertPermissioneditTeacher : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Permissions",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Subject = c.String(),
                        pvalue = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            AddColumn("dbo.Teachers", "Permission", c => c.String());
            DropColumn("dbo.Teachers", "TutorPermission");
            DropColumn("dbo.Teachers", "AdministrationPermission");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Teachers", "AdministrationPermission", c => c.Int(nullable: false));
            AddColumn("dbo.Teachers", "TutorPermission", c => c.Int(nullable: false));
            DropColumn("dbo.Teachers", "Permission");
            DropTable("dbo.Permissions");
        }
    }
}
