namespace BotAgentHub.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class EditCreatedAtProp : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.BotConfigurations", "UpdatedAt", c => c.String());
        }

        public override void Down()
        {
            AlterColumn("dbo.BotConfigurations", "UpdatedAt", c => c.DateTime());
        }
    }
}
