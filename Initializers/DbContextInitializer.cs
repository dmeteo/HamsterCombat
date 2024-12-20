﻿using CSharpClicker.Domain;
using CSharpClicker.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace CSharpClicker.Initializers;

public static class DbContextInitializer
{

    public static void AddAppDbContext(IServiceCollection services)
    {
        var pathToDbFile = GetPathToDbFile();

        services
              .AddDbContext<AppDbContext>(options => options
                .UseSqlite($"Data Source={pathToDbFile}"));


        string GetPathToDbFile()
        {
            var applicationFolder = Path.Combine(Environment.GetFolderPath(
              Environment.SpecialFolder.ApplicationData), "CSharpClicker");

            if (!Directory.Exists(applicationFolder))
            {
                Directory.CreateDirectory(applicationFolder);
            }
            return Path.Combine(applicationFolder, "CSharpClicker.db");
        }
    }

    public static void InitializeDbContext(AppDbContext appDbContext)
    {
        const string Boost1 = "Рудокоп";
        const string Boost2 = "Призрак";
        const string Boost3 = "Стражник";
        const string Boost4 = "Маг огня";
        const string Boost5 = "Рудный барон";

        appDbContext.Database.Migrate();

        var existingBoosts = appDbContext.Boosts
            .ToArray();

        AddBoostIfNotExist(Boost1, price: 100, profit: 1);
        AddBoostIfNotExist(Boost2, price: 500, profit: 15);
        AddBoostIfNotExist(Boost3, price: 2000, profit: 60, isAuto: true);
        AddBoostIfNotExist(Boost4, price: 10000, profit: 400);
        AddBoostIfNotExist(Boost5, price: 100000, profit: 5000, isAuto: true);

        const int limit = 13000000;
        const int asciiLimit = 127;
        const int symbolsLimit = 15;
        const int symbolsMin = 5;
        var random = new Random();

        AddRandomUser();

        appDbContext.SaveChanges();

        void AddRandomUser()
        {
            return;

            for (var i = 0; i < 200; i++)
            {
                var score = random.Next(limit);
                var symbolsCount = random.Next(symbolsMin, symbolsLimit);

                var userName = string.Empty;
                for (var j = 0; j < symbolsCount; j++)
                {
                    var character = random.Next(asciiLimit);
                    userName += (char)character;
                }
                appDbContext.Users.Add(new ApplicationUser
                {
                    UserName = userName,
                    RecordScore = score,
                });
            }

		}

        void AddBoostIfNotExist(string name, long price, long profit, bool isAuto = false)
        {
            if (!existingBoosts.Any(eb => eb.Title == name))
            {
                var pathToImage = Path.Combine(".", "Resources", "BoostImages", $"{name}.png");
                using var fileStream = File.OpenRead(pathToImage);
                using var memoryStream = new MemoryStream();

                fileStream.CopyTo(memoryStream);

                appDbContext.Boosts.Add(new Boost
                {
                    Title = name,
                    Price = price,
                    Profit = profit,
                    IsAuto = isAuto,
                    Image = memoryStream.ToArray(),
                });
            }
        }
    }
}
