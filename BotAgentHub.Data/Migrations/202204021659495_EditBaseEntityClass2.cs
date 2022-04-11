namespace BotAgentHub.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class EditBaseEntityClass2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.BotConfigurations", "UpdatedAt", c => c.String(nullable: false));
        }

        public override void Down()
        {
            AlterColumn("dbo.BotConfigurations", "UpdatedAt", c => c.DateTime());
        }
    }
}
