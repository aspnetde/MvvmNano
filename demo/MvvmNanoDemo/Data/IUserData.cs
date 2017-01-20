using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmNanoDemo.Model;

namespace MvvmNanoDemo.Data
{
    interface IUserData
    {
        User User { get; set; }
    }
}
