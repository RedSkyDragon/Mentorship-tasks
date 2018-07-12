namespace IncomeAndExpenses.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedRequiredAtributes : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.ExpenseTypes", new[] { "User_Id" });
            DropIndex("dbo.IncomeTypes", new[] { "User_Id" });
            DropColumn("dbo.ExpenseTypes", "UserId");
            DropColumn("dbo.IncomeTypes", "UserId");
            RenameColumn(table: "dbo.ExpenseTypes", name: "User_Id", newName: "UserId");
            RenameColumn(table: "dbo.IncomeTypes", name: "User_Id", newName: "UserId");
            AlterColumn("dbo.ExpenseTypes", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.ExpenseTypes", "UserId", c => c.String(maxLength: 128));
            AlterColumn("dbo.IncomeTypes", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.IncomeTypes", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.ExpenseTypes", "UserId");
            CreateIndex("dbo.IncomeTypes", "UserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.IncomeTypes", new[] { "UserId" });
            DropIndex("dbo.ExpenseTypes", new[] { "UserId" });
            AlterColumn("dbo.IncomeTypes", "UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.IncomeTypes", "Name", c => c.String());
            AlterColumn("dbo.ExpenseTypes", "UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.ExpenseTypes", "Name", c => c.String());
            RenameColumn(table: "dbo.IncomeTypes", name: "UserId", newName: "User_Id");
            RenameColumn(table: "dbo.ExpenseTypes", name: "UserId", newName: "User_Id");
            AddColumn("dbo.IncomeTypes", "UserId", c => c.Int(nullable: false));
            AddColumn("dbo.ExpenseTypes", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.IncomeTypes", "User_Id");
            CreateIndex("dbo.ExpenseTypes", "User_Id");
        }
    }
}
