using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedData
{
    [Serializable]
    public class ShopData
    {
        public string Name { get => name; init => name = value; }
        public ulong Price { get => price; init => price = value; }


        public string name;
        public ulong price;

    }
}
