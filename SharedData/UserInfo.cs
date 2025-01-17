﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedData
{
    [Serializable]
    public class UserInfo
    {

        public ulong Coin { get => coin; init => coin = value; }
        public ulong Jam { get => jam; init => jam = value; }
        public IReadOnlyList<string> Decks { get => decks; init => decks = value.ToList(); }
        public IReadOnlyList<string> Towers { get => towers; init => towers = value.ToList(); }

        public ulong coin;
        public ulong jam;
        public List<string> decks;
        public List<string> towers;

    }
}
