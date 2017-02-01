
namespace DAL.Entities
{
    public class Item
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual bool IsCompleted { get; set; }
        public virtual int UserId { get; set; }

    }
}
