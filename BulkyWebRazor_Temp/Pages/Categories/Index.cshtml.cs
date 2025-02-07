using BulkyWebRazor_Temp.Data;
using BulkyWebRazor_Temp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyWebRazor_Temp.Pages.Categories
{
    public class IndexModel : PageModel
    {
        protected readonly ApplicationDbContext _db;
        public List<Category> CategoryList { get; set; }

        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public void OnGet()
        {
            CategoryList = _db.Categories.ToList();
        }

		public IActionResult OnPostDelete(int categoryId)
        {
			Category obj = _db.Categories.Find(categoryId);
			if (obj == null)
            {
				return NotFound();
			}
			_db.Categories.Remove(obj);
			_db.SaveChanges();
			return new JsonResult(new { success = true });
			//return RedirectToPage("Index");
		}
    }
}
