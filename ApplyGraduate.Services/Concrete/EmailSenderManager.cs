using System.Net;
using System.Net.Mail;
using ApplyGraduate.Data.Utilities.Results.Abstract;
using ApplyGraduate.Data.Utilities.Results.ComplexTypes;
using ApplyGraduate.Data.Utilities.Results.Concrete;
using ApplyGraduate.Services.Abstract;
using Microsoft.Extensions.Configuration;

namespace ApplyGraduate.Services.Concrete
{
    public class EmailSenderManager : IEmailSenderService
    {
        private readonly IConfiguration _configuration;
        public EmailSenderManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IResult> MailSenderAsync()
        {
            var mailConfigs = _configuration.GetSection("EmailConfiguration");

            SmtpClient smtp = new();
            smtp.Credentials = new NetworkCredential(mailConfigs["FromMail"], mailConfigs["FromPassword"]);
            smtp.Port = int.Parse(mailConfigs["SmtpPort"]);
            smtp.Host = mailConfigs["SmtpServer"];
            smtp.EnableSsl = bool.Parse(mailConfigs["SmtpUseSSL"]);
            smtp.Timeout = int.Parse(mailConfigs["TimeOut"]) * 1000;

            MailMessage mail = new();
            mail.To.Add("mertcanaslan198@gmail.com");
            mail.From = new MailAddress("alert@fsm.edu.tr");
            mail.Subject = "Deneme";
            mail.Body = "Bu bir deneme mailidir";

            try
            {
                smtp.Send(mail);
                return new Result(ResultStatus.SUCCESS);
            }
            catch (System.Exception exception)
            {
                return new Result(ResultStatus.ERROR, exception.ToString());
            }
        }
    }
}