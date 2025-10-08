Консольное приложение "Управление задачами"
📌 Функциональные требования
1. База данных:
   Создать таблицу `Tasks` со следующими полями:
 `Id` (int, PK, Identity)
 `Title` (nvarchar)
 `Description` (nvarchar)
 `IsCompleted` (bit)
 `CreatedAt` (datetime)
2. Функции в приложении:
    Добавление новой задачи
    Просмотр всех задач
    Обновление состояния задачи (`IsCompleted`)
    Удаление задачи по Id
3. Dapper:
Использовать Dapper для выполнения всех операций с базой данных.
Не использовать EF Core.


4. Design Patterns:
    Вынести работу с базой в отдельный слой с использованием паттерна Repository.
    (Опционально) Применить паттерн `Factory` для создания подключения к базе данных.

   Для запуска бд необходимо выполнить следующее:
   1. Запустить контейнер в терминале:
  cd MsSqlDapperTaskManager
  docker compose up -d
  2. Подключится к бд черз любой доступный обозреватель
  Параметры:
  Сервер: localhost
  Порт: 1433
  Логин: SA
  Пароль: YourStrong!Passw0rd
  3. Создать бд через запрос:
CREATE DATABASE TaskManager;
GO

USE TaskManager;
GO

CREATE TABLE Tasks (
    Id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    Title NVARCHAR(255),
    Description NVARCHAR(MAX),
    IsCompleted BIT,
    CreatedAt DATETIME DEFAULT GETDATE()
);
GO

