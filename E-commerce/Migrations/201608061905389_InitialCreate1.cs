namespace E_commerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Product", "ThumbUrl", c => c.String(maxLength: 100, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Product", "ThumbUrl", c => c.String(nullable: false, maxLength: 100, unicode: false));
        }
    }
}
