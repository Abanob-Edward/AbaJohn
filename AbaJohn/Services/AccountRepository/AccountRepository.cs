using AbaJohn.Models;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace AbaJohn.Services.AccountRepository
{
    public class AccountRepository : IAccountRepository
    {

        private readonly ApplicationDbContext context;
        public AccountRepository(ApplicationDbContext _context)
        {
            context = _context;
        }

        public List<IdentityRole> get_all_roles()
        {
            var cat = context.Roles.ToList();
            return (cat);
        }
    }
}