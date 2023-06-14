using System.Collections;
using System.Collections.Generic;
using Animals;
using Entitas;
using UnityEngine;

namespace Core.GameStates
{
    public class StateSystems : Feature
    {
        public StateSystems(Contexts contexts)
        {
            //data systems
            Add(new IncreaseAnimalUnlockProgressSystem(contexts.game, contexts.data));
            //todo implement palyerturn state
            Add(new InitializeStateManagerSystem(contexts.game));
            Add(new StartMainMenuSystem(contexts.game, contexts.ui)); //change state and open screen
            Add(new StartLoadingStateSystem(contexts.input, contexts.game, contexts.ui));
            Add(new LoadNextLevelSystem(contexts.input, contexts.game, contexts.ui));
            Add(new LoadNextLevelByEventSystem(contexts.level, contexts.game, contexts.ui));
            Add(new LoadLevelEndSystem(contexts.game));
            Add(new StartMainGameSystem(contexts.game));//react when button pressed
            Add(new PreGameStartSystem(contexts.game, contexts.ui));
            Add(new PreGameEndSystem(contexts.input, contexts.game));
            // Add(new IntroEndedSystem(contexts.ui, contexts.game));
            Add(new VictoryEndLevelSystem(contexts.ui, contexts.game, contexts.data));
            Add(new LooseEndLevelSystem(contexts.ui, contexts.game));
            Add(new RestartLevelByLooseSystem(contexts.input, contexts.ui));
            // Add(new MainGameplayStartSystem(contexts.game, contexts.ui));
            Add(new CutsceneDetectionSystem(contexts.game));
            Add(new DebugStateReactSystem(contexts.game, contexts.ui));
            Add(new CheckLooseCondition(contexts.game, contexts.ui));
            // Add(new ComplicatorsImplementationStateSystem(contexts.complicators, contexts.game));
            // Input requests systems
            Add(new ReturnToHomeRequestSystem(contexts.input, contexts.game, contexts.ui));
            Add(new LoadNextGameLevelsSystem(contexts.input, contexts.game, contexts.data));
            Add(new AddAdditionalTurnsSystem(contexts.input, contexts.game, contexts.ui));


        }
    }

    public class AddAdditionalTurnsSystem : ReactiveSystem<InputEntity>
    {
        private readonly GameContext _gameContext;
        private readonly IGroup<UiEntity> _looseScreen;
        private readonly IGroup<UiEntity> _gameScreen;
        private const int TurnsIncrement = 2;

        public AddAdditionalTurnsSystem(InputContext contextsInput, GameContext contextsGame, UiContext contextsUI) : base(contextsInput)
        {
            _gameContext = contextsGame;
            _looseScreen = contextsUI.GetGroup(UiMatcher.LooseScreen);
            _gameScreen = contextsUI.GetGroup(UiMatcher.MainGameScreen);
        }

        protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
        {
            return context.CreateCollector(InputMatcher.ButtonRequestAddNewTurns);
        }

        protected override bool Filter(InputEntity entity)
        {
            return true;
        }

        protected override void Execute(List<InputEntity> entities)
        {
            var turnControllerEntity = _gameContext.turnControllerEntity;
            turnControllerEntity.ReplaceTurnsLimitByLevel(TurnsIncrement);
            turnControllerEntity.ReplaceTotalNumberOfCompletedTurns(0);
            _gameContext.stateManagerEntity.stateLoose = false;
            _gameContext.stateManagerEntity.stateMainGame = true;
            foreach (var uiEntity in _looseScreen)
            {
                uiEntity.triggerHide = true;
            }
            foreach (var uiEntity in _gameScreen)
            {
                uiEntity.triggerShow = true;
            }
        }
    }
}