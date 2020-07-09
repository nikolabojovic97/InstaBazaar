using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using InstaBazaar.Data.Data.Repository.IRepository;
using InstaBazaar.Models;
using InstaBazaar.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InstaBazaar.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class InstagramAccountController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private const int PageSize = 10;

        public InstagramAccountController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // GET: InstagramAccountController
        public async Task<ActionResult> Index(int page = 1, List<Category> categories = null, string search = null)
        {
            ViewModel<InstagramAccount> vm = new ViewModel<InstagramAccount>();

            var accounts = await unitOfWork.InstagramAccount.GetAllAsync(orderBy: (list) => list.OrderBy(x => x.IgUserName), includeProperties: "Category");

            if (categories != null && categories.Count > 0)
                accounts = unitOfWork.InstagramAccount.GetByCategories(accounts, categories);

            if (!string.IsNullOrEmpty(search))
            {
                vm.Search = search;
                accounts = unitOfWork.InstagramAccount.Search(accounts, search);
            }

            vm.Title = "Instagram profili influensera";
            vm.SubTitle = "Svi registrovani Instagram profili";
            vm.List = accounts.Skip((page - 1) * PageSize).Take(PageSize).ToList();
            vm.PagingInfoViewModel = new PagingInfoViewModel() { CurrentPage = page, ItemsPerPage = PageSize, TotalItems = accounts.Count() };

            return View(vm);
        }

        // GET: InstagramAccountController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var account = await unitOfWork.InstagramAccount.GetFirstOrDefaultAsync(filter: x => x.Id == id, includeProperties: "User");
            if (account == null)
                return new NotFoundResult();

            return View(account);
        }

        // GET: InstagramAccountController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var account = await unitOfWork.InstagramAccount.GetFirstOrDefaultAsync(filter: x => x.Id == id, includeProperties: "User");
            if (account == null)
                return new NotFoundResult();

            return View(account);
        }

        // POST: InstagramAccountController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteAccount(int id)
        {
            try
            {
                var account = await unitOfWork.InstagramAccount.GetAsync(id);
                if (account == null)
                    return new NotFoundResult();

                await unitOfWork.InstagramAccount.RemoveAsync(account);
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
