using Microsoft.AspNetCore.Mvc;
using OnlineShop.Data;
using OnlineShop.Models;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductTypesController : Controller
    {
        private ApplicationDbContext _db;
        public ProductTypesController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            //var data = _db.ProductTypes.ToList();
            return View(_db.ProductTypes.ToList());
        }

        //Create GET Action Method
        public ActionResult Create()
        {
            return View();
        }

        //Create POST Action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductTypes productTypes)
        {
            if (ModelState.IsValid)
            {
                _db.ProductTypes.Add(productTypes);
                await _db.SaveChangesAsync();
                TempData["save"] = "Product type has been saved";     /*---Alert message-----*/
                return RedirectToAction(nameof(Index));
                //return RedirectToAction("Index");
            }
            return View(productTypes);
        }


        //Edit GET Action Method
        public ActionResult Edit(int? id)
        {
            if (id==null)
            {
                return NotFound();
            }
            var productType = _db.ProductTypes.Find(id);
            if (productType==null)
            {
                return NotFound();
            }
            return View(productType);
        }

        //Edit POST Action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductTypes productTypes)
        {
            if (ModelState.IsValid)
            {
                _db.Update(productTypes);
                await _db.SaveChangesAsync();
                TempData["edit"] = "Update Sucessfully";   /*---Alert message-----*/
                return RedirectToAction(nameof(Index));
                //return RedirectToAction("Index");
            }
            return View(productTypes);
        }


        //Details GET Action Method
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var productType = _db.ProductTypes.Find(id);
            if (productType == null)
            {
                return NotFound();
            }
            return View(productType);
        }

        //Details POST Action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details(ProductTypes productTypes)
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
            var productType = _db.ProductTypes.Find(id);
            if (productType == null)
            {
                return NotFound();
            }
            return View(productType);
        }

        //Delete POST Action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id,ProductTypes productTypes)
        {
            if (id==null)
            {
                return NotFound();
            }
            if (id!=productTypes.Id)
            {
                return NotFound();
            }
            var productType= _db.ProductTypes.Find(id);
            if (productType == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _db.Remove(productType);
                await _db.SaveChangesAsync();
                TempData["delete"] = "Delete Sucessfully"; /*---Alert message-----*/
                return RedirectToAction(nameof(Index));
                //return RedirectToAction("Index");
            }
            return View(productTypes);
        }
    }
}
