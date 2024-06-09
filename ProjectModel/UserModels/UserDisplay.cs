namespace ProjectModel.AuthModel
{
    public class UserDisplay
    {
        public string UserId { get; set; }
        public string? Email { get; set; }
        public long ChatSessionId { get; set; }
        public string UserName { get; set; }
        public string? UserProfile { get; set; }
        public string? Role { get; set; }
    }
}
