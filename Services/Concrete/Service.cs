using DataAccess;
using DataAccess.Concrete;
using Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Concrete
{
    public class Service<T> : Repository<T>, IService<T> where T : class
    {
        public Service(Context context) : base(context) { }

    }
}
