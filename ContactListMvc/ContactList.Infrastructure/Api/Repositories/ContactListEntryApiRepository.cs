using ContactList.Abstractions;
using ContactList.Configuration;
using ContactList.Infrastructure.Api.Entities;
using ContactList.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ContactList.Infrastructure.Api.Repositories
{
    public class ContactListEntryApiRepository : IContactListEntryRepository
    {
        private readonly ContactListApiOptions options;

        public ContactListEntryApiRepository(ContactListApiOptions options)
        {
            this.options = options;
        }

        public Task<bool> CreateAsync(ContactListEntry entry)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ContactListEntry>> GetAllAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.options.Url);
                HttpResponseMessage response = await client.GetAsync("/users");

                if (response.IsSuccessStatusCode)
                {
                    List<UserEntity> users = await response.Content.ReadAsAsync<List<UserEntity>>();
                    return users.Select(u => new ContactListEntry
                    {
                        Id = u.Id,
                        Name = u.Name,
                        Email = u.Email,
                        PhoneNumber = u.Phone
                    }).ToList();
                }
            }

            return new List<ContactListEntry>();
        }

        public Task<ContactListEntry> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(int id, ContactListEntry updatedEntry)
        {
            throw new NotImplementedException();
        }
    }
}
