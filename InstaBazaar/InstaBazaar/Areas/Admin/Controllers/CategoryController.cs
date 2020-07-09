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
        public async Task<ActionResult> Index(int page = 1, string search = null)
        {
            ViewModel<Category> vm = new ViewModel<Category>();

            var categories = await unitOfWork.Category.GetAllAsync();

            if (!string.IsNullOrEmpty(search))
            {
                vm.Search = search;
                categories = unitOfWork.Category.Search(categories, search);
            }

            vm.Title = "Kategorije";
            vm.SubTitle = "Sve registrovane kategorije";
            vm.List = categories.Skip((page - 1) * PageSize).Take(PageSize).ToList();
            vm.PagingInfoViewModel = new PagingInfoViewModel { CurrentPage = page, ItemsPerPage = PageSize, TotalItems = categories.Count() };

            return View(vm);
        }

        // GET: CategoryController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var category = await unitOfWork.Category.GetAsync(id);
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
        public async Task<ActionResult> Create([Bind(include: "Name, Description")]Category category)
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
                    await file.CopyToAsync(fileStream);

                    category.ImageUrl = @"\images\categories\" + fileName + extension;

                    await unitOfWork.Category.AddAsync(category);
                    await unitOfWork.SaveAsync();
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
        public async Task<ActionResult> Edit(int id)
        {
            var category = await unitOfWork.Category.GetAsync(id);
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
                    var categoryDb = await unitOfWork.Category.GetAsync(id);
                    if (categoryDb == null)
                        return new NotFoundResult();

                    var files = HttpContext.Request.Form.Files;
                    if (files.Count > 0)
                        category.ImageUrl = await unitOfWork.Category.SaveImageAsync(files.First(), categoryDb.ImageUrl);
                   

                    await unitOfWork.Category.UpdateAsync(category);
                    await unitOfWork.SaveAsync();
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
        public async Task<ActionResult> Delete(int id)
        {
            var category = await unitOfWork.Category.GetAsync(id);
            if (category == null)
                return new NotFoundResult();

            return View(category);
        }

        // POST: CategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            try
            {
                var category = await unitOfWork.Category.GetAsync(id);
                if (category == null)
                    return new NotFoundResult();

                await unitOfWork.Category.RemoveAsync(category);
                await unitOfWork.SaveAsync();
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
