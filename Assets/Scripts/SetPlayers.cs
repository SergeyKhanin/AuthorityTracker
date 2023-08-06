public static class SetPlayers
{
    public static string GetPlayerFromList(PlayersRoster.PlayersList playersList)
    {
        string playerName;

        switch (playersList)
        {
            case PlayersRoster.PlayersList.Player1:
                playerName = "player-1";
                break;
            case PlayersRoster.PlayersList.Player2:
                playerName = "player-2";
                break;
            default:
                playerName = "player-1";
                break;
        }

        return playerName;
    }
}