namespace BikeDekho_CRUD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init170420 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bikes", "BikePrice", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Bikes", "BikePrice");
        }
    }
}
