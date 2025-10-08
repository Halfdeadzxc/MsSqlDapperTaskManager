using Dapper;
using DAL.Models;
using DAL.Interfaces;
using DAL.Factory;


public class TaskRepository : ITaskRepository
{
    private readonly DbConnectionFactory _factory;

    public TaskRepository(DbConnectionFactory factory)
    {
        _factory = factory;
    }

    public async Task<int> AddAsync(TaskModel task)
    {
        using var connection = _factory.CreateConnection();
        var sql = @"INSERT INTO Tasks (Title, Description, IsCompleted, CreatedAt)
                    VALUES (@Title, @Description, @IsCompleted, @CreatedAt);
                    SELECT CAST(SCOPE_IDENTITY() as int);";

        return await connection.QuerySingleAsync<int>(sql, task);
    }

    public async Task<IEnumerable<TaskModel>> GetAllAsync()
    {
        using var connection = _factory.CreateConnection();
        var sql = "SELECT * FROM Tasks ORDER BY CreatedAt DESC";
        return await connection.QueryAsync<TaskModel>(sql);
    }

    public async Task<bool> UpdateStatusAsync(int id, bool isCompleted)
    {
        using var connection = _factory.CreateConnection();
        var sql = "UPDATE Tasks SET IsCompleted = @IsCompleted WHERE Id = @Id";
        var affected = await connection.ExecuteAsync(sql, new { Id = id, IsCompleted = isCompleted });
        return affected > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        using var connection = _factory.CreateConnection();
        var sql = "DELETE FROM Tasks WHERE Id = @Id";
        var affected = await connection.ExecuteAsync(sql, new { Id = id });
        return affected > 0;
    }
}
