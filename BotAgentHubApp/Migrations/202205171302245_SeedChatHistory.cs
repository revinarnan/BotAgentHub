namespace BotAgentHubApp.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class SeedChatHistory : DbMigration
    {
        public override void Up()
        {
            Sql("SET IDENTITY_INSERT [dbo].[ChatHistories] ON " +
                "INSERT INTO [dbo].[ChatHistories] ([Id], [UserId], [IsDoneOnBot], [IsDoneOnLiveChat], [IsDoneOnEmail], [ChatHistoryFileName]) " +
                "VALUES (1, NULL, 1, 0, 0, N'Contoh.pdf')" +
                "INSERT INTO [dbo].[ChatHistories] ([Id], [UserId], [IsDoneOnBot], [IsDoneOnLiveChat], [IsDoneOnEmail], [ChatHistoryFileName]) " +
                "VALUES (2, N'staffadmin1@botagenthub.com', 0, 1, 0, N'botA.jpg')" +
                "SET IDENTITY_INSERT [dbo].[ChatHistories] OFF");
        }

        public override void Down()
        {
        }
    }
}
