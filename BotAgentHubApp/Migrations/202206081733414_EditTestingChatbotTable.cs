namespace BotAgentHubApp.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class EditTestingChatbotTable : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.TestingChabot");
            CreateTable(
                "dbo.TestingChabot",
                c => new
                {
                    Id = c.String(nullable: true),
                    Data = c.String(nullable: true)
                });
        }

        public override void Down()
        {
        }
    }
}
