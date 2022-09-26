using FluentValidation;
using Myteka.Models.InternalModels;

namespace Myteka.Web.Validators;

public class AuthorValidator : AbstractValidator<Author>
{
    public AuthorValidator()
    {
        RuleFor(author => author.Name).NotEmpty().WithMessage("Имя не может быть пустым");
        RuleFor(author => author.Surname).NotEmpty().WithMessage("Фамилию необходимо заполнить");
        RuleFor(author => author.Tags).Must(tags => tags.Length < 3).WithMessage("Минимальное количество тегов 3");
    }
}