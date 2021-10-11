namespace GintareTestSuite
{
    public class PostsResponse
    {
        public User[] dataPost { get; set; }
    }
    public class PostResponse
    {
        public User dataPost { get; set; }
    }
    public class Posts
    {
        public int id { get; set; }
        public int user_id { get; set; }
        public string title { get; set; }
        public string body { get; set; }
     
    }
}