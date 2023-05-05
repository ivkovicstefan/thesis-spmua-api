namespace SPMUA.Model.DTOs.WorkingDay
{
    public class WorkingDayDTO
    {
        public int WorkingDayId { get; set; }
        public string WorkingDayName { get; set; } = String.Empty;
        public bool IsActive { get; set; }
        public TimeOnly? StartTime { get; set; }
        public TimeOnly? EndTime { get; set; }
    }
}
