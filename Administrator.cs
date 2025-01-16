using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_Admin_System
{
    public class Administrator : Employee
    {
        public Administrator(string name, string surname, string pesel, string username, string password, Role role)
            : base(name, surname, pesel, username, password, role)
        {
        }
    }
}
