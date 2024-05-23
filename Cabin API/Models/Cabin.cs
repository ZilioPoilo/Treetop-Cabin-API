using System.ComponentModel.DataAnnotations;

namespace Cabin_API.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class Cabin
    {
        [Required]
        public int Id { get; set; }

        public string[]? Reservations { get; set; }
    }
}
