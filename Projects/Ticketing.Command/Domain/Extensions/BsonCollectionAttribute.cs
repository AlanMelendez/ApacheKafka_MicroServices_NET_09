namespace Ticketing.Command.Domain.Extensions
{

    [AttributeUsage(AttributeTargets.Class,Inherited = false)] // We use this attribute only for classes, we use "Inherited = false" to avoid that this attribute is inherited by derived classes
    public class BsonCollectionAttribute: Attribute
    {
        public string tableName {  get; set; }
        public BsonCollectionAttribute(string _tableName) { 
            tableName = _tableName ?? throw new ArgumentNullException(nameof(_tableName));
        }
    }
}
