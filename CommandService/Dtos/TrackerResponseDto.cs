namespace CommandService.Dtos
{
    public class TrackerResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Publisher { get; set; }
        public string Cost { get; set; }
        public DateTime DateCreated { get; set; }
        public long TotalPurchase { get; set; }
    }
}
