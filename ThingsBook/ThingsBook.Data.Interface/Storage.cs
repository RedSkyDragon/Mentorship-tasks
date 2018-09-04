namespace ThingsBook.Data.Interface
{
    /// <summary>
    /// Class with DAL interfaces
    /// </summary>
    public class Storage
    {
        /// <summary>
        /// Gets the users DAL interface.
        /// </summary>
        public virtual IUsersDAL Users { get; }

        /// <summary>
        /// Gets the friends DAL interface.
        /// </summary>
        public virtual IFriendsDAL Friends { get; }

        /// <summary>
        /// Gets the things DAL interface.
        /// </summary>
        public virtual IThingsDAL Things { get; }

        /// <summary>
        /// Gets the categories DAL interface.
        /// </summary>
        public virtual ICategoriesDAL Categories { get; }

        /// <summary>
        /// Gets the lends DAL interface.
        /// </summary>
        public virtual ILendsDAL Lends { get; }

        /// <summary>
        /// Gets the history DAL interface.
        /// </summary>
        public virtual IHistoryDAL History { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Storage"/> class.
        /// </summary>
        /// <param name="users">The users DAL interface.</param>
        /// <param name="friends">The friends DAL interface.</param>
        /// <param name="categories">The categories DAL interface.</param>
        /// <param name="things">The things DAL interface.</param>
        /// <param name="lends">The lends DAL interface.</param>
        /// <param name="history">The history DAL interface.</param>
        public Storage(IUsersDAL users, IFriendsDAL friends, ICategoriesDAL categories, IThingsDAL things, ILendsDAL lends, IHistoryDAL history)
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
