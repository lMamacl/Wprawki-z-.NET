using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Wprawka_2.Data;
using Wprawka_2.Models;

namespace Wprawka_2.Controllers
{
    public class DevicesController : Controller
    {
        private readonly AppDbContext _context;

        public DevicesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Devices
        public async Task<IActionResult> Index(int? filterClusterId)
        {
            // 1. ODCZYT Z SESJI: Jeśli użytkownik nie kliknął filtru, sprawdźmy czy ma jakiś zapisany w sesji
            if (!filterClusterId.HasValue && HttpContext.Session.GetInt32("SavedFilter") != null)
            {
                filterClusterId = HttpContext.Session.GetInt32("SavedFilter");
            }

            // 2. BAZOWE ZAPYTANIE
            var applicationDbContext = _context.Devices.Include(d => d.Cluster).AsQueryable();

            // 3. LOGIKA FILTROWANIA
            if (filterClusterId.HasValue && filterClusterId.Value != 0)
            {
                applicationDbContext = applicationDbContext.Where(d => d.ClusterId == filterClusterId.Value);

                // ZAPIS DO SESJI
                HttpContext.Session.SetInt32("SavedFilter", filterClusterId.Value);
            }
            else
            {
                // Reset filtra (np. wybranie opcji "Wszystkie")
                HttpContext.Session.Remove("SavedFilter");
            }

            // Zwracamy listę klastrów do formularza filtru (Dropdown)
            ViewData["FilterClusterId"] = new SelectList(_context.Clusters, "Id", "Name", filterClusterId);

            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Devices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var device = await _context.Devices
                .Include(d => d.Cluster)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (device == null)
            {
                return NotFound();
            }

            return View(device);
        }

        // GET: Devices/Create
        public IActionResult Create()
        {
            ViewData["ClusterId"] = new SelectList(_context.Clusters, "Id", "Name");
            return View();
        }

        // POST: Devices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DeviceName,PowerWatt,ClusterId")] Device device)
        {
            if (ModelState.IsValid)
            {
                _context.Add(device);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClusterId"] = new SelectList(_context.Clusters, "Id", "Name", device.ClusterId);
            return View(device);
        }

        // GET: Devices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var device = await _context.Devices.FindAsync(id);
            if (device == null)
            {
                return NotFound();
            }
            ViewData["ClusterId"] = new SelectList(_context.Clusters, "Id", "Name", device.ClusterId);
            return View(device);
        }

        // POST: Devices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DeviceName,PowerWatt,ClusterId")] Device device)
        {
            if (id != device.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(device);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeviceExists(device.Id))
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
            ViewData["ClusterId"] = new SelectList(_context.Clusters, "Id", "Name", device.ClusterId);
            return View(device);
        }

        // GET: Devices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var device = await _context.Devices
                .Include(d => d.Cluster)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (device == null)
            {
                return NotFound();
            }

            return View(device);
        }

        // POST: Devices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var device = await _context.Devices.FindAsync(id);
            if (device != null)
            {
                _context.Devices.Remove(device);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DeviceExists(int id)
        {
            return _context.Devices.Any(e => e.Id == id);
        }
    }
}
