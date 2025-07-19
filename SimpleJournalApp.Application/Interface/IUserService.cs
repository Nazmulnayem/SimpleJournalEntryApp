using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SimpleJournalApp.Application.Interface
{
    public interface IUserService
    {


        string Authenticate(string username, string role);
    }
}
