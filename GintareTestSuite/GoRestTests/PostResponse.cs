namespace GintareTestSuite
{
    public class PostsResponse
    {
        // Masyvas = daug posts
        public Post[] data { get; set; }
    }

    public class PostResponse
    {
        // 1 post
        public Post data { get; set; }
    }

    public class Post
    {
        public int id { get; set; }
        public int user_id { get; set; }
        public string title { get; set; }
        public string body { get; set; }
        public int user { get; set; }
    }
}