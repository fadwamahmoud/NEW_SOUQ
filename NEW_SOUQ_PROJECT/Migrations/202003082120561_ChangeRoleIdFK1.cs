namespace NEW_SOUQ_PROJECT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeRoleIdFK1 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.AspNetUsers", name: "RoleName", newName: "RoleId");
            RenameIndex(table: "dbo.AspNetUsers", name: "IX_RoleName", newName: "IX_RoleId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.AspNetUsers", name: "IX_RoleId", newName: "IX_RoleName");
            RenameColumn(table: "dbo.AspNetUsers", name: "RoleId", newName: "RoleName");
        }
    }
}
