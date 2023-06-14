namespace Turn
{
    public class TurnSystems : Feature
    {
        public TurnSystems(Contexts contexts)
        {
            Add(new InitializeTurnControllerSystem(contexts.level, contexts.game));
            Add(new InitializeFirstEndTurnConditionsSystem(contexts.game));
            Add(new FirstPlayerTurnInitializeSystem(contexts.game));
            Add(new EndPlayerTurnSystem(contexts.game));
            Add(new ReplacePlayerTurnAmountSystem(contexts.game));
            Add(new TriggerCheckingEndTurnAfterPokeballOpenSystem(contexts.game));
            Add(new CheckEndTurnConditionSystem(contexts.game));
            Add(new DecreasePlayersTurnsAmountByOpenedPokeball(contexts.game));
            Add(new ResetPlayersTurnAmountAfterLevelEnd(contexts.game));
            Add(new InstantiateComplicators(contexts.game, contexts.level, contexts.complicators));
        }
    }
}