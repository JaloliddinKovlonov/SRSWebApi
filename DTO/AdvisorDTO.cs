namespace SRSWebApi.DTO
{
    public class AdvisorCreateDTO
    {
        public int? ProfessorId { get; set; }
    }

    public class AdvisorUpdateDTO
    {
        public int AdvisorId { get; set; }
        public int? ProfessorId { get; set; }
    }
}
