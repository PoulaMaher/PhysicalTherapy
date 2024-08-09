using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhysicalTherapyAPI.Repositories.Inplementation;

namespace PhysicalTherapyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExerciseController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ExerciseController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("GetExercisesByCategoryId")]
        public IActionResult GetExercisesByCategoryId(int CatId)
        {
            return Ok(_unitOfWork.ExerciseRepository.GetExercisesByCatId(CatId));
        }
    }
}
