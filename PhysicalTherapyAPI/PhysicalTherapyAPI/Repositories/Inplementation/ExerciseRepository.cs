using PhysicalTherapyAPI.Models;
using PhysicalTherapyAPI.Repositories.Interfaces;
using Microsoft.CodeAnalysis.Operations;
using EntityFrameworkCore.GenericRepository;

namespace PhysicalTherapyAPI.Repositories.Inplementation
{
    public class ExerciseRepository:Repository<Exercise>, IExerciseRepository
    {
        private readonly ApplicationDbContext Context;
        public ExerciseRepository(ApplicationDbContext context) : base(context)
        {
            this.Context = context;
        }
        public IEnumerable<Exercise> GetExercisesByCatId(int catId)
        {
            return Context.Exercises.Where(e => e.CategoryId == catId);
        }

    }
}
