using ThingsBook.Data.Interface;

namespace ThingsBook.BusinessLogic
{
    /// <summary>
    /// Base class for business logic implementation.
    /// </summary>
    public class BaseBL
    {
        /// <summary>
        /// Gets the storage.
        /// </summary>
        protected Storage Storage { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseBL"/> class.
        /// </summary>
        /// <param name="storage">The storage.</param>
        public BaseBL(Storage storage)
        {
            Storage = storage;
        }
    }
}
