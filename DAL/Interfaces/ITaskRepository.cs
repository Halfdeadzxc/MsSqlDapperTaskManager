using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
