using System;

namespace Boosters
{
    public class BoosterSystems : Feature
    {
        // private ImplementBoosterReactSystem _implementBoosterReactSystem;
        public BoosterSystems(Contexts contexts)
        {
            Add(new CleanUpRequest(contexts.ui));
            
            Add(new InitializeBoosterInventorySystem(contexts.game));
            Add(new SelectOneInGameBoosterSystem(contexts.ui, contexts.game));
            Add(new BoosterAimingSystem(contexts.input, contexts.game, contexts.ui));
            Add(new ClearCellFocusBySelectionCancellingSystem(contexts.ui, contexts.game));
            Add(new ImplementBoosterReactSystem(contexts.input, contexts.game, contexts.ui));
            Add(new ImplementPreGameBoostersReactSystem(contexts.input, contexts.game, contexts.ui));
            Add(new ExecuteBoosterOpenAllForceSystem(contexts.ui, contexts.game));
            Add(new BlockBoosterInteractionSystem(contexts.game, contexts.ui));
            Add(new EnableBoosterInteractionSystem(contexts.game, contexts.ui));
            Add(new RiseFlagBoosterOpenAllWasUsedSystem(contexts.ui, contexts.game));//debug system
            #region booster implementation

            Add(new ImplementBoosterOpenOneMoreSystem(contexts.ui, contexts.game));

            #region pre-game boosters

            Add(new ImplementBoosterShowAllSystem(contexts.ui, contexts.game));

            #endregion
            
            #endregion
            
            Add(new DisableBoosterAimState(contexts.game, contexts.ui));
        }
    }
}
