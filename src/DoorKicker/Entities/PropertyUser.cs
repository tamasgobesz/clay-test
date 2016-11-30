namespace DoorKicker.Entities
{
    /// <summary>
    /// This class is only needed beacuse EF Core does not support many to many relationships without an entity class
    /// </summary>
    public class PropertyUser: Entity
    {
        public int PropertyId { get; set; }

        public Property Property { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }
    }
}
