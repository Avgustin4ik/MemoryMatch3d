namespace Complicators
{
    public class ComplicatorsSystems : Feature
    {
        public ComplicatorsSystems(Contexts contexts)
        {
            Add(new DestroyComplicatorSystem(contexts.game, contexts.complicators));
            Add(new CalculateRandomTargetForComplicatorSystem(contexts.game, contexts.complicators));
            Add(new EndLevelComplicatorsCleanupSystem(contexts.game, contexts.complicators));
            Add(new ShiftColumnSystem(contexts.game, contexts.complicators));
            Add(new ShiftRowSystem(contexts.game, contexts.complicators));
            Add(new SwitchPokeballsSystem(contexts.game, contexts.complicators));
            Add(new ImplementComplicatorsByConcreteTurnSystem(contexts.game, contexts.complicators));
            Add(new CheckComplicatorsAfterSuccess(contexts.game, contexts.complicators));
            Add(new CheckComplicatorsAfterFail(contexts.game, contexts.complicators));
            Add(new CheckNextComplicatorsSystem(contexts.game, contexts.complicators));
        }
    }
}