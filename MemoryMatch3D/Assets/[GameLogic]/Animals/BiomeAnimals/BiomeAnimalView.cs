using Core.Extension;
using Entitas;

namespace Animals.BiomeAnimal
{
    public class BiomeAnimalView : MonoBehAdvGame, IGameHideListener, IGameShowListener
    {
        public override void Init(IEntity iEntity)
        {
            base.Init(iEntity);
            GameEntity.isBiomeAnimal = true;
            GameEntity.AddGameHideListener(this);
            GameEntity.AddGameShowListener(this);
        }

        public void OnHide(GameEntity entity)
        {
            //temp
            gameObject.SetActive(false);
        }

        public void OnShow(GameEntity entity)
        {
            //temp
            gameObject.SetActive(true);
        }
    }
}