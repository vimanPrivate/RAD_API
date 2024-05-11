namespace UserExperienceAnalizer.API.Models
{
    public class CommonRespond<T>
    {
        public Response Response { get; set; }
        public T Data { get; set; }
    }

    public class Response
    {
        public string Message { get; set; }
    }
}
