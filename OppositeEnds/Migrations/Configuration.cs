namespace OppositeEnds.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Web.Helpers;

    internal sealed class Configuration : DbMigrationsConfiguration<OppositeEnds.Models.OppositeEndsContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(OppositeEnds.Models.OppositeEndsContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //B
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.Users.AddOrUpdate(u => u.FirstName,
                new Models.User { FirstName = "AdminFirstName", LastName = "AdminLastName", UserName = "admin", Email = "admin@gmail.com", Password = "admin", ConfirmPassword = "admin", verified = false }); }
           /* context.Florals.AddOrUpdate(x => x.Name, new Models.Floral {Name="item1",Price ="125"} )*/ 
        }
    }

//public int Id { get; set; }

//public string Name { get; set; }

//public double Price { get; set; }

//public int Quantity { get; set; }

//public string Category { get; set; }

//public string Picture { get; set; }