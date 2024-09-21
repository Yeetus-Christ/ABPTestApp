namespace ABPTestApp.Dtos
{
    public class ConferenceHallDto
    {
        public int Id { get; set; } = 0;
        public string Name { get; set; }

        public int Capacity { get; set; }

        public decimal RatePerHour { get; set; }
    }
}
