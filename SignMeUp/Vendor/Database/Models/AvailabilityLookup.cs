namespace Database.Models
{
    public class AvailabilityLookup
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public const string BUSY_NAME = "Busy";
        public const string FREE_NAME = "Free";
    }
}
