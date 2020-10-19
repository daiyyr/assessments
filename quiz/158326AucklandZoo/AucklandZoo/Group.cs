using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AucklandZoo
{
    public enum Price
    {
        School = 5,
        Tour = 10
    }

    public interface IGroup
    {
        decimal getTotalPrice(decimal size);
        decimal getTotalDiscount(decimal size);
    }

    public class School : IGroup
    {
        public decimal getTotalPrice(decimal size)
        {
            decimal price = Convert.ToDecimal(Price.School);
            if (size == 0)
                return 0;
            if (size < 20)
                return price * size;
            else
                return price * size * 0.8m;
        }
        public decimal getTotalDiscount(decimal size)
        {
            decimal price = Convert.ToDecimal(Price.School);
            if (size < 20)
                return 0;
            else
                return price * size * 0.2m;
        }
    }

    public class Tour : IGroup
    {
        public decimal getTotalPrice(decimal size)
        {
            decimal price = Convert.ToDecimal(Price.Tour);
            if (size == 0)
                return 0;
            if (size < 5)
                return price * size;
            else
                return price * size * 0.9m;
        }
        public decimal getTotalDiscount(decimal size)
        {
            decimal price = Convert.ToDecimal(Price.Tour);
            if (size < 5)
                return 0;
            else
                return price * size * 0.1m;
        }
    }

}
