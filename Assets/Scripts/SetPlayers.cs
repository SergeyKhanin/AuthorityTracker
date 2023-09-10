public static class SetPlayers
{
    public static string GetPlayerFromList(PlayersRoster.PlayersList playersList)
    {
        string playerName;

        switch (playersList)
        {
            case PlayersRoster.PlayersList.Player1:
                playerName = CommonSaveParameters.Player1;
                break;
            case PlayersRoster.PlayersList.Player2:
                playerName = CommonSaveParameters.Player2;
                break;
            default:
                playerName = CommonSaveParameters.Player1;
                break;
        }

        return playerName;
    }
}