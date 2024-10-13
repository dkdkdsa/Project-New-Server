using System;

namespace Project_New_Server
{
    public static class ShopContainer
    {

        private const string URL_FORMAT = "https://docs.google.com/spreadsheets/d/{0}/export?format=tsv&gid={1}";
        public static IReadOnlyDictionary<string, ulong> Container => _container;
        private static Dictionary<string, ulong> _container = new();

        public static async void SetUp()
        {

            var client = new HttpClient();
            var msg = await client.GetAsync(string.Format(URL_FORMAT, "1WGoF0bZKSI7BTcpdeDqrX1B7qRVbazdopzW-kaZCiP8", 0));
            var str = await msg.Content.ReadAsStringAsync();

            var lines = str.Split('\n');

            foreach ( var line in lines )
            {

                var s = line.Split('\t');
                _container.Add(s[0], ulong.Parse(s[1]));

            }

        }

    }
}
