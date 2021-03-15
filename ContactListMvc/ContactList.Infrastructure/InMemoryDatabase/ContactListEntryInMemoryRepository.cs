using ContactList.Abstractions;
using ContactList.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactList.Infrastructure.InMemoryDatabase
{
    public class ContactListEntryInMemoryRepository : IContactListEntryRepository
    {
        private readonly InMemoryDatabase _database;

        public ContactListEntryInMemoryRepository(InMemoryDatabase database)
        {
            _database = database;
        }

        public Task<bool> CreateAsync(ContactListEntry entry)
        {
            InMemoryDatabase.NextId();
            entry.Id = InMemoryDatabase.CurrentId;
            _database.ContactList.Add(entry);

            return Task.FromResult(true);
        }

        public Task<bool> DeleteAsync(int id)
        {
            ContactListEntry entry = _database.ContactList.FirstOrDefault(c => c.Id == id);
            if (entry != null)
            {
                _database.ContactList.Remove(entry);
                return Task.FromResult(true);
            }
            else
            {
                return Task.FromResult(false);
            }
        }

        public Task<List<ContactListEntry>> GetAllAsync()
        {
            return Task.FromResult(_database.ContactList);
        }

        public Task<ContactListEntry> GetByIdAsync(int id)
        {
            return Task.FromResult(_database.ContactList.FirstOrDefault(c => c.Id == id));
        }

        public Task<bool> UpdateAsync(int id, ContactListEntry updatedEntry)
        {
            ContactListEntry entry = _database.ContactList.FirstOrDefault(c => c.Id == id);
            if (entry != null)
            {
                entry.Type = updatedEntry.Type;
                entry.Name = updatedEntry.Name;
                entry.DateOfBirth = updatedEntry.DateOfBirth;
                entry.Email = updatedEntry.Email;
                entry.Address = updatedEntry.Address;
                entry.PhoneNumber = updatedEntry.PhoneNumber;

                return Task.FromResult(true);
            }
            else
            {
                return Task.FromResult(false);
            }
        }
    }
}
