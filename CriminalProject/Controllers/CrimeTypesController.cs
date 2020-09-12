using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using CriminalProject.Models;

namespace CriminalProject.Controllers
{
    [Authorize]
    public class CrimeTypesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CrimeTypes
        public async Task<ActionResult> Index()
        {
            return View(await db.CrimeTypes.ToListAsync());
        }

        // GET: CrimeTypes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CrimeType crimeType = await db.CrimeTypes.FindAsync(id);
            if (crimeType == null)
            {
                return HttpNotFound();
            }
            return View(crimeType);
        }

        // GET: CrimeTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CrimeTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name")] CrimeType crimeType)
        {
            if (ModelState.IsValid)
            {
                db.CrimeTypes.Add(crimeType);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(crimeType);
        }

        // GET: CrimeTypes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CrimeType crimeType = await db.CrimeTypes.FindAsync(id);
            if (crimeType == null)
            {
                return HttpNotFound();
            }
            return View(crimeType);
        }

        // POST: CrimeTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name")] CrimeType crimeType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(crimeType).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(crimeType);
        }

        // GET: CrimeTypes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CrimeType crimeType = await db.CrimeTypes.FindAsync(id);
            if (crimeType == null)
            {
                return HttpNotFound();
            }
            return View(crimeType);
        }

        // POST: CrimeTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CrimeType crimeType = await db.CrimeTypes.FindAsync(id);
            db.CrimeTypes.Remove(crimeType);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
