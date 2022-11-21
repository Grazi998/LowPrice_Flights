namespace LowPrice_Flights_Back
{
    public class SearchParameters
    {
        public string? OriginAirport { get; set; }
        public string? DestinationAirport { get; set; }
        public string? DepartureDate { get; set; }
        public string? ReturnDate { get; set; }
        public int PassangersNumber { get; set; }
        public string? CurrencyCode { get; set; }
    }
}
