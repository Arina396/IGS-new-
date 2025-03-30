using IGS.DAL;
using IGS.DAL.Interfaces; // Добавьте пространство имен для IUserRepository
using IGS.Domain.Entity;
using IGS.Domain.ViewModels.Game;
using Microsoft.AspNetCore.Authorization; // Для атрибута [Authorize]
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
        private readonly IUserRepository _userRepository; // Добавляем зависимость

        // Обновляем конструктор
        public AddGameFormController(ApplicationDbContext context, IWebHostEnvironment environment, IUserRepository userRepository)
        {
            _context = context;
            _environment = environment;
            _userRepository = userRepository;
        }

        // GET: Отображение формы добавления игры
        public IActionResult AddGameForm()
        {
            return View(new AddGameFormModel());
        }

        // POST: Обработка отправки формы
        [HttpPost]
        [Authorize] // Добавляем атрибут для ограничения доступа
        public async Task<IActionResult> AddGameForm(AddGameFormModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Получение логина текущего пользователя
                    var userLogin = User.Identity.Name;
                    var user = await _userRepository.GetByLogin(userLogin);

                    // Проверка, найден ли пользователь
                    if (user == null)
                    {
                        return Unauthorized(); // Возвращаем ошибку, если пользователь не найден
                    }

                    // Сохранение файлов (обложка и скриншоты)
                    string coverFileName = await SaveFileAsync(model.Cover);
                    string screenshotFileName = await SaveFileAsync(model.Screenshot);
                    string screenshotFileName2 = await SaveFileAsync(model.Screenshot2);
                    string screenshotFileName3 = await SaveFileAsync(model.Screenshot3);

                    // Создание объекта игры
                    var game = new Games2
                    {
                        Name = model.Name,
                        Description = model.ShortDescription,
                        LargeDescription = model.AdditionalDescription,
                        Creator = model.Creator,
                        ImageName = coverFileName,
                        Genre = model.Genre,
                        ScrinshotName = screenshotFileName,
                        ScrinshotName2 = screenshotFileName2,
                        ScrinshotName3 = screenshotFileName3,
                        Link = model.Link,
                        User_Id = user.Id // Используем Id текущего пользователя
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