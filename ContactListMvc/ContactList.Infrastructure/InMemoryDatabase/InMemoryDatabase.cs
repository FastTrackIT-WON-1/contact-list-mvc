using ContactList.Models;
using System;
using System.Collections.Generic;

namespace ContactList.Infrastructure.InMemoryDatabase
{
    public class InMemoryDatabase
    {
        public InMemoryDatabase(params ContactListEntry[] seedEntries)
        {
            ContactList = new List<ContactListEntry>(seedEntries ?? new ContactListEntry[0]);
        }

        public static int CurrentId { get; private set; }

        public List<ContactListEntry> ContactList { get; }

        public static void NextId()
        {
            CurrentId++;
        }

        public static ContactListEntry[] CreateSeedData()
        {
            List<ContactListEntry> seedData = new List<ContactListEntry>();

            NextId();
            seedData.Add(new ContactListEntry
            {
                Id = CurrentId,
                Type = ContactType.Person,
                Name = $"Test Person {CurrentId}",
                DateOfBirth = DateTime.Today,
                Email = $"email{CurrentId}@gmail.com",
                Address = $"Address line {CurrentId}",
                PhoneNumber = $"12334343{CurrentId}"
            });

            NextId();
            seedData.Add(new ContactListEntry
            {
                Id = CurrentId,
                Type = ContactType.Company,
                Name = $"Test Company {CurrentId}",
                DateOfBirth = DateTime.Today,
                Email = $"email{CurrentId}@gmail.com",
                Address = $"Address line {CurrentId}",
                PhoneNumber = $"12334343{CurrentId}"
            });

            NextId();
            seedData.Add(new ContactListEntry
            {
                Id = CurrentId,
                Type = ContactType.Person,
                Name = $"Test Person {CurrentId}",
                DateOfBirth = DateTime.Today,
                Email = $"email{CurrentId}@gmail.com",
                Address = $"Address line {CurrentId}",
                PhoneNumber = $"12334343{CurrentId}"
            });

            return seedData.ToArray();
        }
    }
}
