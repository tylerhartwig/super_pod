using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FindYourPod.Data;
using FindYourPod.Models;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using FindYourPod.ViewModels;

namespace FindYourPod.Controllers
{
    public class FinsController : Controller
    {
        private readonly PodContext _context;

        public FinsController(PodContext context)
        {
            _context = context;
        }


        // GET: Fins
        public async Task<IActionResult> Index(string tags)
        {
            IEnumerable<Fin> list;
            if (!string.IsNullOrEmpty(tags))
            {
                var tagArray = tags.Split(',').ToList();
                list = _context.Fins.Include(f => f.Gamernames).AsEnumerable().Where(f => f.Gamernames?.Any(g => tagArray.Contains(g.Platform)) == true);
            }
            else
            {
                list = _context.Fins.Include(f => f.Gamernames).ToList();
            }
            var viewmodels = list.Select(f => new FinViewModel(f)
            {
                GravitarHash = HashEmailForGravatar(f.Email)
            });
            var test = viewmodels.ToList();
            return View(viewmodels);
        }

        // GET: Fins/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fin = await _context.Fins
                .SingleOrDefaultAsync(m => m.ID == id);
            if (fin == null)
            {
                return NotFound();
            }

            return View(fin);
        }

        public static string HashEmailForGravatar(string email)
        {
            // Create a new instance of the MD5CryptoServiceProvider object.  
            MD5 md5Hasher = MD5.Create();

            // Convert the input string to a byte array and compute the hash.  
            byte[] data = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(email.ToLower().Trim()));

            // Create a new Stringbuilder to collect the bytes  
            // and create a string.  
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data  
            // and format each one as a hexadecimal string.  
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();  // Return the hexadecimal string. 
        }

        private void addToGamerList(List<Gamername> list, string platform, string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                list.Add(new Gamername { Platform = platform, Username = name });
            }
        }
        // GET: Fins/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string name, string email, string office,
            string psn, string xbox, string nintendo, string steam, string league,
            string origin, string discord, string battle, string twitch, string about_me,
            string favorite_games)
        {
            var gamernames = new List<Gamername>();
            addToGamerList(gamernames, nameof(psn), psn);
            addToGamerList(gamernames, nameof(xbox), xbox);
            addToGamerList(gamernames, nameof(nintendo), nintendo);
            addToGamerList(gamernames, nameof(steam), steam);
            addToGamerList(gamernames, nameof(league), league);
            addToGamerList(gamernames, nameof(origin), origin);
            addToGamerList(gamernames, nameof(discord), discord);
            addToGamerList(gamernames, nameof(battle), battle);
            addToGamerList(gamernames, nameof(twitch), twitch);

            var newFin = new Fin
            {
                Name = name,
                Email = email,
                Office = office,
                AboutMe = about_me,
                FavoriteGames = favorite_games,
                Gamernames = gamernames

            };

            _context.Add(newFin);
            await _context.SaveChangesAsync();
            return View();
        }

        // POST: Fins/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("ID,Name,Office,Steam,Battle,League,Xbox,Psn,Nintendo")] Fin fin)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(fin);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }
        //    return View(fin);
        //}

        // GET: Fins/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fin = await _context.Fins.SingleOrDefaultAsync(m => m.ID == id);
            if (fin == null)
            {
                return NotFound();
            }
            return View(fin);
        }

        // POST: Fins/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Office,Steam,Battle,League,Xbox,Psn,Nintendo")] Fin fin)
        {
            if (id != fin.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fin);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FinExists(fin.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(fin);
        }

        // GET: Fins/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fin = await _context.Fins
                .SingleOrDefaultAsync(m => m.ID == id);
            if (fin == null)
            {
                return NotFound();
            }

            return View(fin);
        }

        // POST: Fins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fin = await _context.Fins.SingleOrDefaultAsync(m => m.ID == id);
            _context.Fins.Remove(fin);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool FinExists(int id)
        {
            return _context.Fins.Any(e => e.ID == id);
        }
    }
}
