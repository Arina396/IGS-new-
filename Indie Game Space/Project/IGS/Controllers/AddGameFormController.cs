using IGS.DAL;
using IGS.Domain.Entity;
using IGS.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace IGS.Controllers
{
    public class AddGameFormController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public AddGameFormController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: Отображение формы добавления игры
        public IActionResult AddGameForm()
        {
            return View(new AddGameFormModel());
        }

        // POST: Обработка отправки формы
        [HttpPost]
        public async Task<IActionResult> AddGameForm(AddGameFormModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Сохранение файлов (обложка и скриншот)
                    string coverFileName = await SaveFileAsync(model.Cover);
                    string screenshotFileName = await SaveFileAsync(model.Screenshot);
                    

                    // Создание объекта игры
                    var game = new Games2
                    {
                 
                        Name = model.Name,
                        Description = model.ShortDescription,
                        Creator = "Creator", // Замените на актуальное значение
                        ImageName = coverFileName,
                        Genre = "Genre", // Замените на актуальное значение
                        ScrinshotName = screenshotFileName,
                        Link = model.Link, // Замените на актуальное значение
                        User_Id = 7// Замените на актуальное значение
                    };

                    // Сохранение данных в базу данных
                    _context.Games2.Add(game);
                    await _context.SaveChangesAsync();

                    // Перенаправление на главную страницу после успешного сохранения
                    return RedirectToAction("Index", "Home");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                    ModelState.AddModelError("", "Произошла ошибка при сохранении игры. Попробуйте еще раз.");
                    if (ex.InnerException != null)
                    {
                        Console.WriteLine($"Внутреннее исключение: {ex.InnerException.Message}");
                    }
                }
            }

            // Если валидация не прошла, возвращаем форму с ошибками
            return View(model);
        }

        // Вспомогательный метод для сохранения файла
        private async Task<string> SaveFileAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return null;

            // Путь для сохранения файлов
            string uploadsFolder = Path.Combine(_environment.WebRootPath, "img");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            // Генерация уникального имени файла
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            // Сохранение файла на сервер
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return uniqueFileName;
        }
    }
}