namespace EventManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class version9 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Events", "CreatedBy", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Events", "CreatedBy", c => c.String());
        }
    }
}
