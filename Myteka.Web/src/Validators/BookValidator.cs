using FluentValidation;
using Myteka.Models.ExternalModels.RegisterModels;
using Myteka.Models.InternalModels;

namespace Myteka.Web.Validators;

public class BookValidator : AbstractValidator<BookRegisterModel>
{
    public BookValidator()
    {
        // Правила валидации книги
        RuleFor(book => book.Title).NotEmpty().WithMessage("Не указано название книги");
        RuleFor(book => book.AuthorId).NotEmpty().WithMessage("Не указан автор книги");
        RuleFor(book => book.Genre).NotEmpty().WithMessage("Не указан жанр книги");
        RuleFor(book => book.Tags.Length).Must(len => len < 30).WithMessage("Количество тегов не должно превышать 30");
        RuleFor(book => book.WritingDate < DateTime.Now).Equal(true).WithMessage("Дата написания не может быть больше текущей");
    }
}