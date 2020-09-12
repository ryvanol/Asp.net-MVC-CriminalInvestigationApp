using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using CriminalProject.Models;
using CriminalProject.ViewModels;

namespace CriminalProject.Controllers
{
[Authorize]
    public class CrimeHistoriesController : Controller
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        // GET: CrimeHistories
        public async Task<ActionResult> Index()
        {
            var crimeHistories = _db.CrimeHistories.Include(c => c.City).Include(c => c.CrimeType).Include(c => c.Officer).Include(c => c.Weapon);
            return View(await crimeHistories.ToListAsync());
        }

        // GET: CrimeHistories/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CrimeHistory crimeHistory = await _db.CrimeHistories.FindAsync(id);
            if (crimeHistory == null)
            {
                return HttpNotFound();
            }
            return View(crimeHistory);
        }

        // GET: CrimeHistories/Create
        public ActionResult Create()
        {
            var weapons = _db.Weapons.Select(w => new SelectListItem
            {
                Text = w.Type,
                Value = w.Id.ToString()
            }).ToList();
            var crimeTypes = _db.CrimeTypes.Select(c=> new SelectListItem()
            {
                Text = c.Name,
                Value = c.Id.ToString()
            }).ToList();
            var cities = _db.Cities.Select(c=> new SelectListItem
            {
                Text =  c.Name,
                Value = c.Id.ToString(),
            }).ToList();
            var officers = _db.Users.Select(o=> new SelectListItem()
            {
                Text = o.UserName,
                Value = o.Id,
            }).ToList();
            var crimeViewModel = new CrimeHistoryViewModel()
            {
                Weapons = weapons,
                CrimeTypes = crimeTypes,
                Cities = cities,
                Officers = officers,
            };
            return View(crimeViewModel);
        }

        // POST: CrimeHistories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Exclude = "Id")] CrimeHistory crimeHistory)
        {
            crimeHistory.FkOfficerId = crimeHistory.Officer.Id;
           crimeHistory.City = _db.Cities.Find(crimeHistory.City.Id);
           crimeHistory.CrimeType = _db.CrimeTypes.Find(crimeHistory.CrimeType.Id);
            crimeHistory.Officer = _db.Users.Find(crimeHistory.FkOfficerId);
            crimeHistory.Weapon = _db.Weapons.Find(crimeHistory.Weapon.Id);
            crimeHistory.FkWeaponId = crimeHistory.Weapon.Id;
            if (ModelState.IsValid)
            {
                _db.CrimeHistories.Add(crimeHistory);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            var weapons = _db.Weapons.Select(w => new SelectListItem
            {
                Text = w.Type,
                Value = w.Id.ToString()
            }).ToList();
            var crimeTypes = _db.CrimeTypes.Select(c => new SelectListItem()
            {
                Text = c.Name,
                Value = c.Id.ToString()
            }).ToList();
            var cities = _db.Cities.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString(),
            }).ToList();
            var officers = _db.Users.Select(o => new SelectListItem()
            {
                Text = o.UserName,
                Value = o.Id,
            }).ToList();
            var crimeViewModel = new CrimeHistoryViewModel()
            {
                Weapons = weapons,
                CrimeTypes = crimeTypes,
                Cities = cities,
                Officers = officers,
            };
            return View(crimeViewModel);
        }

        // GET: CrimeHistories/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CrimeHistory crimeHistory = await _db.CrimeHistories.FindAsync(id);
            if (crimeHistory == null)
            {
                return HttpNotFound();
            }
            ViewBag.FkCityId = new SelectList(_db.Cities, "Id", "Name", crimeHistory.FkCityId);
            ViewBag.FkCrimeTypeId = new SelectList(_db.CrimeTypes, "Id", "Name", crimeHistory.FkCrimeTypeId);
           // ViewBag.FkOfficerId = new SelectList(db.ApplicationUsers, "Id", "Email", crimeHistory.FkOfficerId);
            ViewBag.FkWeaponId = new SelectList(_db.Weapons, "Id", "Type", crimeHistory.FkWeaponId);
            return View(crimeHistory);
        }

        // POST: CrimeHistories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Date,FkCrimeTypeId,FkWeaponId,FkOfficerId,Area,FkCityId,ResourceUrl")] CrimeHistory crimeHistory)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(crimeHistory).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.FkCityId = new SelectList(_db.Cities, "Id", "Name", crimeHistory.FkCityId);
            ViewBag.FkCrimeTypeId = new SelectList(_db.CrimeTypes, "Id", "Name", crimeHistory.FkCrimeTypeId);
            //ViewBag.FkOfficerId = new SelectList(db.ApplicationUsers, "Id", "Email", crimeHistory.FkOfficerId);
            ViewBag.FkWeaponId = new SelectList(_db.Weapons, "Id", "Type", crimeHistory.FkWeaponId);
            return View(crimeHistory);
        }

        // GET: CrimeHistories/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CrimeHistory crimeHistory = await _db.CrimeHistories.FindAsync(id);
            if (crimeHistory == null)
            {
                return HttpNotFound();
            }
            return View(crimeHistory);
        }

        // POST: CrimeHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CrimeHistory crimeHistory = await _db.CrimeHistories.FindAsync(id);
            _db.CrimeHistories.Remove(crimeHistory);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
