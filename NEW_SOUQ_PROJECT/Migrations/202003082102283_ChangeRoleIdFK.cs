namespace NEW_SOUQ_PROJECT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeRoleIdFK : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "RoleId", c => c.String(maxLength: 128));
            CreateIndex("dbo.AspNetUsers", "RoleId");
            AddForeignKey("dbo.AspNetUsers", "RoleId", "dbo.AspNetRoles", "Id");
            DropColumn("dbo.AspNetUsers", "RoleName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "RoleName", c => c.String());
            DropForeignKey("dbo.AspNetUsers", "RoleId", "dbo.AspNetRoles");
            DropIndex("dbo.AspNetUsers", new[] { "RoleId" });
            DropColumn("dbo.AspNetUsers", "RoleId");
        }
    }
}
