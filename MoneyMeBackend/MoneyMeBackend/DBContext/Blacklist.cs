using System;
using System.Collections.Generic;

namespace MoneyMeBackend.DBContext
{
    public partial class Blacklist
    {
        public int BlacklistId { get; set; }
        public string? Type { get; set; }
        public string? Domain { get; set; }
        public string? Mobile { get; set; }
    }
}
