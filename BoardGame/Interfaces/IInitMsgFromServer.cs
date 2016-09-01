namespace BoardGame.Interfaces
{
    interface IInitMsgFromServer
    {
        IPlayer WPFPlayer { get; }
        IPlayer[] otherWPFPlayers { get; }
        IMsgFromServer MsgFromServer { get; }

    }
}