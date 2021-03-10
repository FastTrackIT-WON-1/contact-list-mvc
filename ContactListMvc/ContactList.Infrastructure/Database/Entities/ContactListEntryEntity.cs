using ContactList.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace ContactList.Infrastructure.Database.Entities
{
    public class ContactListEntryEntity
    {
        [Key]
        public int Id { get; set; }

        public ContactType Type { get; set; }

        public string Name { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }
    }
}
