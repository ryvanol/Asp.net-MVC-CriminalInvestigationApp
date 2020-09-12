using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using CriminalProject.Models;

namespace CriminalProject.Controllers
{
    [Authorize]
    public class SuspectsController : Controller
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        // GET: Suspects
        public async Task<ActionResult> Index()
        {
            return View(await _db.Suspects.ToListAsync());
        }

        // GET: Suspects/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Suspect suspect = await _db.Suspects.FindAsync(id);
            if (suspect == null)
            {
                return HttpNotFound();
            }
            return View(suspect);
        }

        // GET: Suspects/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Suspects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Age,Birthdate,Address,FkSuspectPictureId")] Suspect suspect)
        {
            if (ModelState.IsValid)
            {
                _db.Suspects.Add(suspect);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(suspect);
        }

        // GET: Suspects/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Suspect suspect = await _db.Suspects.FindAsync(id);
            if (suspect == null)
            {
                return HttpNotFound();
            }
            return View(suspect);
        }

        // POST: Suspects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Age,Birthdate,Address,FkSuspectPictureId")] Suspect suspect)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(suspect).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(suspect);
        }

        // GET: Suspects/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Suspect suspect = await _db.Suspects.FindAsync(id);
            if (suspect == null)
            {
                return HttpNotFound();
            }
            return View(suspect);
        }

        // POST: Suspects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Suspect suspect = await _db.Suspects.FindAsync(id);
            _db.Suspects.Remove(suspect);
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
