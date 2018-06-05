using System.ComponentModel.DataAnnotations.Schema;

namespace BlogModule.Entities
{
    public class Post
    {
        public int PostId { get; set; }

        public int BlogId { get; set; }

        public string Content { get; set; }

        public string Title { get; set; }

        [ForeignKey("BlogId")]
        public virtual Blog Blog { get; set; }
    }
}