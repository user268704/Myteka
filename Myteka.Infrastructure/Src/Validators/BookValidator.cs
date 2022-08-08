using FluentValidation;
using Myteka.Infrastructure.Data.Interfaces;
using Myteka.Models.InternalModels;

namespace Myteka.Infrastructure.Validators;

public class BookValidator : AbstractValidator<Book>
{
    public BookValidator()
    {
        // Правила валидации книги
        RuleFor(book => book.Title).NotEmpty().WithMessage("Не указано название книги");
        RuleFor(book => book.AuthorId).NotEmpty().WithMessage("Не указан автор книги");
        RuleFor(book => book.Theme).NotEmpty().WithMessage("Не указана тема книги");
        RuleFor(book => book.Tags.Length).Must(len => len < 30).WithMessage("Количество тегов не должно превышать 30");
    }
}