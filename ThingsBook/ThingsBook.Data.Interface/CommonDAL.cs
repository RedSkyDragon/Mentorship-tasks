namespace ThingsBook.Data.Interface
{
    /// <summary>
    /// Class with DAL interfaces
    /// </summary>
    public class CommonDAL
    {
        /// <summary>
        /// Gets the users DAL interface.
        /// </summary>
        public IUsers Users { get; }

        /// <summary>
        /// Gets the friends DAL interface.
        /// </summary>
        public IFriends Friends { get; }

        /// <summary>
        /// Gets the things DAL interface.
        /// </summary>
        public IThings Things { get; }

        /// <summary>
        /// Gets the categories DAL interface.
        /// </summary>
        public ICategories Categories { get; }

        /// <summary>
        /// Gets the lends DAL interface.
        /// </summary>
        public ILends Lends { get; }

        /// <summary>
        /// Gets the history DAL interface.
        /// </summary>
        public IHistory History { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommonDAL"/> class.
        /// </summary>
        /// <param name="users">The users DAL interface.</param>
        /// <param name="friends">The friends DAL interface.</param>
        /// <param name="categories">The categories DAL interface.</param>
        /// <param name="things">The things DAL interface.</param>
        /// <param name="lends">The lends DAL interface.</param>
        /// <param name="history">The history DAL interface.</param>
        public CommonDAL(IUsers users, IFriends friends, ICategories categories, IThings things, ILends lends, IHistory history)
        {
            Users = users;
            Friends = friends;
            Categories = categories;
            Things = things;
            Lends = lends;
            History = history;
        }
    }
}
