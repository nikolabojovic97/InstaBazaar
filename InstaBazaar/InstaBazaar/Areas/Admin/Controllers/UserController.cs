using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InstaBazaar.Data.Data.Repository.IRepository;
using InstaBazaar.Models;
using InstaBazaar.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InstaBazaar.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private const int PageSize = 10;

        public UserController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<ActionResult> Index(int page = 1, string search = null)
        {
            ViewModel<User> vm = new ViewModel<User>();

            var users = await unitOfWork.User.GetAllUsersAsync(orderBy: (list) => list.OrderBy(x => x.UserName));

            if (!string.IsNullOrEmpty(search))
            {
                vm.Search = search;
                users = unitOfWork.User.Search(users, search);
            }

            vm.Title = "Korisnici";
            vm.SubTitle = "Svi registrovani korisnici";
            vm.List = users.Skip((page - 1) * PageSize).Take(PageSize).ToList();
            vm.PagingInfoViewModel = new PagingInfoViewModel { CurrentPage = page, ItemsPerPage = PageSize, TotalItems = users.Count() };

            return View(vm);
        }

        // GET: UserController/Details/xcvbnk
        public async Task<ActionResult> Details(string id)
        {
            var user = await unitOfWork.User.GetFirstOrDefaultAsync(filter: x => x.Id.Equals(id), includeProperties: "InstagramAccounts");
            if (user == null)
                return new NotFoundResult();

            return View(user);
        }

        // GET: UserController/Delete/xdcfvgb
        public async Task<ActionResult> Delete(string id)
        {
            var user = await unitOfWork.User.GetFirstOrDefaultAsync(filter: x => x.Id.Equals(id), includeProperties: "InstagramAccounts");
            if (user == null)
                return new NotFoundResult();

            return View(user);
        }

        // POST: UserController/Delete/cfvgbh
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteUser(string id)
        {
            try
            {
                var user = await unitOfWork.User.GetFirstOrDefaultAsync(filter: x => x.Id.Equals(id));
                if (user == null)
                    return new NotFoundResult();

                await unitOfWork.User.RemoveAsync(user);
                await unitOfWork.SaveAsync();
            }
            catch
            {
                return View();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
