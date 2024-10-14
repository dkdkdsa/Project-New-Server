using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedData
{
    [Serializable]
    public class ShopInfo
    {

        public IReadOnlyList<ShopData> Elements { get => elements; init => elements = value.ToList(); }
        public List<ShopData> elements;

    }
}
