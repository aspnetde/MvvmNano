using Demo.Model;

namespace Demo.Data
{
    class UserData : IUserData
    {
        public User User { get; set; } = new User("Default");
    }
}