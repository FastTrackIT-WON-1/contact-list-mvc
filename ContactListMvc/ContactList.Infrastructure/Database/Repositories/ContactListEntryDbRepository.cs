using ContactList.Abstractions;
using ContactList.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactList.Infrastructure.Database.Repositories
{
    public class ContactListEntryDbRepository : IContactListEntryRepository
    {
        private readonly DatabaseContext database;

        public ContactListEntryDbRepository(DatabaseContext database)
        {
            this.database = database ?? throw new ArgumentNullException(nameof(database));
        }

        public async Task<bool> CreateAsync(ContactListEntry entry)
        {
            database.ContactListEntry.Add(new Entities.ContactListEntryEntity
            {
                Type = entry.Type,
                Name = entry.Name,
                DateOfBirth = entry.DateOfBirth,
                Address = entry.Address,
                PhoneNumber = entry.PhoneNumber,
                Email = entry.Email
            });

            int rowsInserted = await database.SaveChangesAsync();

            return rowsInserted > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            Entities.ContactListEntryEntity entityToDelete = await database.ContactListEntry
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync();

            if (entityToDelete is null)
            {
                return false;
            }

            database.ContactListEntry.Remove(entityToDelete);
            int rowsDeleted = await database.SaveChangesAsync();

            return rowsDeleted > 0;
        }

        public async Task<List<ContactListEntry>> GetAllAsync()
        {
            List<Entities.ContactListEntryEntity> entities = await database.ContactListEntry.ToListAsync();
            return entities.Select(e => new ContactListEntry
            {
                Id = e.Id,
                Type = e.Type,
                Name = e.Name,
                DateOfBirth = e.DateOfBirth,
                Address = e.Address,
                PhoneNumber = e.PhoneNumber,
                Email = e.Email
            }).ToList();
        }

        public async Task<ContactListEntry> GetByIdAsync(int id)
        {
            Entities.ContactListEntryEntity entity = await database.ContactListEntry
               .Where(c => c.Id == id)
               .FirstOrDefaultAsync();

            if (entity is null)
            {
                return null;
            }

            return new ContactListEntry
            {
                Id = entity.Id,
                Type = entity.Type,
                Name = entity.Name,
                DateOfBirth = entity.DateOfBirth,
                Address = entity.Address,
                PhoneNumber = entity.PhoneNumber,
                Email = entity.Email
            };
        }

        public async Task<bool> UpdateAsync(int id, ContactListEntry updatedEntry)
        {
            Entities.ContactListEntryEntity entityToUpdate = await database.ContactListEntry
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync();

            if (entityToUpdate is null)
            {
                return false;
            }

            entityToUpdate.Type = updatedEntry.Type;
            entityToUpdate.Name = updatedEntry.Name;
            entityToUpdate.DateOfBirth = updatedEntry.DateOfBirth;
            entityToUpdate.Address = updatedEntry.Address;
            entityToUpdate.PhoneNumber = updatedEntry.PhoneNumber;
            entityToUpdate.Email = updatedEntry.Email;

            int rowsUpdated = await database.SaveChangesAsync();

            return rowsUpdated > 0;
        }
    }
}
