namespace TechnicalTestCibergestionBack.Application.DTOs
{
    public class NpsResultDto
    {
        public int Promoters { get; set; }
        public int Neutrals { get; set; }
        public int Detractors { get; set; }
        public double NPS { get; set; }
    }
}
