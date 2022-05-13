namespace BotAgentHubApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBaseEntityTiChatbotConfiguration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ChatbotConfigurations", "Date", c => c.String(maxLength: 15));
            AddColumn("dbo.ChatbotConfigurations", "Time", c => c.String(maxLength: 15));
            AddColumn("dbo.ChatbotConfigurations", "CreatedAt", c => c.String());
            AddColumn("dbo.ChatbotConfigurations", "UpdatedAt", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ChatbotConfigurations", "UpdatedAt");
            DropColumn("dbo.ChatbotConfigurations", "CreatedAt");
            DropColumn("dbo.ChatbotConfigurations", "Time");
            DropColumn("dbo.ChatbotConfigurations", "Date");
        }
    }
}
