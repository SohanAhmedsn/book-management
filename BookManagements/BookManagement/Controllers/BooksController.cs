using BookManagement.Repositories.Interfaces;
using BookManagement.Repositories;
using Microsoft.AspNetCore.Mvc;
using BookManagement.Models;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using BookManagement.ViewModel;

namespace BookManagement.Controllers
{
    public class BooksController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IGenericRepository<Book> repo;
        private readonly IWebHostEnvironment env;
        public BooksController(IUnitOfWork unitOfWork, IWebHostEnvironment env)
        {
            this.unitOfWork = unitOfWork;
            this.env = env;
            this.repo = this.unitOfWork.GetRepo<Book>();
        }
        public async Task<IActionResult> Index(int pg = 1)
        {
            var data = await this.repo.GetAllAsync(
                    x => x.Include(y => y.Publishers)
                );
            var pagedData = await data.OrderBy(x => x.BookId).ToPagedListAsync(pg, 5);
            return View(pagedData);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(BookInputViewModel model)
        {
            if (ModelState.IsValid)
            {
                var book = new Book
                {
                    BookName = model.BookName,
                    Price = model.Price,
                    BookType = model.BookType
                };
                string ext = Path.GetExtension(model.Picture.FileName);
                string fileName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + ext;
                string savePath = Path.Combine(this.env.WebRootPath, "Images", fileName);
                FileStream fs = new FileStream(savePath, FileMode.Create, FileAccess.Write);
                await model.Picture.CopyToAsync(fs);
                fs.Close();
                book.Picture = fileName;
                await this.repo.InsertAsync(book);
                bool saved = await this.unitOfWork.SaveAsync();
                if (saved)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "Failed to Book Data");
                }

            }
            return View(model);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var data = await this.repo.GetByIdAsync(x => x.BookId == id);
            if (data == null) return NotFound();
            var modelData = new BookEditViewModel
            {
                BookId = data.BookId,
                BookName = data.BookName,
                BookType = data.BookType,
                Price = data.Price
            };
            ViewBag.CurrentPic = data.Picture;
            return View(modelData);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(BookEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var book = await this.repo.GetByIdAsync(x => x.BookId == model.BookId);
                if (book == null) return NotFound();
                book.BookName = model.BookName;
                book.Price = model.Price;
                book.BookType = model.BookType;
                if (model.Picture != null)
                {
                    string ext = Path.GetExtension(model.Picture.FileName);
                    string fileName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + ext;
                    string savePath = Path.Combine(this.env.WebRootPath, "Images", fileName);
                    FileStream fs = new FileStream(savePath, FileMode.Create, FileAccess.Write);
                    await model.Picture.CopyToAsync(fs);
                    fs.Close();
                    book.Picture = fileName;
                }
                await this.repo.UpdateAsync(book);
                bool saved = await this.unitOfWork.SaveAsync();
                if (saved)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "Failed to Book data Saved");
                }

            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if (ModelState.IsValid)
            {
                await repo.DeleteAsync(id);
                await this.unitOfWork.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
    }
}
