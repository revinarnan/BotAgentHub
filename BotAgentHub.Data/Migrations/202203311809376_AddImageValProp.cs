namespace BotAgentHub.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImageValProp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BotConfigurations", "Message", c => c.String());
            AddColumn("dbo.BotConfigurations", "IsValid", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BotConfigurations", "IsValid");
            DropColumn("dbo.BotConfigurations", "Message");
        }
    }
}
