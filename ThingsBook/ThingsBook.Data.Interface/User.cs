namespace ThingsBook.Data.Interface
{
    /// <summary>
    /// User data model.
    /// </summary>
    /// <seealso cref="ThingsBook.Data.Interface.Entity" />
    public class User: Entity
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }
    }
}
