namespace WebApiMy.Models
{
    public class AccountGetToken
    {
        public string Login { get; set; } = null!;
        public string? Password { get; set; }
    }
}
