namespace EHS_Benjamin_Pasic.Models
{
    public class UserSavedNews
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public AppUser User { get; set; }

        public int NewsItemId { get; set; }
        public NewsItem NewsItem { get; set; }
    }

}
