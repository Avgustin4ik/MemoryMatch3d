using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Core.Common
{
    [Game, Unique, FlagPrefix("isStatic")]
    public sealed class AdsCompleteLevelRewardComponent : IComponent
    {
        // Тип награды: Получение дополнительной награды за прохождение уровня
    }

    [Game, Unique, FlagPrefix("isStatic")]
    public sealed class AdsStoreRewardComponent : IComponent
    {
        // Тип награды: Денежная награда в магазине
    }

    [Game, Unique, FlagPrefix("isStatic")]
    public sealed class AdsPartStoreItemRewardComponent : IComponent
    {
        // Тип награды: Получение части предмета в магазине
    }

    [Game, Unique, FlagPrefix("isStatic")]
    public sealed class AdsRevivalRewardComponent : IComponent
    {
        // Тип награды: Возрождение после смерти
    }

    [Game]
    public sealed class AdsGameShowRequestComponent : IComponent
    {
        // Регестрирует нажатие на просмотр рекламы
    }

    [Game, Event(EventTarget.Self)]
    public sealed class AdsActiveStateComponent : IComponent
    {
        //Сосотяние рекламы, доступна ли игроку
        public bool value;
    }

    //[Game, Event(EventTarget.Self)]
    //public class AdsMoneyRewardComponent : IComponent
    //{
    //    //Денежная награда за просмотр рекламы
    //    public int value;
    //}

    [Game]
    public sealed class AdsShowCounterComponent : IComponent
    {
        /// <summary>
        /// Счетчик показов за сессию
        /// </summary>
        public int value;
    }


    [Game, Event(EventTarget.Self), Cleanup(CleanupMode.RemoveComponent)]
    public sealed class AdsRewardResultComponent : IComponent
    {
        /// <summary>
        /// Результат просмотра рекламы
        /// </summary>
        public bool value;
    }
}