using System;

namespace Database.Models
{
    public class Schedule
    {
        public int Id { get; set; }
        public int ExternalId { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int AvailabilityLookupId { get; set; }
        public AvailabilityLookup AvailabilityLookup { get; set; }
    }
}
