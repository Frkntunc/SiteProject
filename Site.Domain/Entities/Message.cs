using Site.Domain.Entities.Commons;

namespace Site.Domain.Entities
{
    public class Message:EntityBase
    {
        public string Content { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public bool Read { get; set; }
    }
}
