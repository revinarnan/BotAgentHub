namespace BotAgentHubApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteNotification : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ChatbotConfigurations", "UserId");
            DropTable("dbo.Notifications");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Message = c.String(),
                        IsRead = c.Boolean(nullable: false),
                        ChatHistoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.ChatbotConfigurations", "UserId", c => c.Int(nullable: false));
        }
    }
}
