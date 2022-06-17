using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBookADO
{
    public class Contact
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public Int64 PhoneNumber { get; set; }
        public Int64 Zip { get; set; }
        public string Email { get; set; }
        public string Type { get; set; }
    }
}
