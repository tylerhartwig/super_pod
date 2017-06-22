﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FindYourPod.Models;

namespace FindYourPod.ViewModels
{
    public class FinViewModel
    {
        public FinViewModel(Fin fin)
        {
            this.Name = fin.Name;
            this.Office = fin.Office;
            this.Email = fin.Email;
            this.Gamernames = fin.Gamernames;
        }
        public string Name { get; set; }
        public string Office { get; set; }
        public string Email { get; set; }
        public IEnumerable<Gamername> Gamernames { get; set; }
        public string GravitarHash { get; set; }
    }
}
