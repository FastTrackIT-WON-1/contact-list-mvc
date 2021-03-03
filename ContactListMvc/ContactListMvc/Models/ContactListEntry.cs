using System;
using System.ComponentModel.DataAnnotations;

namespace ContactListMvc.Models
{
    public class ContactListEntry
    {
        [Key]
        public int Id { get; set; }

        public ContactType Type { get; set; }

        public string Name { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        public string Address { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
