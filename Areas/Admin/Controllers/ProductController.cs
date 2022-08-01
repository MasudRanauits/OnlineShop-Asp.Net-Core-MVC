using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using OnlineShop.Models;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private ApplicationDbContext _db;
        //[System.Obsolete]
        private IHostingEnvironment _he;

        //[System.Obsolete]
        public ProductController(ApplicationDbContext db, IHostingEnvironment he)
        {
            _db = db;
            _he = he;
        }
        public IActionResult Index()
        {
            return View(_db.Products.Include(c=>c.ProductTypes).Include(f=>f.SpecialTag).ToList());
        }

        //POST Index Action Method
        [HttpPost]
        //lowAmount & largeAmount price range list
        public IActionResult Index(decimal? lowAmount, decimal? largeAmount)
        {
            var products=_db.Products.Include(c=>c.ProductTypes).Include(c=>c.SpecialTag)
                .Where(c=>c.Price >=lowAmount && c.Price <= largeAmount).ToList();
            if (lowAmount==null ||largeAmount==null)
            {
                products = _db.Products.Include(c => c.ProductTypes).Include(c => c.SpecialTag).ToList();
            }
            return View(products);
        }

        //GET Create method
        public IActionResult Create()
        {
            ViewData["productTypeId"]=new SelectList(_db.ProductTypes.ToList(),"Id", "ProductType");
            ViewData["TagId"] = new SelectList(_db.SpecialTags.ToList(), "Id", "Name");
            return View();
        }

        //POST Create Method
        [HttpPost]
        public async Task<IActionResult> Create(Products products,IFormFile image)
        {
            if (ModelState.IsValid)
            {
                //Same name products not exist code
                var searchProduct=_db.Products.FirstOrDefault(c=>c.Name== products.Name);
                if (searchProduct!=null)
                {
                    ViewBag.message = "This product is already exist";
                    ViewData["productTypeId"] = new SelectList(_db.ProductTypes.ToList(), "Id", "ProductType");
                    ViewData["TagId"] = new SelectList(_db.SpecialTags.ToList(), "Id", "Name");
                    return View(products);
                }
                if (image!=null)
                {
                    var name = Path.Combine(_he.WebRootPath + "/Images", Path.GetFileName(image.FileName));
                    await image.CopyToAsync(new FileStream(name, FileMode.Create));
                    products.Image = "Images/" + image.FileName;

                }
                if (image==null)
                {
                    products.Image = "Images/No-Image-Found.png";
                }
                _db.Products.Add(products);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(products);
        }

        // GET Edit Action Method
        public ActionResult Edit(int? id)
        {
            ViewData["productTypeId"] = new SelectList(_db.ProductTypes.ToList(), "Id", "ProductType");
            ViewData["TagId"] = new SelectList(_db.SpecialTags.ToList(), "Id", "Name");
            if (id==null)
            {
                return NotFound();
            }
            var product=_db.Products.Include(c=>c.ProductTypes).Include(c=>c.SpecialTag)
                .FirstOrDefault(c=>c.Id==id);
            if (product==null)
            {
                NotFound();
            }
            return View(product);
        }

        //Edit POST Action Method
        [HttpPost]
        public async Task<IActionResult> Edit(Products products, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                //Same name products not exist code
                var searchProduct = _db.Products.FirstOrDefault(c => c.Name == products.Name);
                if (searchProduct != null)
                {
                    ViewBag.message = "This product is already exist";
                    ViewData["productTypeId"] = new SelectList(_db.ProductTypes.ToList(), "Id", "ProductType");
                    ViewData["TagId"] = new SelectList(_db.SpecialTags.ToList(), "Id", "Name");
                    return View(products);
                }
                if (image != null)
                {
                    var name = Path.Combine(_he.WebRootPath + "/Images", Path.GetFileName(image.FileName));
                    await image.CopyToAsync(new FileStream(name, FileMode.Create));
                    products.Image = "Images/" + image.FileName;
                }
                if (image == null)
                {
                    products.Image = "Images/No-Image-Found.png";
                }
                _db.Products.Update(products);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(products);
        }

        //GET Details Actiom Method
        public ActionResult Details(int? id)
        {
            if (id==null)
            {
                return NotFound();
            }
            var product=_db.Products.Include(c=>c.ProductTypes).Include(c=>c.SpecialTag)
                .FirstOrDefault(c => c.Id == id);
            if (product==null)
            {
                return NotFound();
            }
            return View(product);
        }

        //Get Delete Action Method
        public ActionResult Delete(int? id)
        {
            if (id==null)
            {
                return NotFound();
            }
            var product=_db.Products.Include(c => c.ProductTypes).Include(c => c.SpecialTag)
                .Where(c => c.Id == id).FirstOrDefault(c=>c.Id==id);
            if (product==null)
            {
                return NotFound();
            }
            return View(product);
        }

        //POST Delete Action Method
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int? id)
        {
            if (id==null)
            {
                return NotFound();
            }
            var product=_db.Products.FirstOrDefault(c=>c.Id==id);
            if (product==null)
            {
                return NotFound();
            }
            _db.Products.Remove(product);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
