namespace BotAgentHub.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveImageValProp : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.BotConfigurations", "Message");
            DropColumn("dbo.BotConfigurations", "IsValid");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BotConfigurations", "IsValid", c => c.Boolean(nullable: false));
            AddColumn("dbo.BotConfigurations", "Message", c => c.String());
        }
    }
}
