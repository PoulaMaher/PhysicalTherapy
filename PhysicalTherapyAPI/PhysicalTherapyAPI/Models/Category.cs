using System.ComponentModel.DataAnnotations.Schema;

namespace PhysicalTherapyAPI.Models
{
    public class Category
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public string? Name { get; set; }
        public string? PhotoUrl { get; set; }

        public IEnumerable<Exercise>? Exercises{ get; set; }

    }
}
