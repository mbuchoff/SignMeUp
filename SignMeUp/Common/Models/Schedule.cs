using System;

namespace VendorService.Models
{
    public enum Availability { Busy, Free };

    public class Schedule
    {
        public int Id { get; set; }
        public string Location { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Title { get; set; }
        public bool AllDay { get; set; }
        public Availability Availability { get; set; }
    }
}
