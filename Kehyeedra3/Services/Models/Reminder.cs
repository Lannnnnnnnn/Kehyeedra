namespace Kehyeedra3.Services.Models
{
    public class Reminder
    {
        public ulong Id { get; set; }
        public ulong Created { get; set; } = 0;
        public ulong Send { get; set; } = 0;
        public ulong UserId { get; set; } = 0;
        public string Message { get; set; }
    }
}
