namespace WebApiMy.Models
{
    public class AccountDto
    {
        public string Name { get; set; } = null!;
        public string Login { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? RoleName { get; set; }
    }
}
