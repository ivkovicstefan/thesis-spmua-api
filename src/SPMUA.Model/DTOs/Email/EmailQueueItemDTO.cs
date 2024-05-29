namespace SPMUA.Model.DTOs.Email
{
    public class EmailQueueItemDTO
    {
        public string ToEmail { get; set; } = string.Empty;
        public string EmailSubject { get; set; } = string.Empty;
        public string EmailBody { get; set; } = string.Empty;
    }
}
