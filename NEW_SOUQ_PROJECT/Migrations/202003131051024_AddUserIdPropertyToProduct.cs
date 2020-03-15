namespace NEW_SOUQ_PROJECT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserIdPropertyToProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "UserId");
        }
    }
}
