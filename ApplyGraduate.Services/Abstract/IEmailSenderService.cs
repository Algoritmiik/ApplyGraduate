using ApplyGraduate.Data.Utilities.Results.Abstract;

namespace ApplyGraduate.Services.Abstract
{
    public interface IEmailSenderService
    {
        Task<IResult> MailSenderAsync();
    }
}