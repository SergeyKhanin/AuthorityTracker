using Common;

namespace Core
{
    public static class SetPlayers
    {
        public static string GetPlayerFromList(PlayersRoster playersRoster)
        {
            string playerName;

            switch (playersRoster)
            {
                case PlayersRoster.Player1:
                    playerName = CommonSaveParameters.Player1;
                    break;
                case PlayersRoster.Player2:
                    playerName = CommonSaveParameters.Player2;
                    break;
                default:
                    playerName = CommonSaveParameters.Player1;
                    break;
            }

            return playerName;
        }
    }
}
