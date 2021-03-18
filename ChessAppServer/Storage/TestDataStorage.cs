using System.Collections.Generic;

namespace ChessAppServer.Storage
{
    public class TestDataStorage : IChessDataStorage
    {

        private static TestDataStorage INSTANCE;
        public IDictionary<string, (string, string)> GlobalGames { get; private set; } = new Dictionary<string, (string, string)>();
        public IDictionary<string, (string, string, string)> PrivateGames { get; private set; } = new Dictionary<string, (string, string, string)>();

        public IDictionary<string, (string, string, bool)> Players { get; private set; } = new Dictionary<string, (string, string, bool)>();

        public static TestDataStorage GetStorage()
        {
            if (INSTANCE == null)
            {
                INSTANCE = new TestDataStorage();
            }

            return INSTANCE;
        }

        public bool IsGameRunning(string id)
        {
            bool found = false;

            if (GlobalGames.ContainsKey(id))
            {
                if (GlobalGames[id].Item1 == null || GlobalGames[id].Item2 == null)
                {
                    return false;
                }
                found = true;
            }

            if (!found || PrivateGames.ContainsKey(id))
            {
                if (PrivateGames[id].Item1 == null || PrivateGames[id].Item2 == null) return false;
            }

            return true;
        }

        public (string, string, string) FindGameByToken(string token)
        {
            foreach (var game in PrivateGames)
            {
                if (game.Value.Item1 == token)
                {
                    return (game.Key, game.Value.Item2, Players[game.Value.Item2].Item1);
                }
            }

            return (null, null, null);
        }

    }
}
