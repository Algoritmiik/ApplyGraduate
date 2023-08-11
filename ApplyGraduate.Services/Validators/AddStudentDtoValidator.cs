using ApplyGraduate.Data.Concrete.EntityFramework.Repositories;
using ApplyGraduate.Entities.Concrete;
using ApplyGraduate.Entities.Dtos.StudentDtos;
using ApplyGraduate.Services.Abstract;
using ApplyGraduate.Services.Concrete;
using FluentValidation;

namespace ApplyGraduate.Services.Validators
{
    public class AddStudentDtoValidator : AbstractValidator<AddStudentDto>
    {
        public AddStudentDtoValidator()
        {
            RuleFor(e => e.GownRequest).NotNull().WithMessage("Cübbe talebi boş bırakılamaz!");

            RuleFor(e => e.GownSize).Must(SizeChecker).WithMessage("Lütfen belirtilen ölçülerden birini seçiniz!");

            RuleFor(e => e.Note).MaximumLength(300).WithMessage("Notunuz 300 karakterden fazla olamaz!");
        }

        
        private bool SizeChecker(string size)
        {
            if (size == null || size == "XS" || size == "S" || size == "M" || size == "L" || size == "XL" || size == "XXL")
            {
                return true;
            }

            return false;
        }
    }
}