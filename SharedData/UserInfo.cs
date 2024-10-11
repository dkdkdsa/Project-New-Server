using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedData
{
    public class UserInfo
    {

        public ulong Coin { get; init; }
        public ulong Jam { get; init; }
        public IReadOnlyList<string> Decks { get; init; }
        public IReadOnlyList<string> Towers { get; init; }

    }
}
