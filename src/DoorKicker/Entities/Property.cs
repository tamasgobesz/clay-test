using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DoorKicker.Entities
{
    public class Property: Entity
    {
        [Required]
        public string Name { get; set; }

        public ICollection<Door> Doors { get; set; }

        public ICollection<PropertyUser> PropertyUsers { get; set; }
    }
}
