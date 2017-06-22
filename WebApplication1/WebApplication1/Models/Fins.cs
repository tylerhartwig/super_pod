using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FindYourPod.Models
{
    public class Fin
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Office { get; set; }
        public string Email { get; set; }
        public IEnumerable<Gamername> Gamernames { get; set; }
    }
}
