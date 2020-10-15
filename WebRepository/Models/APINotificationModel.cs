namespace WebRepository.Models
{
    public class APINotificationModel
    {
        public string Message { get; set; }
        public APINotificationModel(string message)
        {
            Message = message;
        }
    }
}
