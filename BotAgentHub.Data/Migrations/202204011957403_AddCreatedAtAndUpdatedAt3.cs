namespace BotAgentHub.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCreatedAtAndUpdatedAt3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.BotConfigurations", "CreatedAt", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.BotConfigurations", "CreatedAt", c => c.DateTime(nullable: false));
        }
    }
}
