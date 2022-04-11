namespace BotAgentHub.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class EditBaseEntityClass1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.BotConfigurations", "CreatedAt", c => c.DateTime(precision: 0));
            AlterColumn("dbo.BotConfigurations", "UpdatedAt", c => c.DateTime(precision: 0));
        }

        public override void Down()
        {
            AlterColumn("dbo.BotConfigurations", "CreatedAt", c => c.DateTime());
            AlterColumn("dbo.BotConfigurations", "UpdatedAt", c => c.DateTime());
        }
    }
}
