namespace EventManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class version8 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Events", "StartTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Events", "EndTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.Events", "Date");
            DropColumn("dbo.Events", "StartTime1");
            DropColumn("dbo.Events", "EndTime1");
            DropColumn("dbo.VisitorRegistrations", "DOB");
        }
        
        public override void Down()
        {
            AddColumn("dbo.VisitorRegistrations", "DOB", c => c.String());
            AddColumn("dbo.Events", "EndTime1", c => c.Double(nullable: false));
            AddColumn("dbo.Events", "StartTime1", c => c.Double(nullable: false));
            AddColumn("dbo.Events", "Date", c => c.String());
            AlterColumn("dbo.Events", "EndTime", c => c.String());
            AlterColumn("dbo.Events", "StartTime", c => c.String());
        }
    }
}
