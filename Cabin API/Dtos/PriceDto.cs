using System.ComponentModel.DataAnnotations;

namespace Cabin_API.Dtos
{
    public class PriceDto
    {
        public string? Id { get; set; }

        [Required]
        public int Winter { get; set; } = 0;

        [Required]
        public int Spring { get; set; } = 0;

        [Required]
        public int Summer { get; set; } = 0;

        [Required]
        public int Autumn { get; set; } = 0;
    }
}
