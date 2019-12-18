using System;

namespace Weather.Domain.Models
{
    public class Temperature
    {
        public int Id { get; set; }

        public decimal Degrees { get; set; }

        public DateTime Created { get; set; }

        public int CityId { get; set; }
    }
}