using Microsoft.AspNetCore.Identity;

namespace AbaJohn.Services.AccountRepository
{
    public interface IAccountRepository
    {
        List<IdentityRole> get_all_roles();
    }
}