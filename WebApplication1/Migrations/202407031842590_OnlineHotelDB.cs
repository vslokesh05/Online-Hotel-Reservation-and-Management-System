namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OnlineHotelDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccountLogins",
                c => new
                    {
                        LoginId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Password = c.String(),
                        Admin_AdminId = c.Int(),
                        User_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.LoginId)
                .ForeignKey("dbo.Admins", t => t.Admin_AdminId)
                .ForeignKey("dbo.Users", t => t.User_UserId)
                .Index(t => t.Admin_AdminId)
                .Index(t => t.User_UserId);
            
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        AdminId = c.Int(nullable: false, identity: true),
                        AName = c.String(),
                        AUserName = c.String(nullable: false, maxLength: 255),
                        APassword = c.String(nullable: false, maxLength: 255),
                        APhoneNo = c.String(nullable: false, maxLength: 10),
                        AEmailId = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.AdminId);
            
            CreateTable(
                "dbo.RoomInfoes",
                c => new
                    {
                        RoomId = c.Int(nullable: false, identity: true),
                        RoomType = c.String(),
                        Size = c.Int(nullable: false),
                        Description = c.String(),
                        RoomNo = c.Int(nullable: false),
                        Availability = c.Boolean(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AdminId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RoomId)
                .ForeignKey("dbo.Admins", t => t.AdminId, cascadeDelete: true)
                .Index(t => t.AdminId);
            
            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        ReservationId = c.Int(nullable: false, identity: true),
                        RoomId = c.Int(nullable: false),
                        NoOfPerson = c.Int(nullable: false),
                        CheckInDate = c.DateTime(nullable: false),
                        CheckOutDate = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ReservationId)
                .ForeignKey("dbo.RoomInfoes", t => t.RoomId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.RoomId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        PaymentId = c.Int(nullable: false, identity: true),
                        ReservationId = c.Int(nullable: false),
                        TotalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PaymentMethod = c.String(),
                        TimeStamp = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PaymentId)
                .ForeignKey("dbo.Reservations", t => t.ReservationId, cascadeDelete: true)
                .Index(t => t.ReservationId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        PhoneNo = c.String(nullable: false, maxLength: 10),
                        EmailId = c.String(nullable: false),
                        Username = c.String(nullable: false, maxLength: 255),
                        Password = c.String(nullable: false, maxLength: 255),
                        AadharNo = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservations", "UserId", "dbo.Users");
            DropForeignKey("dbo.AccountLogins", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.Reservations", "RoomId", "dbo.RoomInfoes");
            DropForeignKey("dbo.Payments", "ReservationId", "dbo.Reservations");
            DropForeignKey("dbo.RoomInfoes", "AdminId", "dbo.Admins");
            DropForeignKey("dbo.AccountLogins", "Admin_AdminId", "dbo.Admins");
            DropIndex("dbo.Payments", new[] { "ReservationId" });
            DropIndex("dbo.Reservations", new[] { "UserId" });
            DropIndex("dbo.Reservations", new[] { "RoomId" });
            DropIndex("dbo.RoomInfoes", new[] { "AdminId" });
            DropIndex("dbo.AccountLogins", new[] { "User_UserId" });
            DropIndex("dbo.AccountLogins", new[] { "Admin_AdminId" });
            DropTable("dbo.Users");
            DropTable("dbo.Payments");
            DropTable("dbo.Reservations");
            DropTable("dbo.RoomInfoes");
            DropTable("dbo.Admins");
            DropTable("dbo.AccountLogins");
        }
    }
}
