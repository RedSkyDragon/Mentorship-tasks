using ThingsBook.Data.Interface;

namespace ThingsBook.BusinessLogic
{
    public class BaseBL
    {
        protected CommonDAL Data { get; }

        public BaseBL(CommonDAL data)
        {
            Data = data;
        }
    }
}
