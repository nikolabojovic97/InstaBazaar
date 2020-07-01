using System;
using System.Collections.Generic;
using System.Data;
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
    public class BrandController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private const int PageSize = 10;

        public BrandController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // GET: BrandController
        public ActionResult Index(int page = 1, string search = null)
        {
            ViewModel<Brand> vm = new ViewModel<Brand>();

            IEnumerable<Brand> brands;

            if (!string.IsNullOrEmpty(search))
            {
                vm.Search = search;
                brands = unitOfWork.Brand.Search(search);
            }
            else
                brands = unitOfWork.Brand.GetAll();

            vm.Title = "Brendovi";
            vm.SubTitle = "Svi registrovani brendovi";
            vm.List = brands.Skip((page - 1) * PageSize).Take(PageSize).ToList();
            vm.PagingInfoViewModel = new PagingInfoViewModel { CurrentPage = page, ItemsPerPage = PageSize, TotalItems = brands.Count() };

            return View(vm);
        }

        // GET: BrandController/Details/5
        public ActionResult Details(int id)
        {
            var brand = unitOfWork.Brand.GetFirstOrDefault(filter: x=> x.Id == id, includeProperties: "User");
            if (brand == null)
                return new NotFoundResult();

            return View(brand);
        }

        // GET: BrandController/Delete/5
        public ActionResult Delete(int id)
        {
            var brand = unitOfWork.Brand.GetFirstOrDefault(filter: x => x.Id == id, includeProperties: "User");
            if (brand == null)
                return new NotFoundResult();

            return View(brand);
        }

        // POST: BrandController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteBrand(int id)
        {
            try
            {
                var brand = unitOfWork.Brand.Get(id);
                if (brand == null)
                    return new NotFoundResult();

                unitOfWork.Brand.Remove(brand);
                unitOfWork.Save();
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
