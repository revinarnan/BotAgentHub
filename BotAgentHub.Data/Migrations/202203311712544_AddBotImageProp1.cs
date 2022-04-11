namespace BotAgentHub.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddBotImageProp1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BotConfigurations", "BotImageName", c => c.String());
            DropColumn("dbo.BotConfigurations", "BotImagePath");
        }

        public override void Down()
        {
            AddColumn("dbo.BotConfigurations", "BotImagePath", c => c.String());
            DropColumn("dbo.BotConfigurations", "BotImageName");
        }
    }
}
