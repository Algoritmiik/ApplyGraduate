using ApplyGraduate.Entities.Dtos.CompanionDtos;
using FluentValidation;

namespace ApplyGraduate.Services.Validators
{
    public class AddCompanionDtoValidator : AbstractValidator<AddCompanionDto>
    {
        public AddCompanionDtoValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Lütfen refakatçinizin ismini giriniz!");
            RuleFor(c => c.Name).MaximumLength(50).WithMessage("Refakatçinizin ismi en fazla 50 karakter olabilir!");
            
            RuleFor(c => c.Surname).NotEmpty().WithMessage("Lütfen refakatçinizin ismini giriniz!");
            RuleFor(c => c.Surname).MaximumLength(50).WithMessage("Refakatçinizin ismi en fazla 50 karakter olabilir!");

            RuleFor(c => c.HesCode).NotEmpty().NotNull().WithMessage("Lütfen refakatçinizin HES kodunu giriniz!");
            RuleFor(c => c.HesCode).Length(10).WithMessage("HES Kodu 10 karakterli olmalıdır!");
            
            RuleFor(c => c.PhoneNumber).NotEmpty().WithMessage("Lütfen refakatçinizin teşefon numarasını giriniz!");
            RuleFor(c => c.PhoneNumber).Length(10).WithMessage("Telefon numarası 10 adet rakamdan oluşmalıdır!");
        }
    }
}