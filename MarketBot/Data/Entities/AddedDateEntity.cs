namespace ProductManagerBot.Data.Entities
{
    public abstract class AddedDateEntity
    {
        public AddedDateEntity() => AddedDate = DateTime.UtcNow;
        public DateTime AddedDate { get; set; }
    }
}
