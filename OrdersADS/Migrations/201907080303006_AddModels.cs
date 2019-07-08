namespace OrdersADS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Details",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Articul = c.String(),
                        Order_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.Order_Id)
                .Index(t => t.Order_Id);
            
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DetailId = c.Int(nullable: false),
                        OrderId = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Details", t => t.DetailId, cascadeDelete: true)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.DetailId)
                .Index(t => t.OrderId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        RequestId = c.Int(nullable: false),
                        dateTime = c.DateTime(nullable: false),
                        ProviderId = c.Int(nullable: false),
                        StatusOrderId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Providers", t => t.ProviderId, cascadeDelete: true)
                .ForeignKey("dbo.Requests", t => t.RequestId, cascadeDelete: true)
                .ForeignKey("dbo.StatusOrders", t => t.StatusOrderId, cascadeDelete: true)
                .Index(t => t.RequestId)
                .Index(t => t.ProviderId)
                .Index(t => t.StatusOrderId);
            
            CreateTable(
                "dbo.Providers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        Phone = c.String(),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Requests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        StatusId = c.Int(nullable: false),
                        ZakazchikId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Status", t => t.StatusId, cascadeDelete: true)
                .ForeignKey("dbo.Zakazchiks", t => t.ZakazchikId, cascadeDelete: true)
                .Index(t => t.StatusId)
                .Index(t => t.ZakazchikId);
            
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StatusName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Zakazchiks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        Address = c.String(),
                        Phone = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StatusOrders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderDetails", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Orders", "StatusOrderId", "dbo.StatusOrders");
            DropForeignKey("dbo.Orders", "RequestId", "dbo.Requests");
            DropForeignKey("dbo.Requests", "ZakazchikId", "dbo.Zakazchiks");
            DropForeignKey("dbo.Requests", "StatusId", "dbo.Status");
            DropForeignKey("dbo.Orders", "ProviderId", "dbo.Providers");
            DropForeignKey("dbo.Details", "Order_Id", "dbo.Orders");
            DropForeignKey("dbo.OrderDetails", "DetailId", "dbo.Details");
            DropIndex("dbo.Requests", new[] { "ZakazchikId" });
            DropIndex("dbo.Requests", new[] { "StatusId" });
            DropIndex("dbo.Orders", new[] { "StatusOrderId" });
            DropIndex("dbo.Orders", new[] { "ProviderId" });
            DropIndex("dbo.Orders", new[] { "RequestId" });
            DropIndex("dbo.OrderDetails", new[] { "OrderId" });
            DropIndex("dbo.OrderDetails", new[] { "DetailId" });
            DropIndex("dbo.Details", new[] { "Order_Id" });
            DropTable("dbo.StatusOrders");
            DropTable("dbo.Zakazchiks");
            DropTable("dbo.Status");
            DropTable("dbo.Requests");
            DropTable("dbo.Providers");
            DropTable("dbo.Orders");
            DropTable("dbo.OrderDetails");
            DropTable("dbo.Details");
        }
    }
}
