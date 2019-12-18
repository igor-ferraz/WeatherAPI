namespace Weather.Domain.DataContracts
{
    public class HGWeatherResponse
    {
        public decimal Temp { get; set; }
        public string City_Name { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
    }
}