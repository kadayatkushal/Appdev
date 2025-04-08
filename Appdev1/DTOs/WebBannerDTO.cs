namespace AppDevAssignment.DTOs
{
    public class BannerDTO
    {
        public int BannerId { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required DateTime StartDate { get; set; }
        public required DateTime EndDate { get; set; }
        public int CompanyId { get; set; }
    }
}