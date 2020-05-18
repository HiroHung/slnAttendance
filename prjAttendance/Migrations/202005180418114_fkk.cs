namespace prjAttendance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fkk : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Classes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TeacherID = c.Int(nullable: false),
                        ClassName = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teachers", t => t.TeacherID, cascadeDelete: true)
                .Index(t => t.TeacherID);
            
            CreateTable(
                "dbo.Teachers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IDcardNumber = c.String(),
                        Email = c.String(),
                        BirthDate = c.DateTime(nullable: false),
                        TutorPermission = c.Int(nullable: false),
                        AdministrationPermission = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        StudentID = c.String(),
                        TeacherID = c.Int(nullable: false),
                        Class = c.String(),
                        BirthDate = c.DateTime(nullable: false),
                        Address = c.String(),
                        IDcardNumber = c.String(),
                        Gender = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teachers", t => t.TeacherID, cascadeDelete: true)
                .Index(t => t.TeacherID);
            
            CreateTable(
                "dbo.Timetables",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClassID = c.Int(nullable: false),
                        Week = c.Int(nullable: false),
                        Subject = c.String(),
                        LessonOrder = c.Int(nullable: false),
                        StudentID = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Classes", t => t.ClassID, cascadeDelete: true)
                .Index(t => t.ClassID);
            
            CreateTable(
                "dbo.Records",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RollCallTime = c.DateTime(nullable: false),
                        LessonDate = c.DateTime(nullable: false),
                        LessonOrder = c.Int(nullable: false),
                        StudentName = c.String(),
                        Attendance = c.String(),
                        RollCallTeacher = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Timetables", "ClassID", "dbo.Classes");
            DropForeignKey("dbo.Students", "TeacherID", "dbo.Teachers");
            DropForeignKey("dbo.Classes", "TeacherID", "dbo.Teachers");
            DropIndex("dbo.Timetables", new[] { "ClassID" });
            DropIndex("dbo.Students", new[] { "TeacherID" });
            DropIndex("dbo.Classes", new[] { "TeacherID" });
            DropTable("dbo.Records");
            DropTable("dbo.Timetables");
            DropTable("dbo.Students");
            DropTable("dbo.Teachers");
            DropTable("dbo.Classes");
        }
    }
}
