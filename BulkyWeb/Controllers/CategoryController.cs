using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
	public class CategoryController : Controller
	{
		private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
			_db = db;
        }

		public IActionResult Index()
		{
			List<Category> categoriesList = _db.Categories.ToList();
			return View(categoriesList);
		}


		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Create(Category obj)
		{
			if(obj.Name == obj.DisplayOrder.ToString())
			{
				ModelState.AddModelError("Name", "The DisplayOrder cannot exactly match the Name.");
			}

			if(ModelState.IsValid)
			{
				_db.Categories.Add(obj);
				_db.SaveChanges();
				TempData["success"] = "Category created successfully";
				return RedirectToAction("Index");
			}
			return View();
			
		}

		public IActionResult Edit(int? categoryId)
		{
			if(categoryId == null || categoryId == 0)
			{
				return NotFound();
			}
			Category? category = _db.Categories.Find(categoryId);
			if(category == null)
			{
				return NotFound();
			}
			return View(category);
		}

		[HttpPost]
		public IActionResult Edit(Category obj)
		{
			if (obj.Name == obj.DisplayOrder.ToString())
			{
				ModelState.AddModelError("Name", "The DisplayOrder cannot exactly match the Name.");
			}

			if (ModelState.IsValid)
			{
				_db.Categories.Update(obj);
				_db.SaveChanges();
				TempData["success"] = "Category updated successfully";
				return RedirectToAction("Index");
			}
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult Delete(int? categoryId)
        {
			bool result = false;
			Category? obj = _db.Categories.Find(categoryId);
			if(obj != null)
			{
				_db.Categories.Remove(obj);
				_db.SaveChanges();
				TempData["success"] = "Category deleted successfully";
				result = true;
			}
			
			return Json(result);
        }
    }
}
