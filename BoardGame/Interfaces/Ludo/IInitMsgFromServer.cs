namespace BoardGame.Interfaces.Ludo
{
    interface IInitMsgFromServer
    {
        IPlayer WPFPlayer { get; }
        IPlayer[] otherWPFPlayers { get; }
        IMsgFromServer MsgFromServer { get; }

    }
}