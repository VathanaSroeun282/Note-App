namespace NotesAPI.Models
{
    public class Note
    {
        public int Id { get; set; }
        public string Title { set; get; }
        public string? Content { set; get; }
        public DateTime CreateDate { set; get; }
        public DateTime? UpdateDate { set; get; }
    }
}
