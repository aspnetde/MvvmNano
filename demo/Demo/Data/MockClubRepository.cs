using System.Collections.Generic;
using Demo.Model;

namespace Demo.Data
{
    public class MockClubRepository : IClubRepository
    {
        public List<Club> All()
        {
            return new List<Club>
            {
                new Club("FC Bayern München", "Germany"),
                new Club("Borussia Dortmund", "Germany"),
                new Club("Real Madrid", "Spain"),
                new Club("FC Barcelona", "Spain"),
                new Club("Manchester United", "England")
            };
        }
    }
}