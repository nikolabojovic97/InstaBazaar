using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
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
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private const int PageSize = 10;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // GET: CategoryController
        public ActionResult Index(int page = 1, string search = null)
        {
            CategoryViewModel cvm = new CategoryViewModel();

            var categories = unitOfWork.Category.GetAll();

            if (!string.IsNullOrEmpty(search))
            {
                cvm.Search = search;
                categories = unitOfWork.Category.Search(search);
            }

            cvm.Title = "Kategorije";
            cvm.SubTitle = "Spisak svih registrovanih kategorija";
            cvm.Categories = categories.Skip((page - 1) * PageSize).Take(PageSize).ToList();
            cvm.PagingInfoViewModel = new PagingInfoViewModel { CurrentPage = page, ItemsPerPage = PageSize, TotalItems = categories.Count() };

            return View(cvm);
        }

        // GET: CategoryController/Details/5
        public ActionResult Details(int id)
        {
            var category = unitOfWork.Category.Get(id);
            if (category == null)
                return new NotFoundResult();

            return View(category);
        }

        // GET: CategoryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(include: "Name, Description")] Category category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    unitOfWork.Category.Add(category);
                    unitOfWork.Save();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(category);
        }

        // GET: CategoryController/Edit/5
        public ActionResult Edit(int id)
        {
            var category = unitOfWork.Category.Get(id);
            if (category == null)
                return new NotFoundResult();

            return View(category);
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind(include: "Id, Name, Description")] Category category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (unitOfWork.Category.Get(id) == null)
                        return new NotFoundResult();

                    unitOfWork.Category.Update(category);
                    unitOfWork.Save();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(category);
        }

        // GET: CategoryController/Delete/5
        public ActionResult Delete(int id)
        {
            var category = unitOfWork.Category.Get(id);
            if (category == null)
                return new NotFoundResult();

            return View(category);
        }

        // POST: CategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCategory(int id)
        {
            try
            {
                var category = unitOfWork.Category.Get(id);
                if (category == null)
                    return new NotFoundResult();

                unitOfWork.Category.Remove(category);
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
