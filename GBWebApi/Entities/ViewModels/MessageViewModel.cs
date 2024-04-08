namespace Entities.ViewModels
{
    public class MessageViewModel
    {
        public bool PerformedService { get; set; }
        public DateTime DateTimeReturn { get; set; }
        public string FailedMessage { get; set; }
        public string SuccessMessage { get; set; }
        public int id { get; set; }
    }
}
