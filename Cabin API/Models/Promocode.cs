using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace Cabin_API.Models
{
    public class Promocode
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [Required]
        public string Code { get; set; } = null!;

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public int Percent { get; set; }

        public int? Uses { get; set; }

        public int? MaxUses { get; set; }

        public DateTime? CreatedAt { get; set; }
    
        public DateTime? ValidUntil { get; set; }
    }
}
