using System.Text.Json.Serialization;

namespace CustomerAutomationWeb.Models
{
    public class ResponseMessages<T>
    {
        public T Data { get; set; }
        public int Status { get; set; }
        public string ErrorMessages { get; set; }

        [JsonConstructor]
        public ResponseMessages(T data, int status)
        {
            this.Data = data;
            this.Status = status;
        }
        public ResponseMessages(string errMsg, int status)
        {
            this.Status = status;
            this.ErrorMessages = errMsg;
        }
        public ResponseMessages(int status)
        {
            this.Status = status;
        }
        public static ResponseMessages<T> Success(T data, int status)
        {
            return new ResponseMessages<T>(data, status);
        }
        public static ResponseMessages<T> Fail(string errMsg, int status)
        {
            return new ResponseMessages<T>(errMsg, status);
        }
        public static ResponseMessages<T> SuccessNoContent()
        {
            return new ResponseMessages<T>(204);
        }
        public static ResponseMessages<T> FailNoContent(string errMsg, int status)
        {
            return new ResponseMessages<T>(errMsg, status);
        }
    }
}
