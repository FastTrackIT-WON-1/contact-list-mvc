using ContactList.Abstractions;
using ContactList.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactList.Services
{
    public class ContactListEntryService : IContactListEntryService
    {
        private readonly IContactListEntryRepository repository;

        public ContactListEntryService(IContactListEntryRepository repository)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public Task<bool> CreateAsync(ContactListEntry entry)
        {
            if (entry is null)
            {
                throw new ArgumentNullException(nameof(entry));
            }

            return this.repository.CreateAsync(entry);
        }

        public Task<bool> DeleteAsync(int id)
        {
            return this.repository.DeleteAsync(id);
        }

        public async Task<bool> EntryExistsAsync(int id)
        {
            ContactListEntry entry = await GetByIdAsync(id);

            bool exists = !(entry is null);

            return exists;
        }

        public Task<List<ContactListEntry>> GetAllAsync()
        {
            return this.repository.GetAllAsync();
        }

        public Task<ContactListEntry> GetByIdAsync(int id)
        {
            return this.repository.GetByIdAsync(id);
        }

        public Task<bool> UpdateAsync(int id, ContactListEntry updatedEntry)
        {
            if (updatedEntry is null)
            {
                throw new ArgumentNullException(nameof(updatedEntry));
            }

            return this.repository.UpdateAsync(id, updatedEntry);
        }
    }
}
