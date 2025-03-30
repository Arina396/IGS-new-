using IGS.DAL;
using IGS.DAL.Interfaces;
using IGS.Domain.Entity;
using IGS.Domain.ViewModels.Jam;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace IGS.Controllers
{
    public class JamRegistrationController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserRepository _userRepository;

        public JamRegistrationController(ApplicationDbContext context, IUserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;
        }

        // GET: Отображение формы регистрации на джем
        public IActionResult JamRegistration()
        {
            return View(new JamRegistrationModel());
        }

        // POST: Обработка отправки формы
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> JamRegistration(JamRegistrationModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Шаг 1: Получение логина текущего пользователя
                    Console.WriteLine("Шаг 1: Получение логина пользователя.");
                    var userLogin = User.Identity.Name;
                    if (string.IsNullOrEmpty(userLogin))
                    {
                        throw new Exception("Логин пользователя не найден. Возможно, пользователь не авторизован.");
                    }

                    // Шаг 2: Получение данных пользователя из репозитория
                    Console.WriteLine("Шаг 2: Запрос данных пользователя из репозитория.");
                    var user = await _userRepository.GetByLogin(userLogin);
                    if (user == null)
                    {
                        Console.WriteLine("Ошибка: Пользователь с логином '{0}' не найден в базе данных.", userLogin);
                        return Unauthorized();
                    }

                    // Шаг 3: Создание объекта команды
                    Console.WriteLine("Шаг 3: Создание объекта JamTeam.");
                    var team = new JamTeam
                    {
                        Name = model.NameOfTeam,
                        Description = $"{model.NameOfCapitan}, {model.MembersOfTeam}",
                        //JamId = 0 // вернуть когда будет JamId
                    };

                    // Шаг 4: Добавление команды в контекст базы данных
                    Console.WriteLine("Шаг 4: Добавление команды в контекст базы данных.");
                    _context.JamTeam.Add(team);

                    // Шаг 5: Сохранение изменений в базе данных
                    Console.WriteLine("Шаг 5: Сохранение изменений в базе данных.");
                    await _context.SaveChangesAsync();

                    // Успешное завершение
                    Console.WriteLine("Регистрация прошла успешно.");
                    return RedirectToAction("Index", "Home");
                }
                catch (Exception ex)
                {
                    // Логирование ошибки с деталями
                    string errorMessage = $"Ошибка на этапе обработки: {ex.Message}";
                    if (ex.InnerException != null)
                    {
                        errorMessage += $" | Внутреннее исключение: {ex.InnerException.Message}";
                    }
                    Console.WriteLine(errorMessage);
                    Console.WriteLine($"Стек вызовов: {ex.StackTrace}");

                    // Добавление детализированной ошибки в ModelState
                    ModelState.AddModelError("", $"Произошла ошибка при регистрации на джем: {ex.Message}. Попробуйте еще раз.");
                }
            }
            else
            {
                // Логирование ошибок валидации
                var validationErrors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                Console.WriteLine("Ошибки валидации модели: " + string.Join("; ", validationErrors));
            }

            // Возвращаем форму с ошибками
            return View(model);
        }
    }
}