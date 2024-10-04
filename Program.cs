
using Microsoft.EntityFrameworkCore;
using System;

//class Program
//{
//    //Необходимо загрузить данные о пользователе с сервера или локальной базы данных, используя асинхронный метод.
//    //Если в процессе загрузки возникает ошибка(например, сервер не отвечает), необходимо обработать эту ошибку и сообщить пользователю о проблеме.
//    //Выполните симуляцию зависания сервера и отмените операцию, если запрос к серверу выполняется дольше 10 секунд.
//    static async Task Main(string[] args)
//    {
//        await LoadUserDataAsync(1);
//    }
//    static async Task LoadUserDataAsync(int userId)
//    {
//        using (var db = new AppDbContext())
//        using (var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(10)))
//        {
//            try
//            {
//                var user = await db.Users
//                    .Where(u => u.Id == userId)
//                    .FirstOrDefaultAsync(cancellationTokenSource.Token);

//                if (user != null)
//                {
//                    Console.WriteLine($"User: {user.Name}, Email: {user.Email}");
//                }
//                else
//                {
//                    Console.WriteLine("Пользователь не найден.");
//                }
//            }
//            catch (TaskCanceledException)
//            {
//                Console.WriteLine("Запрос был отменён из-за превышения времени ожидания.");
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"Произошла ошибка при загрузке данных: {ex.Message}");
//            }
//        }
//    }
//}
//public class User
//{
//    public int Id { get; set; }
//    public string Name { get; set; }
//    public string Email { get; set; }
//}
//public class AppDbContext : DbContext
//{
//    public DbSet<User> Users { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//    {
//        optionsBuilder.UseSqlServer("Server=DESKTOP-C317JNM;Database=User;Trusted_Connection=True; TrustServerCertificate=True;");
//    }
//}




//Реализовать возможность перехода по папкам в заданной директории. Пользователь вводит адрес, директории,
//отображаются все папки и файлы из данной директории. Пользователь может ввести название папки,
//необходимо отобразить все содержимое папки.
//class Program
//{
//    static void Main()
//    {
//        Console.WriteLine("Введите путь к директории:");
//        string currentDirectory = Console.ReadLine();

//        while (true)
//        {
//            if (Directory.Exists(currentDirectory))
//            {
//                string[] directories = Directory.GetDirectories(currentDirectory);
//                string[] files = Directory.GetFiles(currentDirectory);

//                DisplayDirectoryContents(directories, files);

//                Console.WriteLine("\nВведите название папки для перехода или '..' для возврата на уровень выше:");
//                string input = Console.ReadLine();
//                if (input == "..")
//                {
//                    currentDirectory = Directory.GetParent(currentDirectory)?.FullName ?? currentDirectory;
//                }
//                else
//                {
//                    string newDirectory = Path.Combine(currentDirectory, input);

//                    if (Directory.Exists(newDirectory))
//                    {
//                        currentDirectory = newDirectory;
//                    }
//                    else
//                    {
//                        Console.WriteLine("Такой папки не существует.");
//                    }
//                }
//            }
//            else
//            {
//                Console.WriteLine("Путь недействителен. Попробуйте еще раз.");
//                currentDirectory = Console.ReadLine();
//            }
//        }
//    }

//    static void DisplayDirectoryContents(string[] directories, string[]files)
//    {
//        Console.Clear();
//        Console.WriteLine("\nПапки:");
//        foreach (string dir in directories)
//        {
//            Console.WriteLine(" - " + Path.GetFileName(dir));
//        }
//        Console.WriteLine("\nФайлы:");
//        foreach (string file in files)
//        {
//            Console.WriteLine(" - " + Path.GetFileName(file));
//        }
//    }
//}


//У вас есть папка на вашем компьютере, в которой находится несколько сотен файлов разных типов.
//Вам необходимо создать программу на C#, которая должна считать названия файлов и отсортировать в соответствии с их типом.





class Program
{
    static async Task Main(string[] args)
    {
        // Создаем список для хранения исключений
        List<Exception> exceptions = new List<Exception>();

        // Вызываем метод несколько раз
        try
        {
            await Task.WhenAll(
                ExecuteAsync(new string[] { "name1", "name2", "name3", "name4", "name5" }, exceptions),
                ExecuteAsync(new string[] { "name1", "name2", "name3", "name4", "name5" }, exceptions),
                ExecuteAsync(new string[] { "name1", "name2", "name3", "name4", "name1" }, exceptions)
            );
        }
        catch (AggregateException ex)
        {
            // В случае агрегации исключений, выводим их
            foreach (var innerEx in ex.InnerExceptions)
            {
                Console.WriteLine(innerEx.Message);
            }
        }

        // Вывод всех собранных исключений
        if (exceptions.Any())
        {
            Console.WriteLine("Ошибки:");
            foreach (var exception in exceptions)
            {
                Console.WriteLine(exception.Message);
            }
        }
        else
        {
            Console.WriteLine("Ошибок нет.");
        }
    }

    static async Task ExecuteAsync(string[] names, List<Exception> exceptions)
    {
        await Task.Run(() =>
        {
            try
            {
                // Проверяем на наличие дубликатов имен
                if (names.Distinct().Count() != names.Length)
                {
                    throw new InvalidOperationException("Имена не должны повторяться: " + string.Join(", ", names));
                }
                // Если дубликатов нет, выводим имена
                Console.WriteLine("Имена: " + string.Join(", ", names));
            }
            catch (Exception ex)
            {
                // Добавляем исключение в список
                exceptions.Add(ex);
            }
        });
    }
}