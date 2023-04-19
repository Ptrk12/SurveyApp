using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IIdentity<K> where K :IComparable<K>
    {
        public K Id
        {
            get;
            set;
        }
    }
}
