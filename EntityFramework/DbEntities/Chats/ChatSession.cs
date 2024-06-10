namespace EntityFramework.DbEntities.Chats
{
    public class ChatSession {
        
        public long Id { get; set; }
        public string FirstUserId { get; set; }
        public string SecondUserId { get; set; }
    }
}