namespace Site.Domain.Dtos
{
    public class MessageDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public bool Read { get; set; }
    }
}
