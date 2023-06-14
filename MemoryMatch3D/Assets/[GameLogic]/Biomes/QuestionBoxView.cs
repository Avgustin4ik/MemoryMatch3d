using Core.Extension;
using Entitas;

namespace Biomes
{
    class QuestionBoxView : MonoBehAdvGame, IGameHideListener, IGameShowListener
    {
        public override void Init(IEntity iEntity)
        {
            base.Init(iEntity);
            GameEntity.isQuestionBox = true;
            GameEntity.AddGameHideListener(this);
            GameEntity.AddGameShowListener(this);
        }

        public void OnHide(GameEntity entity)
        {
            gameObject.SetActive(false);
        }

        public void OnShow(GameEntity entity)
        {
            gameObject.SetActive(true);
        }
    }
}