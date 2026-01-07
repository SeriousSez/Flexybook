namespace Flexybook.Domain.Responses
{
    public class BaseResponse
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }

        public BaseResponse()
        {
            Created = DateTime.Now;
        }
    }
}
