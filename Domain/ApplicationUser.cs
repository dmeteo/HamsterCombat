﻿using Microsoft.AspNetCore.Identity;

namespace CSharpClicker.Domain
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public long CurrentScore { get; set; }

        public long RecordScore { get; set; }

        public IEnumerable<UserBoost> UserBoosts { get; set; } = [];
    }
}
