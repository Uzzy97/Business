using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Data
{
    public class MemoryEntryStorage : INoteEntryStorage
    {
        private readonly Dictionary<string, NoteTake> entries = new Dictionary<string, NoteTake>();

        public Task<IEnumerable<NoteTake>> GetAllAsync()
        {
            IEnumerable<NoteTake> result = entries.Values.ToList();
            return Task.FromResult(result);
        }

        public Task AddAsync (NoteTake entry)
        {
            entries.Add(entry.ID, entry);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(NoteTake entry)
        {
            return Task.CompletedTask;
        }

        public Task DeleteAsync(NoteTake entry)
        {
            entries.Remove(entry.ID);
            return Task.CompletedTask;
        }

        public Task<NoteTake> GetByIDAsync(string id)
        {
            NoteTake entry = null;
            entries.TryGetValue(id, out entry);
            return Task.FromResult<NoteTake>(entry);
        }
    }
}
