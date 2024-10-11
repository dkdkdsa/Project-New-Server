namespace Project_New_Server.Model
{
    public class UserData
    {

        public string Id { get; set; }
        public ulong Coin { get; set; }
        public ulong Jam { get; set; }
        public List<string> Decks { get; set; }
        public List<string> Towers { get; set; }

        public UserData(string id)
        {
            Id = id;
            Coin = 0;
            Jam = 0;
            Decks = new List<string>();
            Towers = new List<string>();
        }

    }
}
