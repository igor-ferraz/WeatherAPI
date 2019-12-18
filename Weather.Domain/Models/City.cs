using System.Collections.Generic;

namespace Weather.Domain.Models
{
    public class City
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string State { get; set; }

        public List<Temperature> Temperatures { get; set; }
    }
}