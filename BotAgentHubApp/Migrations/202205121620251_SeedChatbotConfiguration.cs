namespace BotAgentHubApp.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class SeedChatbotConfiguration : DbMigration
    {
        public override void Up()
        {
            Sql("SET IDENTITY_INSERT [dbo].[ChatbotConfigurations] ON " +
                "INSERT INTO [dbo].[ChatbotConfigurations] ([Id], [Name], [UrlClient], [UrlKb]) VALUES (1, N'Website Akademik TETI', N'https://www.sarjana.jteti.ugm.ac.id', N'https://www.simaster.ugm.ac.id')" +
                "SET IDENTITY_INSERT [dbo].[ChatbotConfigurations] OFF");
        }

        public override void Down()
        {
        }
    }
}
