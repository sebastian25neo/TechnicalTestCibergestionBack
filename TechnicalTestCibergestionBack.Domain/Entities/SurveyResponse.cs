namespace TechnicalTestCibergestionBack.Domain.Entities
{
    public class SurveyResponse
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Score { get; set; } // 0 a 10
        public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;
    }
}
