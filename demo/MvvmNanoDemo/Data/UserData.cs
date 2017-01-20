using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmNanoDemo.Model;

namespace MvvmNanoDemo.Data
{
    class UserData : IUserData
    {
        public User User { get; set; }
    }
}
