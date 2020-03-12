namespace NEW_SOUQ_PROJECT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeRoleNameFK : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.AspNetUsers", name: "RoleId", newName: "RoleName");
            RenameIndex(table: "dbo.AspNetUsers", name: "IX_RoleId", newName: "IX_RoleName");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.AspNetUsers", name: "IX_RoleName", newName: "IX_RoleId");
            RenameColumn(table: "dbo.AspNetUsers", name: "RoleName", newName: "RoleId");
        }
    }
}
