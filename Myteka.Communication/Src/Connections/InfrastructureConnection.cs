using Myteka.Communication.Controllers.Infrastructure;

namespace Myteka.Communication;

public class InfrastructureConnection : Init
{
    public BookController Book { get; set; }
    public AudioController Audio { get; set; }
    public AuthorController Author { get; set; }
    public ContentController Content { get; set; }
    public UserController User { get; set; }
}