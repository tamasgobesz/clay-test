using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoorKicker.Entities
{
    public class Event: Entity
    {
        public int DoorId { get; set; }

        public Door Door { get; set; }

        public string Message { get; set; }
    }
}
