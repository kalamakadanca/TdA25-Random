namespace TourDeApp.Models.Schemas
{
    public class Error
    {
        public int code { get; set; }
        public string message { get; set; }

        public Error(int _code = 000, string _message = "No message")
        {
            code = _code;
            message = _message;
        }
    }
}
