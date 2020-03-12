namespace NEW_SOUQ_PROJECT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IDTOString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Requests", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Requests", "UserId", c => c.Int(nullable: false));
        }
    }
}
