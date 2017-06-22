using FindYourPod.Models;
using System;
using System.Linq;

namespace FindYourPod.Data
{
    public static class DbInitializer
    {
        public static void Initialize(PodContext context)
        {
            context.Database.EnsureCreated();

            // Look for any fins.
            if (context.Fins.Any())
            {
                return;   // DB has been seeded
            }

            var fins = new Fin[]
            {
            new Fin{Name="Kristen Barrett", Email="kristennbarrett@gmail.com", Office="Houston", Gamernames=new[]{new Gamername{Platform="Battle",Username="luescense#1366" } } },
            new Fin { Name = "Tyler Hartwig", Email = "Blah.com", Office = "Dallas", Gamernames = new[] { new Gamername { Platform = "Xbox", Username="Something" } } },
            new Fin { Name = "Carlos C.", Email = "Boop.com", Office = "Dallas", Gamernames = new[]{new Gamername { Platform = "Battle", Username = "Someboop"} }}
            };
        
            foreach (Fin f in fins)
            {
                context.Fins.Add(f);
            }
            context.SaveChanges();

        }
    }
}