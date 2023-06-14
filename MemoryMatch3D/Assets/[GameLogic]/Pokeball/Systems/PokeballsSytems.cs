using System;
using Entitas;

namespace Pokeball
{
    public class PokeballsSytems : Systems
    {
        public PokeballsSytems(Contexts contexts)
        {
            //todo включение и отключение систeм
            Add(new HideAllAnimalAtIntroEndedSystem(contexts.game));
            Add(new ShowAnimalByClickSystem(contexts.game));
            Add(new PokeballsComparerSystem(contexts.game));
            Add(new SuccessCompareVFX(contexts.game, contexts.ui));
            Add(new PokeballsMatchLogicSystem(contexts.game));
            // Add(new DetectMistakeSystem(contexts.game)); //см. HideAllOpenPokeballsDuringTrunEndSystem
            Add(new DetectVictorySystem(contexts.game));
            Add(new BlockInteractionSystems(contexts)); //todo add cutscene
            Add(new ReturnIdleAnimationToAnimals(contexts.game));
            Add(new HideAllOpenPokeballsDuringTurnEndSystem(contexts.game));
            Add(new DetectBorderPokeballSystem(contexts.game));
            Add(new IncreaseAnimationSpeedByClickSystem(contexts.game));
            Add(new ReturnAnimationSpeedToDefault(contexts.game));


        }
    }
}