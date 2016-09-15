namespace cTime.Core.Results
{
    public class Result<T>
    {
        public static ResultData<T> Success(T data)
        {
            return new ResultData<T>
            {
                Status = ResultState.Success,
                Data = data
            };
        }

        public static ResultData<T> Error(string message)
        {
            return new ResultData<T>
            {
                Status = ResultState.Error,
                Message = message
            };
        }

        
    }
}