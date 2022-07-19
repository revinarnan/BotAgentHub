namespace BotAgentHubApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddChatBotEmailQUestionstable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ChatBotEmailQuestions",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(),
                        Question = c.String(),
                        IsAnswered = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ChatBotEmailQuestions");
        }
    }
}
