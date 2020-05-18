namespace prjAttendance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editRecord : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "ClassName", c => c.String());
            AddColumn("dbo.Records", "StudentID", c => c.Int(nullable: false));
            AddColumn("dbo.Records", "ClassName", c => c.String());
            CreateIndex("dbo.Records", "StudentID");
            AddForeignKey("dbo.Records", "StudentID", "dbo.Students", "Id", cascadeDelete: true);
            DropColumn("dbo.Students", "Class");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Students", "Class", c => c.String());
            DropForeignKey("dbo.Records", "StudentID", "dbo.Students");
            DropIndex("dbo.Records", new[] { "StudentID" });
            DropColumn("dbo.Records", "ClassName");
            DropColumn("dbo.Records", "StudentID");
            DropColumn("dbo.Students", "ClassName");
        }
    }
}
