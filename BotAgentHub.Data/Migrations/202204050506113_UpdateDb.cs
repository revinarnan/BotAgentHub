namespace BotAgentHub.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class UpdateDb : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.BotConfigurations", "CreatedAt", c => c.String());
            AlterColumn("dbo.BotConfigurations", "UpdatedAt", c => c.String());
        }

        public override void Down()
        {
            AlterColumn("dbo.BotConfigurations", "UpdatedAt", c => c.DateTime());
            AlterColumn("dbo.BotConfigurations", "CreatedAt", c => c.String(nullable: false));
        }
    }
}
