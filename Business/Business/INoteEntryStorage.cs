using Business.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Data
{
    public interface INoteEntryStorage
    {
        Task<NoteTake> GetByIDAsync(string id);
        Task<IEnumerable<NoteTake>> GetAllAsync();
        Task AddAsync(NoteTake entry);
        Task UpdateAsync(NoteTake entry);
        Task DeleteAsync(NoteTake entry);

    }
}
