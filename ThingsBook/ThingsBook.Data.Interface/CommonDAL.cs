namespace ThingsBook.Data.Interface
{
    public class CommonDAL
    {
        public IUsers Users { get; }

        public IFriends Friends { get; }

        public IThings Things { get; }

        public ICategories Categories { get; }

        public ILends Lends { get; }

        public IHistory History { get; }

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
