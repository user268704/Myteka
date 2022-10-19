using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Myteka.Models.InternalModels.Users;

namespace Myteka.Web.Data;

public class IdentityContext : IdentityDbContext<User>
{
    public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
    {
    }
}