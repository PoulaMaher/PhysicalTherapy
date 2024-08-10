using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhysicalTherapyAPI.DTOs;
using PhysicalTherapyAPI.Models;
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
        [HttpGet("GetFilteredExercises")]
        public ActionResult<Exercise> GetFilteredExercises([FromQuery] FilterationDto filterObj)
        {
            if (ModelState.IsValid)
            {
                return Ok(_unitOfWork.ExerciseRepository.GetFilteredExercises(filterObj, "Category"));
            }
            return Ok(ModelState);
        }
    }
}
