namespace cTime.Core.Results
{
    public class ResultData<T>
    {
        public ResultState Status { get; set; }

        public T Data { get; set; }

        public string Message { get; set; }
    }
}