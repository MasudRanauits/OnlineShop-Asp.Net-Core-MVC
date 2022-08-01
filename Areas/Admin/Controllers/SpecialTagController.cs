using Microsoft.AspNetCore.Mvc;
using OnlineShop.Data;
using OnlineShop.Models;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SpecialTagController : Controller
    {
        private ApplicationDbContext _db;


        public SpecialTagController(ApplicationDbContext db)
        {
            _db = db;
        }
        //GET Index Action Method
        public IActionResult Index()
        {
            //var data = _db.ProductTypes.ToList();
            return View(_db.SpecialTags.ToList());
        }

        //Create GET Action Method
        public ActionResult Create()
        {
            return View();
        }

        //Create POST Action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SpecialTag specialTag)
        {
            if (ModelState.IsValid)
            {
                _db.SpecialTags.Add(specialTag);

                await _db.SaveChangesAsync();
                TempData["save"] = "Product type has been saved";     /*---Alert message-----*/
                return RedirectToAction(nameof(Index));
                //return RedirectToAction("Index");
            }
            return View(specialTag);
        }


        //Edit GET Action Method
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var specialTag = _db.SpecialTags.Find(id);
            if (specialTag == null)
            {
                return NotFound();
            }
            return View(specialTag);
        }

        //Edit POST Action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SpecialTag specialTag)
        {
            if (ModelState.IsValid)
            {
                _db.Update(specialTag);
                await _db.SaveChangesAsync();
                TempData["edit"] = "Update Sucessfully";   /*---Alert message-----*/
                return RedirectToAction(nameof(Index));
                //return RedirectToAction("Index");
            }
            return View(specialTag);
        }


        //Details GET Action Method
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var specialTag = _db.SpecialTags.Find(id);
            if (specialTag == null)
            {
                return NotFound();
            }
            return View(specialTag);
        }

        //Details POST Action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details(SpecialTag specialTag)
        {
            return RedirectToAction(nameof(Index));
            //return RedirectToAction("Index");           
        }


        //Delete GET Action Method
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var specialTag = _db.SpecialTags.Find(id);
            if (specialTag == null)
            {
                return NotFound();
            }
            return View(specialTag);
        }

        //Delete POST Action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id, SpecialTag specialTag)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (id != specialTag.Id)
            {
                return NotFound();
            }
            var specialTags = _db.SpecialTags.Find(id);
            if (specialTags == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _db.Remove(specialTags);
                await _db.SaveChangesAsync();
                TempData["delete"] = "Delete Sucessfully"; /*---Alert message-----*/
                return RedirectToAction(nameof(Index));
                //return RedirectToAction("Index");
            }
            return View(specialTag);
        }
    }
}
