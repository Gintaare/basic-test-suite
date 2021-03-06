namespace GintareTestSuite
{
    public class UsersResponse
    {
        public User[] data { get; set; }
    }

    public class UserResponse
    {
        public User data { get; set; }
    }

    public class User
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string gender { get; set; }
        public string status { get; set; }
    }
}