using Contacts.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contacts.Data
{
    public interface IContactRepository
    {
        Task<IEnumerable<Contact>> GetAll();
        Task<Contact> Get(Guid id);
        Task<bool> Exists(Guid id);
        Task Add(Contact contact);
        Task Update(Contact contact);
        Task Remove(Guid id);
    }
}