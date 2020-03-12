namespace NEW_SOUQ_PROJECT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditRoleTypeInUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "FK_RoleId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "FK_RoleId");
        }
    }
}
