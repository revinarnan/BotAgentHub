namespace BotAgentHubApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChatHistoryFileNameasprimarykey : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.ChatHistories");
            AlterColumn("dbo.ChatHistories", "UserId", c => c.String());
            AlterColumn("dbo.ChatHistories", "ChatHistoryFileName", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.ChatHistories", "ChatHistoryFileName");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.ChatHistories");
            AlterColumn("dbo.ChatHistories", "ChatHistoryFileName", c => c.String());
            AlterColumn("dbo.ChatHistories", "UserId", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.ChatHistories", "UserId");
        }
    }
}
