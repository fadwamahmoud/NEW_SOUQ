namespace NEW_SOUQ_PROJECT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRoleNameColumnInUser : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "Role_Id", "dbo.AspNetRoles");
            DropIndex("dbo.AspNetUsers", new[] { "Role_Id" });
            AddColumn("dbo.AspNetUsers", "RoleName", c => c.String());
            DropColumn("dbo.AspNetUsers", "FK_RoleId");
            DropColumn("dbo.AspNetUsers", "Role_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Role_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.AspNetUsers", "FK_RoleId", c => c.String());
            DropColumn("dbo.AspNetUsers", "RoleName");
            CreateIndex("dbo.AspNetUsers", "Role_Id");
            AddForeignKey("dbo.AspNetUsers", "Role_Id", "dbo.AspNetRoles", "Id");
        }
    }
}
