namespace AppDevAssignment.DTOs
{
    public class CompanyDTO
    {
        public int CompanyId { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
    }
}