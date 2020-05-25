namespace prjAttendance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class insertContactRecord : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contactrecords",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TeacherId = c.Int(nullable: false),
                        ContactDateTime = c.DateTime(nullable: false),
                        StudentId = c.Int(nullable: false),
                        ContactGuardian = c.String(),
                        Reason = c.String(),
                        Results = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teachers", t => t.TeacherId, cascadeDelete: true)
                .Index(t => t.TeacherId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Contactrecords", "TeacherId", "dbo.Teachers");
            DropIndex("dbo.Contactrecords", new[] { "TeacherId" });
            DropTable("dbo.Contactrecords");
        }
    }
}
