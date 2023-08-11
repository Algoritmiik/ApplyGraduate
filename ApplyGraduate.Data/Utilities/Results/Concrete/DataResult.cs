using ApplyGraduate.Data.Utilities.Results.Abstract;
using ApplyGraduate.Data.Utilities.Results.ComplexTypes;

namespace ApplyGraduate.Data.Utilities.Results.Concrete
{
    public class DataResult<T> : IDataResult<T>
    {
        public DataResult(ResultStatus resultStatus, T data)
        {
            ResultStatus = resultStatus;
            Data = data;
        }

        public DataResult(ResultStatus resultStatus, List<T> datas)
        {
            ResultStatus = resultStatus;
            Datas = datas;
        }

        public DataResult(ResultStatus resultStatus, string message, T data)
        {
            ResultStatus = resultStatus;
            Message = message;
            Data = data;
        }

        public DataResult(ResultStatus resultStatus, string message, List<T> datas)
        {
            ResultStatus = resultStatus;
            Message = message;
            Datas = datas;
        }

        public DataResult(ResultStatus resultStatus, string message, T data, Exception exception)
        {
            ResultStatus = resultStatus;
            Message = message;
            Data = data;
            Exception = exception;
        }

        public DataResult(ResultStatus resultStatus, string message, List<T> datas, Exception exception)
        {
            ResultStatus = resultStatus;
            Message = message;
            Datas = datas;
            Exception = exception;
        }
        
        public ResultStatus ResultStatus { get; }
        public string Message { get; }
        public Exception Exception { get; }
        public T Data { get; }
        public List<T> Datas { get; }
    }
}