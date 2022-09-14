namespace RT_Coffee_Machine.Models
{
    public class GetBrewCoffeResponse
    {
        public string Message { get; set; } = string.Empty;
        public DateTimeOffset Prepared { get; set; }
    }
}
