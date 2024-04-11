using AvicLimited.Web.Models;
using AvicLimited.Web.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AvicLimited.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class AccountsController : Controller
    {
        private readonly IAccountRepository _accountRepository;

        public AccountsController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        

    }
}
