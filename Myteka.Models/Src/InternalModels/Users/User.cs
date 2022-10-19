using Microsoft.AspNetCore.Identity;

namespace Myteka.Models.InternalModels.Users;

public class User : IdentityUser
{
    public List<Guid>? LikedBooks { get; set; }
    public ICollection<ReadableBook>? ReadBooks { get; set; }
}