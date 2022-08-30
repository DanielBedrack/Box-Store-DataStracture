using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxStore.DAL
{
    public interface IRepository<V>
    {
        void Add(double width, double height, int amount);

        IEnumerable SearchInDB(double width, double height, int amount);

        V GetItem(double width, double height);
    }
}
