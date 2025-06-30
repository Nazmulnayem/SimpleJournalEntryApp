using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SimpleJournalApp.Application.Interface;
using SimpleJournalApp.Domain.Entities;
using SimpleJournalApp.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleJournalApp.Infrastructure.Service
{
    public class JournalEntryService : IJournalEntryService
    {
        private readonly AppDbContext _context;

        public JournalEntryService(AppDbContext context) => _context = context;

        public async Task<List<JournalEntry>> GetAllAsync()
        {
            try
            {

                return await _context.JournalEntry.FromSqlRaw("EXEC sp_GetAllJournalEntries").ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<JournalEntry> GetByIdAsync(int id)
        {
            try
            {
                var result = await _context.JournalEntry
                    .FromSqlRaw("EXEC sp_GetJournalEntryById @p0", id)
                    .ToListAsync();

                return result.FirstOrDefault() ?? new JournalEntry();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching journal entry by ID: {ex.Message}", ex);
            }
        }

        public async Task CreateAsync(List<JournalEntry> entries)
        {


            try
            {
                // List<JournalEntry> vars = new List<JournalEntry>();

                foreach (var entry in entries)
                {
                    await _context.Database.ExecuteSqlRawAsync(
                        "EXEC sp_InsertJournalEntry @p0, @p1, @p2, @p3",
                        entry.EntryDate,
                        entry.Description,
                        entry.DebitAmount,
                        entry.CreditAmount
                    );
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error inserting journal entries: " + ex.Message);
            }
        }

        public async Task UpdateAsync(JournalEntry entry)
        {
            try
            {
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC sp_UpdateJournalEntry @p0, @p1, @p2, @p3,@p4",
                    entry.Id,
                    entry.EntryDate,
                    entry.Description,
                    entry.DebitAmount,
                    entry.CreditAmount
                );
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error Updating journal entries: " + ex.Message);
            }
        }

        public async Task DeleteAsync(int id)
        {
            var entry = await _context.JournalEntry.FindAsync(id);
            if (entry != null)
            {
                _context.JournalEntry.Remove(entry);
                await _context.SaveChangesAsync();
            }
        }

        public Task CreateAsync(JournalEntry entry)
        {
            throw new NotImplementedException();
        }
    }

}
