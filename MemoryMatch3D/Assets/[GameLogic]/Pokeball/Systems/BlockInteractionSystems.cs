using System.Collections.Generic;
using Entitas;

namespace Pokeball
{
    public class BlockInteractionSystems : Feature
    {
        public BlockInteractionSystems(Contexts contexts)
        {
            //todo сделать нормальную блокировку по лимиту
            Add(new BlockPokeballInteractionByLimit(contexts.game, contexts.ui));
            Add(new BlockInteractionDuringCutscene(contexts.game));//only intro now
        }
    }
}