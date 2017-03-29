using Alexa.Entities;
using Alexa_GWV.Web.Models;
using Microsoft.AspNet.Identity;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace Alexa_GWV.Web.Controllers
{
    public class UserPreferencesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: UserPreferences
        [Authorize]
        public async Task<ActionResult> Index()
        {
            var userId = User.Identity.GetUserId();
            var userPreferences = db.UserPreferences
                .Include(u => u.ApplicationUser)
                .Where(x=>x.ApplicationUser.Id == userId);
            return View(await userPreferences.ToListAsync());
        }

        // GET: UserPreferences/Details/5
        [Authorize]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserPreference userPreference = await db.UserPreferences.FindAsync(id);
            if (userPreference == null)
            {
                return HttpNotFound();
            }
            return View(userPreference);
        }

        // GET: UserPreferences/Create
        [Authorize]
        public ActionResult Create()
        {

            //ViewBag.UserId = User.Identity.GetUserId();
            return View();
        }

        // POST: UserPreferences/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> Create([Bind(Include = "PreferredName,City")] UserPreference userPreference)
        {
            userPreference.UserId = this.User.Identity.GetUserId();
          

            if (ModelState.IsValid)
            {
                db.UserPreferences.Add(userPreference);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = this.User.Identity.GetUserId();
            return View(userPreference);
        }

        // GET: UserPreferences/Edit/5
        [Authorize]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserPreference userPreference = await db.UserPreferences.FindAsync(id);
            if (userPreference == null)
            {
                return HttpNotFound();
            }
            if (userPreference.UserId != this.User.Identity.GetUserId())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.UserId = this.User.Identity.GetUserId();
            return View(userPreference);
        }

        // POST: UserPreferences/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> Edit([Bind(Include = "Id,PreferredName,City")] UserPreference userPreference)
        {
            userPreference.UserId = this.User.Identity.GetUserId();

            if (ModelState.IsValid)
            {
                db.Entry(userPreference).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = this.User.Identity.GetUserId();
            return View(userPreference);
        }

        // GET: UserPreferences/Delete/5
        [Authorize]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserPreference userPreference = await db.UserPreferences.FindAsync(id);
            if (userPreference == null)
            {
                return HttpNotFound();
            }
            return View(userPreference);
        }

        [Authorize]
        // POST: UserPreferences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {

            UserPreference userPreference = await db.UserPreferences.FindAsync(id);
            db.UserPreferences.Remove(userPreference);
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
