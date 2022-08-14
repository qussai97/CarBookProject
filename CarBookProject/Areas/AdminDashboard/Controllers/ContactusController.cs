using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarBookProject.Areas.AdminDashboard.Models;
using CarBookProject.Data;

namespace CarBookProject.Areas.AdminDashboard.Controllers
{
    [Area("AdminDashboard")]
    public class ContactusController : Controller
    {
        private readonly AppDbContext _context;

        public ContactusController(AppDbContext context)
        {
            _context = context;
        }
        
        public async Task<IActionResult> AllMessages()
        {
            return View(await _context.Contactuss.ToListAsync());
        }
        // GET: AdminDashboard/Contactus
        public async Task<IActionResult> Index()
        {
            return View(await _context.Contactuss.Where(x=>x.Status==false).ToListAsync());
        }
        [HttpPost]
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

         
            var contactus = await _context.Contactuss.FindAsync(id);
            if (contactus == null)
            {
                return NotFound();
            }
            contactus.ContactusId = id;
            contactus.Status = true;
            _context.Contactuss.Update(contactus);
            _context.SaveChanges();

            return View(contactus);
        }
        // GET: AdminDashboard/Contactus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactus = await _context.Contactuss
                .FirstOrDefaultAsync(m => m.ContactusId == id);
            if (contactus == null)
            {
                return NotFound();
            }

            return View(contactus);
        }

        // GET: AdminDashboard/Contactus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdminDashboard/Contactus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ContactusId,Name,Email,Subject,Message,Status")] Contactus contactus)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contactus);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contactus);
        }

        // GET: AdminDashboard/Contactus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactus = await _context.Contactuss.FindAsync(id);
            if (contactus == null)
            {
                return NotFound();
            }
            return View(contactus);
        }

        // POST: AdminDashboard/Contactus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ContactusId,Name,Email,Subject,Message,Status")] Contactus contactus)
        {
            if (id != contactus.ContactusId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contactus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactusExists(contactus.ContactusId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(contactus);
        }

        // GET: AdminDashboard/Contactus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactus = await _context.Contactuss
                .FirstOrDefaultAsync(m => m.ContactusId == id);
            if (contactus == null)
            {
                return NotFound();
            }

            return View(contactus);
        }

        // POST: AdminDashboard/Contactus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contactus = await _context.Contactuss.FindAsync(id);
            _context.Contactuss.Remove(contactus);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactusExists(int id)
        {
            return _context.Contactuss.Any(e => e.ContactusId == id);
        }
    }
}
