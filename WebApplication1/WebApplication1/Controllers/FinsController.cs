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
            var tagArray = tags.Split(',');

            return View(await _context.Fins.ToListAsync());
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
            byte[] data = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(email));

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

        // GET: Fins/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Fins/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Office,Steam,Battle,League,Xbox,Psn,Nintendo")] Fin fin)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fin);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(fin);
        }

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
