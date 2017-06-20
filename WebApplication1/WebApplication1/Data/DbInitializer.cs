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
            new Fin{Name="Kristen Barrett",Office="Houston",Steam="lunainpurple",Battle="luescense#1366",League="NONE",Xbox="NONE",Psn="luescense",Nintendo="lolidk" },
            new Fin{Name="Carlos Cardin",Office="Dallas",Steam="ccardin9",Battle="NONE",League="NONE",Xbox="NONE",Psn="about-9-otters",Nintendo="lolidk" },
            };
            foreach (Fin f in fins)
            {
                context.Fins.Add(f);
            }
            context.SaveChanges();

        }
    }
}