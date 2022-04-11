namespace BotAgentHub.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCreatedAtAndUpdatedAt : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BotConfigurations", "CreatedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.BotConfigurations", "UpdatedAt", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BotConfigurations", "UpdatedAt");
            DropColumn("dbo.BotConfigurations", "CreatedAt");
        }
    }
}
