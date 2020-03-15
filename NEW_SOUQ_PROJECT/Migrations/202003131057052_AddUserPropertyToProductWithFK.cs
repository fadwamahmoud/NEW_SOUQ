namespace NEW_SOUQ_PROJECT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserPropertyToProductWithFK : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "FK_UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Products", "FK_UserId");
            AddForeignKey("dbo.Products", "FK_UserId", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.Products", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "UserId", c => c.String());
            DropForeignKey("dbo.Products", "FK_UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Products", new[] { "FK_UserId" });
            DropColumn("dbo.Products", "FK_UserId");
        }
    }
}
