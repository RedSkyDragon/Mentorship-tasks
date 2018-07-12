namespace IncomeAndExpenses.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedStandartAttrForTypes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ExpenseTypes", "IsStandart", c => c.Boolean(nullable: false));
            AddColumn("dbo.IncomeTypes", "IsStandart", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.IncomeTypes", "IsStandart");
            DropColumn("dbo.ExpenseTypes", "IsStandart");
        }
    }
}
