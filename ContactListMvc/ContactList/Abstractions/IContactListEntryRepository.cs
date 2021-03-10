using ContactList.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactList.Abstractions
{
    public interface IContactListEntryRepository
    {
        /// <summary>
        /// Returns the whole collection of contact lists from the database.
        /// </summary>
        /// <returns>The whole collection of contact lists from the database.</returns>
        Task<List<ContactListEntry>> GetAllAsync();

        /// <summary>
        /// Gets a single contact list entry for the specified identifier.
        /// </summary>
        /// <param name="id">The contact list identifier.</param>
        /// <returns>The contact list entry for the specified identifier.</returns>
        Task<ContactListEntry> GetByIdAsync(int id);

        /// <summary>
        /// Creates a new contact list entry.
        /// </summary>
        /// <param name="entry">The contact list entry.</param>
        /// <returns>True if creation succeeds, false otherwise.</returns>
        Task<bool> CreateAsync(ContactListEntry entry);

        /// <summary>
        /// Updates a contact list entry.
        /// </summary>
        /// <param name="id">The contact list identifier.</param>
        /// <param name="updatedEntry">The updated contact list entry.</param>
        /// <returns>True if update succeeds, false otherwise.</returns>
        Task<bool> UpdateAsync(int id, ContactListEntry updatedEntry);

        /// <summary>
        /// Deletes a contact list entry.
        /// </summary>
        /// <param name="id">The contact list identifier.</param>
        /// <returns>True if delete succeeds, false otherwise.</returns>
        Task<bool> DeleteAsync(int id);
    }
}
