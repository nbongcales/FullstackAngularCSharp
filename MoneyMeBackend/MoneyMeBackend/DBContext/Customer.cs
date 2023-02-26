using System;
using System.Collections.Generic;

namespace MoneyMeBackend.DBContext
{
    public partial class Customer
    {
        public Customer()
        {
            Loans = new HashSet<Loan>();
            Quotes = new HashSet<Quote>();
        }

        public int CustomerId { get; set; }
        public string? Title { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Mobile { get; set; }
        public string? Email { get; set; }

        public virtual ICollection<Loan> Loans { get; set; }
        public virtual ICollection<Quote> Quotes { get; set; }
    }
}
