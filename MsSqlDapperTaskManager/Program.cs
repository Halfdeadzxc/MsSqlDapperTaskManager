using DAL.Factory;
using DAL.Models;
namespace MsSqlDapperTaskManager
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var connectionString = "Server=localhost;Database=TaskManager;User Id=SA;Password=YourStrong!Passw0rd;TrustServerCertificate=true;";
            var factory = new DbConnectionFactory(connectionString);
            var repo = new TaskRepository(factory);

            while (true)
            {
                Console.WriteLine("Управление задачами");
                Console.WriteLine("1. Добавить задачу");
                Console.WriteLine("2. Показать все задачи");
                Console.WriteLine("3. Обновить статус задачи");
                Console.WriteLine("4. Удалить задачу");
                Console.WriteLine("0. Выход");
                Console.Write("Выберите действие: ");
                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        await AddTask(repo);
                        break;
                    case "2":
                        await ShowTasks(repo);
                        break;
                    case "3":
                        await UpdateTaskStatus(repo);
                        break;
                    case "4":
                        await DeleteTask(repo);
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Неверный выбор.");
                        break;
                }
            }
        }

        static async Task AddTask(TaskRepository repo)
        {
            Console.Write("Введите заголовок: ");
            var title = Console.ReadLine();
            Console.Write("Введите описание: ");
            var description = Console.ReadLine();

            var task = new TaskModel
            {
                Title = title ?? "",
                Description = description ?? "",
                IsCompleted = false,
                CreatedAt = DateTime.Now
            };

            var id = await repo.AddAsync(task);
            Console.WriteLine($"Задача добавлена с ID: {id}");
        }

        static async Task ShowTasks(TaskRepository repo)
        {
            var tasks = await repo.GetAllAsync();
            Console.WriteLine("\nСписок задач:");
            foreach (var t in tasks)
            {
                Console.WriteLine($"[{t.Id}] {t.Title} — {(t.IsCompleted ? "Выполнена" : "Не выполнена")} — {t.CreatedAt:g}");
            }
        }

        static async Task UpdateTaskStatus(TaskRepository repo)
        {
            Console.Write("Введите ID задачи: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                Console.Write("Отметить как выполненную? (y/n): ");
                var input = Console.ReadLine();
                var isCompleted = input?.ToLower() == "y";

                var success = await repo.UpdateStatusAsync(id, isCompleted);
                Console.WriteLine(success ? "Статус обновлён." : "Задача не найдена.");
            }
            else
            {
                Console.WriteLine("Неверный ID.");
            }
        }

        static async Task DeleteTask(TaskRepository repo)
        {
            Console.Write("Введите ID задачи для удаления: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var success = await repo.DeleteAsync(id);
                Console.WriteLine(success ? "Задача удалена." : "Задача не найдена.");
            }
            else
            {
                Console.WriteLine("Неверный ID.");
            }
        }
    }
}