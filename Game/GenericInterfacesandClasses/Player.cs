namespace Game.GenericInterfacesandClasses
{
    abstract public class Player
    {
        public Player(string newname)
        {
            Name = newname;
        }
        public string Name { get; set; }
    }
}