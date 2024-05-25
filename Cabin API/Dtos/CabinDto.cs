using Cabin_API.Models;
using System.ComponentModel.DataAnnotations;

namespace Cabin_API.Dtos
{
    public class CabinDto
    {
        [Required]
        public int Id { get; set; }

        public string[]? Reservations { get; set; }
    }
}
