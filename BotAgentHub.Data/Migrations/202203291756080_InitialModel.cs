namespace BotAgentHub.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ChatHistories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FileName = c.String(),
                        BotConfigurationId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Message = c.String(),
                        IsRead = c.Boolean(nullable: false),
                        RoleType = c.Int(nullable: false),
                        ChatHistoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.BotConfigurations", "BotImageName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.BotConfigurations", "BotImageName");
            DropTable("dbo.Notifications");
            DropTable("dbo.ChatHistories");
        }
    }
}
