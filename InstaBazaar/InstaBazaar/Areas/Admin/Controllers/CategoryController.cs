using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using InstaBazaar.Data.Data.Repository.IRepository;
using InstaBazaar.Models;
using InstaBazaar.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Hosting;

namespace InstaBazaar.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IWebHostEnvironment hostEnvironment;
        private const int PageSize = 10;

        public CategoryController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            this.unitOfWork = unitOfWork;
            this.hostEnvironment = hostEnvironment;
        }

        // GET: CategoryController
        public ActionResult Index(int page = 1, string search = null)
        {
            ViewModel<Category> vm = new ViewModel<Category>();

            IEnumerable<Category> categories;

            if (!string.IsNullOrEmpty(search))
            {
                vm.Search = search;
                categories = unitOfWork.Category.Search(search);
            }
            else
                categories = unitOfWork.Category.GetAll();

            vm.Title = "Kategorije";
            vm.SubTitle = "Sve registrovane kategorije";
            vm.List = categories.Skip((page - 1) * PageSize).Take(PageSize).ToList();
            vm.PagingInfoViewModel = new PagingInfoViewModel { CurrentPage = page, ItemsPerPage = PageSize, TotalItems = categories.Count() };

            return View(vm);
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
        public ActionResult Create([Bind(include: "Name, Description")]Category category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string webRootPath = hostEnvironment.WebRootPath;
                    var file = HttpContext.Request.Form.Files.First();
                    string fileName = Guid.NewGuid().ToString();
                    var uploadPath = Path.Combine(webRootPath, @"images\categories");
                    var extension = Path.GetExtension(file.FileName);

                    var fileStream = new FileStream(Path.Combine(uploadPath, fileName + extension), FileMode.Create);
                    file.CopyToAsync(fileStream);

                    category.ImageUrl = @"\images\categories\" + fileName + extension;

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
        public async Task<ActionResult> EditAsync(int id, [Bind(include: "Id, Name, Description")] Category category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var categoryDb = unitOfWork.Category.Get(id);
                    if (categoryDb == null)
                        return new NotFoundResult();

                    var files = HttpContext.Request.Form.Files;
                    if (files.Count > 0)
                        category.ImageUrl = await unitOfWork.Category.SaveImageAsync(files.First(), categoryDb.ImageUrl);
                   

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
