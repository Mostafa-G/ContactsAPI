using Contacts.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Contacts.Models
{
    public class Contact : IValidatableObject
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Avatar { get; set; }
        public string NickName { get; set; }
        public string Company { get; set; }
        public string JobTitle { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime Birthday { get; set; }
        public string Notes { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var _context = validationContext.GetService(typeof(DataContext)) as DataContext;

            List<ValidationResult> validationResult = new List<ValidationResult>();

            // unique Phone
            if (Phone != null && _context.Contacts.Any(x => x.Phone == Phone) == true)
            {
                validationResult.Add(new ValidationResult
                    ("The contact Phone already exists.", new[] { "Phone" }));
            }

            // unique Email
            if (Email != null && _context.Contacts.Any(x => x.Email == Email) == true)
            {
                validationResult.Add(new ValidationResult
                    ("The contact Email already exists.", new[] { "Email" }));
            }

            return validationResult;
        }
    }
}