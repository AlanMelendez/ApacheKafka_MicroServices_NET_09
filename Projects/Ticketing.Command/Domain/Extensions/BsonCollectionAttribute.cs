namespace Ticketing.Command.Domain.Extensions
{

    [AttributeUsage(AttributeTargets.Class,Inherited = false)]
    public class BsonCollectionAttribute: Attribute
    {
        public string tableName {  get; set; }
        public BsonCollectionAttribute(string tableName) { 
            tableName = tableName ?? throw new ArgumentNullException(nameof(tableName));
        }
    }
}
