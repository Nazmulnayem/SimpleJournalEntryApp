using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SimpleJournalApp.Application.Interface
{
    public interface IGenerateToken
    {


        string GenerateTokengen(string username, string role);
    }
}
