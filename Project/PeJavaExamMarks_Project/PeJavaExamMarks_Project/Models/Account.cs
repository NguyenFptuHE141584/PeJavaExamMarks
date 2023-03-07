using System;
using System.Collections.Generic;

#nullable disable

namespace PeJavaExamMarksProject.Models
{
    public partial class Account
    {
        public Account()
        {
            ClassAccounts = new HashSet<ClassAccount>();
        }

        public int AccountId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public virtual ICollection<ClassAccount> ClassAccounts { get; set; }
    }
}
