using System;

namespace DoorKicker.Entities
{
    public class Event: Entity
    {
        public DateTime Created { get; set; }
        public int DoorId { get; set; }

        public Door Door { get; set; }

        public string Message { get; set; }
    }
}
