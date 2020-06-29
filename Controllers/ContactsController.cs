using AutoMapper;
using Contacts.Data;
using Contacts.Dtos;
using Contacts.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contacts.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IContactRepository _repository;
        private readonly IMapper _mapper;

        public ContactsController(IContactRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // Get api/contacts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactDto>>> GetAll()
        {
            var contacts = await _repository.GetAll();

            return Ok(_mapper.Map<IEnumerable<ContactDto>>(contacts));
        }

        // Get api/contacts/{guid}
        [HttpGet("{id}", Name = nameof(GetById))]
        public async Task<ActionResult<ContactDto>> GetById(Guid id)
        {

            if (await _repository.Exists(id) == false)
            {
                return NotFound();
            }

            var contact = await _repository.Get(id);

            return Ok(_mapper.Map<ContactDto>(contact));
        }

        // Post api/contacts
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] ContactDto contactDto)
        {
            var contact = _mapper.Map<Contact>(contactDto);

            await _repository.Add(contact);

            // reload for id
            contactDto = _mapper.Map<ContactDto>(contact);

            return CreatedAtRoute(nameof(GetById), new { Controller = "Contacts", id = contactDto.Id }, contactDto);
        }

        // PUT api/contacts/{guid}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, ContactDto contactDto)
        {
            if (id != contactDto.Id)
            {
                return BadRequest(new { message = "id's in uri and object are different" });
            }

            if (await _repository.Exists(id) == false)
            {
                return NotFound();
            }

            var contact = _mapper.Map<Contact>(contactDto);
            await _repository.Update(contact);
            return NoContent();
        }

        // DELETE api/contacts/{guid}
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            if (await _repository.Exists(id) == false)
            {
                return NotFound();
            }

            await _repository.Remove(id);
            return NoContent();
        }
    }
}
