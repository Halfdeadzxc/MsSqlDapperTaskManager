using DAL.Models;
namespace DAL.Interfaces
{
    public interface ITaskRepository
    {
        Task<int> AddAsync(TaskModel task);
        Task<IEnumerable<TaskModel>> GetAllAsync();
        Task<bool> UpdateStatusAsync(int id, bool isCompleted);
        Task<bool> DeleteAsync(int id);
    }
}
