using System;
using System.ComponentModel.DataAnnotations;


namespace Wtw.Policies.Domain.Models
{
    public class Policy : Entity
    {
        [Required]
        public Guid PolicyHolder_UUID { get; set; }

        public PolicyHolder PolicyHolder { get; set; }
    }
}
