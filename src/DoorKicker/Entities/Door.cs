using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DoorKicker.Entities
{
    public class Door: Entity
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Token { get; set; }

        public bool IsOpen { get; set; }

        public int PropertyId { get; set; }

        public Property Property { get; set; }

        public ICollection<Event> Events { get; set; }
    }
}
