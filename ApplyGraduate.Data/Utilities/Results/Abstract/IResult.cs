using ApplyGraduate.Data.Utilities.Results.ComplexTypes;

namespace ApplyGraduate.Data.Utilities.Results.Abstract
{
    public interface IResult
    {
        public ResultStatus ResultStatus { get; }
        public string Message { get; }
        public Exception Exception { get; }
    }
}