using DSM.Models.Entities;
using DSM.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace DSM.UI.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        
        public IActionResult Index()
        {
            var data = _categoryRepository.GetAll();

            return View(data);
        }

        
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _categoryRepository.Add(category);

                return RedirectToAction("Index");
            }

            return View(category);
        }

      
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var data = _categoryRepository.GetById(id);

            return View(data);
        }

       
        [HttpPost]
        public IActionResult Edit(Category category)
        {
            _categoryRepository.Update(category);

            return RedirectToAction("Index");
        }

        
        public IActionResult Delete(int id)
        {
            _categoryRepository.Delete(id);

            return RedirectToAction("Index");
        }
    }
}