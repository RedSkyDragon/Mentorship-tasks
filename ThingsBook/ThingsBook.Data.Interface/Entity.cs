using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThingsBook.Data.Interface
{
    public class Entity
    {
        public Guid Id { get; set; } = SequentialGuidUtils.CreateGuid();
    }
}
