using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InstaBazaar.Data.Data.Repository.IRepository;
using InstaBazaar.Models;
using InstaBazaar.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InstaBazaar.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class InfluencerController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private const int PageSize = 10;

        public InfluencerController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // GET: InfluencerController
        public ActionResult Index(int page = 1, List<Category> categories = null, string search = null)
        {
            ViewModel<InstagramAccount> vm = new ViewModel<InstagramAccount>();

            IEnumerable<InstagramAccount> accounts = unitOfWork.InstagramAccount.GetAll(includeProperties: "Category");

            if (categories != null)
                accounts = unitOfWork.InstagramAccount.GetByCategories(accounts, categories);

            if (!string.IsNullOrEmpty(search))
                accounts = unitOfWork.InstagramAccount.Search(accounts, search);

            vm.Title = "Instagram profili influensera";
            vm.SubTitle = "Svi registrovani Instagram profili";
            vm.List = accounts.Skip((page - 1) * PageSize).Take(PageSize).ToList();
            vm.PagingInfoViewModel = new PagingInfoViewModel() { CurrentPage = page, ItemsPerPage = PageSize, TotalItems = accounts.Count() };

            return View(vm);
        }

        // GET: InfluencerController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: InfluencerController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: InfluencerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: InfluencerController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: InfluencerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: InfluencerController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: InfluencerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
