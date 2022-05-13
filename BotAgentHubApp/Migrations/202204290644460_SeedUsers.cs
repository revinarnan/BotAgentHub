namespace BotAgentHubApp.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'14c1c124-ef18-4e8e-8189-8696019a7e87', N'revinarnan@mail.ugm.ac.id', 0, N'AM+py/HEFzyexvm3rGVVXYkj5WCUXc4wnKv+WwPOqWfWpxyXTNtlGBA0QABrHTYNcQ==', N'968bb295-2a6f-4b43-ada4-afab84eaae7f', NULL, 0, 0, NULL, 1, 0, N'revinarnan@mail.ugm.ac.id')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'cec3d2da-bed4-4ac4-b83e-eea80d3c175e', N'admin@botagenthub.com', 0, N'AIUa9VfIKcmXdAa/Ntf3HDIiTQITA2zCdQWssvg+lm7o3kxhgX3ha/QDJO+vIYVsdA==', N'e97c422f-c307-4ba4-8c1a-21b8d9faea30', NULL, 0, 0, NULL, 1, 0, N'admin@botagenthub.com')
INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'3c663acb-1a24-46b4-ac37-21b6ccc0acd6', N'SuperAdmin')
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'cec3d2da-bed4-4ac4-b83e-eea80d3c175e', N'3c663acb-1a24-46b4-ac37-21b6ccc0acd6')
");
        }

        public override void Down()
        {
        }
    }
}
