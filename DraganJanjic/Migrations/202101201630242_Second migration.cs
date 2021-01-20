namespace DraganJanjic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Secondmigration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Zaposlenis", "GodinaRodjenja", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Zaposlenis", "GodinaRodjenja", c => c.Int(nullable: false));
        }
    }
}
