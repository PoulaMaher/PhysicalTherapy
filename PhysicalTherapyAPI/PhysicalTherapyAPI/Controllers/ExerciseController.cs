﻿using AutoMapper;
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
        private readonly IMapper _mapper;

        public ExerciseController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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

        [HttpPost("AddExercise")]
        public IActionResult AddExercise([FromQuery]ExerciseDTO exerciseDTO, IFormFile img)
        {
            if (ModelState.IsValid)
            {
                if (img == null || img.Length == 0)
                { return BadRequest("No file uploaded."); }

                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "Exercise");
                var fileName = Guid.NewGuid().ToString() + "_" + img.FileName;
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                {
                    img.CopyTo(stream);
                }
                var photoUrl = $"https://localhost:7197/Images/Exercise/{fileName}";
                Exercise exercise = _mapper.Map<Exercise>(exerciseDTO);
                exercise.PhotoUrl = photoUrl;
                _unitOfWork.ExerciseRepository.Add(exercise);
                _unitOfWork.save();
                return Ok();
            }
            return BadRequest();

        }

        [HttpPut("UpdateExercise")]
        public IActionResult UpdateExercise([FromQuery] ExerciseDTO exerciseDTO, int id, IFormFile? img)
        {
            var DBExercise = _unitOfWork.ExerciseRepository.Get(c => c.Id == id);
            if (DBExercise == null)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(exerciseDTO.Name))
                {
                    DBExercise.Name = exerciseDTO.Name;
                }
                if (!string.IsNullOrEmpty(exerciseDTO.Description))
                {
                    DBExercise.Description = exerciseDTO.Description;
                }
                if (!string.IsNullOrEmpty(exerciseDTO.ExerciseType))
                {
                    DBExercise.ExerciseType = exerciseDTO.ExerciseType;
                }
                if (!string.IsNullOrEmpty(exerciseDTO.ExerciseLink))
                {
                    DBExercise.ExerciseLink = exerciseDTO.ExerciseLink;
                }

                if (img != null && img.Length > 0)
                {
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "Exercise");
                    var fileName = Guid.NewGuid().ToString() + "_" + img.FileName;
                    var filePath = Path.Combine(uploadsFolder, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        img.CopyTo(stream);
                    }

                    var photoUrl = $"https://localhost:7197/Images/Exercise/{fileName}";
                    DBExercise.PhotoUrl = photoUrl;
                }
                _unitOfWork.ExerciseRepository.Update(DBExercise);
                _unitOfWork.save();

                return Ok();
            }

            return BadRequest(ModelState);
        }

    }
}
