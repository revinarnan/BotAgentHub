namespace BotAgentHubApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changeprimarykeychathistorytable : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.ChatHistories");
            AlterColumn("dbo.ChatHistories", "UserId", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.ChatHistories", "UserId");
            DropColumn("dbo.ChatHistories", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ChatHistories", "Id", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.ChatHistories");
            AlterColumn("dbo.ChatHistories", "UserId", c => c.String());
            AddPrimaryKey("dbo.ChatHistories", "Id");
        }
    }
}
