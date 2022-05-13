namespace BotAgentHubApp.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class SeedUserKaprodi : DbMigration
    {
        public override void Up()
        {
            Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'0daa9579-6aed-4648-b4cc-cfcbea643bbc', N'kaprodi1@botagenthub.com', 0, N'AOtDNlxOm/g9BpLyvK4PDJjBYq5Z6WaI7CofGkCLa+6nW9HipiLjzaqvvEUGcL2fLg==', N'e3f24d90-a82b-4d33-b504-0182f8690b66', NULL, 0, 0, NULL, 1, 0, N'kaprodi1@botagenthub.com')
INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'3811da12-c0af-423e-aeab-78c5a6cec7d4', N'Kaprodi')
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'0daa9579-6aed-4648-b4cc-cfcbea643bbc', N'3811da12-c0af-423e-aeab-78c5a6cec7d4')
");
        }

        public override void Down()
        {
        }
    }
}
