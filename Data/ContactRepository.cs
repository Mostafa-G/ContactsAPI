using Contacts.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contacts.Data
{
    public class ContactRepository : IContactRepository
    {
        private readonly DataContext _context;

        public ContactRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Contact>> GetAll()
        {
            return await _context.Contacts.ToListAsync();
        }

        public async Task<Contact> Get(Guid id)
        {
            return await _context.Contacts.SingleOrDefaultAsync(c => c.Id == id);
        }

        public async Task<bool> Exists(Guid id)
        {
            return (await _context.Contacts.AnyAsync(x => x.Id == id));
        }

        public async Task Add(Contact contact)
        {
            if (contact == null)
            {
                throw new ArgumentNullException(nameof(contact));
            }


            await _context.Contacts.AddAsync(contact);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Contact contact)
        {
            var contactToUpdate = await _context.Contacts.SingleOrDefaultAsync(c => c.Id == contact.Id);
            if (contactToUpdate != null)
            {
                contactToUpdate.Name = contact.Name;
                contactToUpdate.Phone = contact.Phone;
            }

            _context.Update(contactToUpdate);
            await _context.SaveChangesAsync();
        }

        public async Task Remove(Guid id)
        {
            var itemToRemove = await _context.Contacts.SingleOrDefaultAsync(c => c.Id == id);
            if (itemToRemove != null)
            {
                _context.Contacts.Remove(itemToRemove);
                await _context.SaveChangesAsync();
            }
        }
    }
}