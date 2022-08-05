namespace BotAgentHubApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_chatbotconfiguration_field : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ChatbotConfigurations", "UrlClient", c => c.String());
            AlterColumn("dbo.ChatbotConfigurations", "UrlKb", c => c.String());
            DropColumn("dbo.ChatbotConfigurations", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ChatbotConfigurations", "Name", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.ChatbotConfigurations", "UrlKb", c => c.String(nullable: false));
            AlterColumn("dbo.ChatbotConfigurations", "UrlClient", c => c.String(nullable: false));
        }
    }
}
