using System.Collections.Generic;
using Entitas;

namespace Ui
{
    public class UIUpdateUserDataMoneyDisplaySystem : ReactiveSystem<GameEntity>
    {
        private readonly UiContext _uiContext;
        private readonly GameContext _gameContext;
        private readonly IGroup<UiEntity> _uiMoneyDisplayGroup;

        public UIUpdateUserDataMoneyDisplaySystem(GameContext gameContext, UiContext uiContext) : base(gameContext)
        {
            _gameContext = gameContext;
            _uiContext = uiContext;
            _uiMoneyDisplayGroup = _uiContext.GetGroup(UiMatcher.AllOf(
                UiMatcher.AnyUserDataMoneyDisplayListener));
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.UserDataMoney);
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.isUserData;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var userData in entities)
            {
                foreach (var uiEntity in _uiMoneyDisplayGroup.GetEntities())
                {
                    uiEntity.ReplaceUserDataMoneyDisplay(userData.userDataMoney.value);
                }
            }
        }
    }
}