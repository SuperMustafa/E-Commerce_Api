using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos.Sequrity
{
    public record UserLoginDto
    {
        [Required (ErrorMessage ="Email Is Required")]
        [EmailAddress]
        public string Email { get; init; }

        [Required(ErrorMessage = "Password Is Required")]
        public string Password { get; init; }
    }
}
