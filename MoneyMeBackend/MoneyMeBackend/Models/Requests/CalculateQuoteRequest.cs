namespace MoneyMeBackend.Models.Requests
{
    public class CalculateQuoteRequest
    {
        public string AmountRequired { get; set; }
        public string Term { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DateOfBirth { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
    }
}
