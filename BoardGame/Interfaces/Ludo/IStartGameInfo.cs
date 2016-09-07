namespace BoardGame.Interfaces.Ludo
{
    interface IStartGameInfo
    {
        IPlayer WPFPlayer { get; }
        IPlayer[] otherWPFPlayers { get; }
        IGameInfo MsgFromServer { get; }

    }
}