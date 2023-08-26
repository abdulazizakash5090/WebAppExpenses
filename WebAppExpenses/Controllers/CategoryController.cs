using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using WebAppExpenses.Data;
using WebAppExpenses.Models;
using WebAppExpenses.Models.Domain;


namespace WebAppExpenses.Controllers
{
    public class CategoryController : Controller
    {

        private readonly AppDemoDbContext _context;

        public CategoryController(AppDemoDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {   

            var categories = await _context.Categories.ToListAsync();
            return View(categories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddCategoryViewModel addCategoryRequest)
        {
            // Check if the category name already exists.
            if (_context.Categories.Any(m => m.Name == addCategoryRequest.Name))
            {

                TempData["ErrorMes"] = "Category already exists";
                return View(addCategoryRequest);
            }
            if (ModelState.IsValid)
            {
                TempData["successMessage"] = "Category created successfully";
                var category = new Category()
                {
                    /*    Id = addCategoryRequest.Id,*/
                    Name = addCategoryRequest.Name,
                    Amount = addCategoryRequest.Amount,
                    FromDate = addCategoryRequest.FromDate,
                    ToDate = addCategoryRequest.ToDate,

                };

                await _context.Categories.AddAsync(category);
                await _context.SaveChangesAsync();
                return RedirectToAction("Create");
            }
            else
            {
                TempData["errorMessage"] = "Model state is invalid";
                return View();
            }
         
    
         
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {

            var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);


            if (category != null)
            {
                var view = new UpdateCategoryModel()
                {

                 /*   Id = category.Id,*/
                    Name = category.Name,
                    Amount = category.Amount,
                    FromDate = category.FromDate,
                    ToDate = category.ToDate,

                };
                return await Task.Run(() =>View(view));
               
            }
            TempData["errorMessage"] = $"Category details not found with id : {id}";
            return RedirectToAction("Index");

        }



        [HttpPost]
        public async Task<IActionResult> Update(UpdateCategoryModel model) 
        {

           
            // Check if the category name already exists.
            if (_context.Categories.Any(m => m.Name == model.Name))
            {
              
                TempData["ErrorMes"] = "Category already exists";
                return View(model);
            }

            var category = await _context.Categories.FindAsync(model.Id);

            if (category != null)
            {

                category.Id = model.Id;
                category.Name = model.Name;
                category.Amount = model.Amount;
                category.FromDate = model.FromDate;
                category.ToDate = model.ToDate;

            }

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");

        }

        [HttpPost]
  
        public async Task<IActionResult> Delete(int id)
        {
        
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
}
