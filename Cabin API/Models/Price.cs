using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace Cabin_API.Models
{
    public class Price
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [Required]
        public int Winter { get; set; } = 0;

        [Required]
        public int Spring { get; set; } = 0;

        [Required]
        public int Summer { get; set; } = 0;

        [Required]
        public int Autumn { get; set; } = 0;

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public int GetPrice(DateTime Date)
        {   
            DateTime springStart = new DateTime(Date.Year, 3, 20);
            DateTime summerStart = new DateTime(Date.Year, 6, 21);
            DateTime fallStart = new DateTime(Date.Year, 9, 23);
            DateTime winterStart = new DateTime(Date.Year, 12, 21);

            if (Date >= springStart && Date < summerStart)
                return Spring;
            else if (Date >= summerStart && Date < fallStart)
                return Summer;
            else if (Date >= fallStart && Date < winterStart)
                return Autumn;
            else if (Date >= winterStart && Date < springStart)
                return Winter;
            return 0;
        }

    }
}
