using Entitas;

namespace Core.GameStates
{
    public class CutsceneDetectionSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _detectionGroup;
        private readonly GameContext _gameContext;

        public CutsceneDetectionSystem(GameContext gameContext)
        {
            _gameContext = gameContext;
            _detectionGroup = gameContext.GetGroup(GameMatcher.AnyOf(
                GameMatcher.BoosterCutscene,
                GameMatcher.PreGame,
                GameMatcher.WrongPair,
                GameMatcher.SuccessCompare,
                GameMatcher.LoadingLevel,
                GameMatcher.Victory,
                GameMatcher.Loose,
                GameMatcher.ComplicatorsImplementation));
        }

        public void Execute()
        {
            var stateManagerEntity = _gameContext.stateManagerEntity;
            if (_detectionGroup.count > 0)
            {
                if (!stateManagerEntity.stateCutscene)
                {
                    stateManagerEntity.stateCutscene = true;
                }
            }
            else
            {
                if (stateManagerEntity.stateCutscene) stateManagerEntity.stateCutscene = false;
            }
        }
    }
}