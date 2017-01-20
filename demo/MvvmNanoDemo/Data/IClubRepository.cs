using System.Collections.Generic;
using MvvmNanoDemo.Model;

namespace MvvmNanoDemo.Data
{
    public interface IClubRepository
    {
        List<Club> All();
    }
}


