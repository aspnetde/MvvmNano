using System.Collections.Generic;
using Demo.Model;

namespace Demo.Data
{
    public interface IClubRepository
    {
        List<Club> All();
    }
}