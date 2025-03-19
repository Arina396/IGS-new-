using IGS.DAL;
using IGS.Domain.Entity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace IGS.Service.Implementations
{
    public class AddGameService
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public AddGameService(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        //public async Task AddGameAsync(AddGameFormModel model)
        //{
        //    try
        //    {
        //        // Сохранение файлов
        //        string coverFileName = await SaveFileAsync(model.Cover);
        //        string screenshotFileName = await SaveFileAsync(model.Screenshot);

        //        // Создание объекта игры
        //        var game = new Game
        //        {
        //            Name = model.Name,
        //            Description = model.ShortDescription,
        //            Creator = "Creator", // Замените на актуальное значение
        //            ImageName = coverFileName,
        //            Genre = "Genre", // Замените на актуальное значение
        //            ScrinshotName = screenshotFileName,
        //            Link = "Link", // Замените на актуальное значение
        //            User_Id = 2 // Замените на актуальное значение
        //        };

        //        // Сохранение в БД
        //        _context.Games.Add(game);
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Ошибка при сохранении игры: {ex.Message}");
        //        throw;
        //    }
        //}

        private async Task<string> SaveFileAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return null;

            // Путь для сохранения файла
            string uploadsFolder = Path.Combine(_environment.WebRootPath, "images");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            // Уникальное имя файла
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            // Сохранение файла
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return uniqueFileName;
        }
    }
}