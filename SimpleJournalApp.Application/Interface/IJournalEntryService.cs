using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleJournalApp.Domain.Entities;

namespace SimpleJournalApp.Application.Interface
{
    public interface IJournalEntryService
    {
        Task<List<JournalEntry>> GetAllAsync();
        Task<JournalEntry> GetByIdAsync(int id);
        Task CreateAsync(List<JournalEntry> entries);
        Task UpdateAsync(JournalEntry entry);
        Task DeleteAsync(int id);

    }
}
