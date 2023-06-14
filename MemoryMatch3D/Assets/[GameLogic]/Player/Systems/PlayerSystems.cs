namespace Player
{
    public class PlayerSystems : Feature
    {
        public PlayerSystems(Contexts contexts)
        {
            Add(new InitializePlayersSystem(contexts.game));//temporary 
            
        }
    }
}