namespace IncomeAndExpenses.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModelCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Expenses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Date = c.DateTime(nullable: false),
                        Comment = c.String(),
                        ExpenseTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ExpenseTypes", t => t.ExpenseTypeId, cascadeDelete: true)
                .Index(t => t.ExpenseTypeId);
            
            CreateTable(
                "dbo.ExpenseTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        UserId = c.Int(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Incomes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Date = c.DateTime(nullable: false),
                        Comment = c.String(),
                        IncomeTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.IncomeTypes", t => t.IncomeTypeId, cascadeDelete: true)
                .Index(t => t.IncomeTypeId);
            
            CreateTable(
                "dbo.IncomeTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        UserId = c.Int(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IncomeTypes", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Incomes", "IncomeTypeId", "dbo.IncomeTypes");
            DropForeignKey("dbo.ExpenseTypes", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Expenses", "ExpenseTypeId", "dbo.ExpenseTypes");
            DropIndex("dbo.IncomeTypes", new[] { "User_Id" });
            DropIndex("dbo.Incomes", new[] { "IncomeTypeId" });
            DropIndex("dbo.ExpenseTypes", new[] { "User_Id" });
            DropIndex("dbo.Expenses", new[] { "ExpenseTypeId" });
            DropTable("dbo.IncomeTypes");
            DropTable("dbo.Incomes");
            DropTable("dbo.ExpenseTypes");
            DropTable("dbo.Expenses");
        }
    }
}
