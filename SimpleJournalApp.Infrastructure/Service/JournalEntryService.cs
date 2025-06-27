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
                //var parameters = new[]
                //        {
                //            new SqlParameter("p_corpId", SqlDbType.BigInt) { Value = officeId ?? (object)DBNull.Value },
                //            new SqlParameter("p_branchId", SqlDbType.BigInt) { Value = branchId ?? (object)DBNull.Value },
                //            new SqlParameter("p_boothId", SqlDbType.Int) { Value = boothId ?? (object)DBNull.Value },
                //            new SqlParameter("p_assetId", SqlDbType.Int) { Value = assetId ?? (object)DBNull.Value },

                //        };
                //var data = await _connection.ScheduleDataDto.FromSqlRaw("SELECT * FROM get_schedule_data_set({0}, {1}, {2}, {3})", parameters).ToListAsync();

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
                var result = await _context.JournalEntry.FirstOrDefaultAsync(i => i.Id == id);
                return result ?? new JournalEntry();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task CreateAsync(List<JournalEntry> entries)
        {
            List<JournalEntry> vars = new List<JournalEntry>();

            foreach (var item in entries)
            {
                var result = new JournalEntry
                {
                    Id = item.Id,
                    DebitAmount = item.DebitAmount,
                    CreditAmount = item.CreditAmount,
                   
                };

                vars.Add(result);
            }

            try
            {
                await _context.JournalEntry.AddRangeAsync(vars);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error inserting journal entries: " + ex.Message);
            }
        }

        public async Task UpdateAsync(JournalEntry entry)
        {
            _context.JournalEntry.Update(entry);
            await _context.SaveChangesAsync();
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
