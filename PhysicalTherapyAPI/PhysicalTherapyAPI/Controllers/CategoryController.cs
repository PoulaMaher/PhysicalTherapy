using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhysicalTherapyAPI.Models;
using PhysicalTherapyAPI.Repositories.Inplementation;

namespace PhysicalTherapyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("GetAllCategories")]
        public IActionResult GetAllCategories() 
        { 
            return Ok(_unitOfWork.CategoryRepository.GetAll());
        }

        [HttpPost("AddCategory")]
        public IActionResult AddCategory(string CategoryName, IFormFile img)
        {
            if (ModelState.IsValid)
            {
                if (img == null || img.Length == 0)
                { return BadRequest("No file uploaded."); }

                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "Category");
                var fileName = Guid.NewGuid().ToString() + "_" + img.FileName;
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                {
                    img.CopyTo(stream);
                }
                var photoUrl = $"https://localhost:7197/Images/Category/{fileName}";
                Category category = new Category()
                {
                    Name = CategoryName,
                    PhotoUrl = photoUrl
                };
                _unitOfWork.CategoryRepository.Add(category);
                _unitOfWork.save();
                return Ok();
            }
            return BadRequest();

        }


    }
}
