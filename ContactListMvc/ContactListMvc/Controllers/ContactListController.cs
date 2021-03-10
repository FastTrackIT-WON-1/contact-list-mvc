using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ContactListMvc.Models;
using ContactList.Abstractions;

namespace ContactListMvc.Controllers
{
    public class ContactListController : Controller
    {
        private readonly IContactListEntryService service;

        public ContactListController(IContactListEntryService service)
        {
            this.service = service ?? throw new ArgumentNullException(nameof(service));
        }

        // GET: ContactList
        public async Task<IActionResult> Index()
        {
            List<ContactList.Models.ContactListEntry> contactList = await service.GetAllAsync();

            IEnumerable<ContactListEntry> contactListViewModels = contactList.Select(e => new ContactListEntry
            {
                Id = e.Id,
                Type = e.Type,
                Name = e.Name,
                DateOfBirth = e.DateOfBirth,
                Address = e.Address,
                PhoneNumber = e.PhoneNumber,
                Email = e.Email
            });

            return View(contactListViewModels);
        }

        // GET: ContactList/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ContactList.Models.ContactListEntry contactListEntry = await service.GetByIdAsync(id.Value);

            if (contactListEntry == null)
            {
                return NotFound();
            }

            var viewModel = new ContactListEntry
            {
                Id = contactListEntry.Id,
                Type = contactListEntry.Type,
                Name = contactListEntry.Name,
                DateOfBirth = contactListEntry.DateOfBirth,
                Address = contactListEntry.Address,
                PhoneNumber = contactListEntry.PhoneNumber,
                Email = contactListEntry.Email
            };

            return View(viewModel);
        }

        // GET: ContactList/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ContactList/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Type,Name,DateOfBirth,Address,PhoneNumber,Email")] ContactListEntry contactListEntry)
        {
            if (ModelState.IsValid)
            {
                bool createSuccess = await service.CreateAsync(new ContactList.Models.ContactListEntry
                {
                    Type = contactListEntry.Type,
                    Name = contactListEntry.Name,
                    DateOfBirth = contactListEntry.DateOfBirth,
                    Address = contactListEntry.Address,
                    PhoneNumber = contactListEntry.PhoneNumber,
                    Email = contactListEntry.Email
                });

                if (createSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(contactListEntry);
        }

        // GET: ContactList/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ContactList.Models.ContactListEntry contactListEntry = await service.GetByIdAsync(id.Value);

            if (contactListEntry == null)
            {
                return NotFound();
            }

            var viewModel = new ContactListEntry
            {
                Id = contactListEntry.Id,
                Type = contactListEntry.Type,
                Name = contactListEntry.Name,
                DateOfBirth = contactListEntry.DateOfBirth,
                Address = contactListEntry.Address,
                PhoneNumber = contactListEntry.PhoneNumber,
                Email = contactListEntry.Email
            };

            return View(viewModel);
        }

        // POST: ContactList/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Type,Name,DateOfBirth,Address,PhoneNumber,Email")] ContactListEntry contactListEntry)
        {
            if (id != contactListEntry.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    bool updateSuccess = await service.UpdateAsync(id, new ContactList.Models.ContactListEntry
                    {
                        Type = contactListEntry.Type,
                        Name = contactListEntry.Name,
                        DateOfBirth = contactListEntry.DateOfBirth,
                        Address = contactListEntry.Address,
                        PhoneNumber = contactListEntry.PhoneNumber,
                        Email = contactListEntry.Email
                    });

                    if (updateSuccess)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    bool exists = await service.EntryExistsAsync(contactListEntry.Id);

                    if (!exists)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            return View(contactListEntry);
        }

        // GET: ContactList/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ContactList.Models.ContactListEntry contactListEntry = await service.GetByIdAsync(id.Value);

            if (contactListEntry == null)
            {
                return NotFound();
            }

            var viewModel = new ContactListEntry
            {
                Id = contactListEntry.Id,
                Type = contactListEntry.Type,
                Name = contactListEntry.Name,
                DateOfBirth = contactListEntry.DateOfBirth,
                Address = contactListEntry.Address,
                PhoneNumber = contactListEntry.PhoneNumber,
                Email = contactListEntry.Email
            };

            return View(viewModel);
        }

        // POST: ContactList/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
