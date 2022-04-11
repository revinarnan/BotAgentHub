namespace BotAgentHub.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditCreatedAt : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.BotConfigurations", "CreatedAt", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.BotConfigurations", "CreatedAt", c => c.DateTime());
        }
    }
}
