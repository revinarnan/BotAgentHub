namespace BotAgentHubApp.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class CreateTestingTableForBot : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                    "dbo.TestingChabot",
                    c => new
                    {
                        UsrId = c.Int(nullable: false, identity: true),
                        UserState = c.String(nullable: true, maxLength: 255),
                        ConversationState = c.String(nullable: true)
                    })
                .PrimaryKey(t => t.UsrId);
        }

        public override void Down()
        {
        }
    }
}
