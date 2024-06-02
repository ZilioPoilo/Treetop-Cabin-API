using System.ComponentModel.DataAnnotations;

namespace Cabin_API.Dtos
{
    public class PromocodeDto
    {
        public string? Id { get; set; }

        [Required]
        public string Code { get; set; } = null!;

        public bool? IsActive { get; set; }

        [Required]
        public int Percent { get; set; }

        public int? Uses { get; set; }

        public int? MaxUses { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? ValidUntil { get; set; }
    }
}
