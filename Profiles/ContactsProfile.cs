using AutoMapper;
using Contacts.Dtos;
using Contacts.Models;

namespace Contacts.Profiles
{
    public class ContactsProfile : Profile
    {
        public ContactsProfile()
        {
            CreateMap<Contact, ContactDto>();
            CreateMap<ContactDto, Contact>();
        }
    }
}