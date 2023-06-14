using Entitas;

namespace Core.Extension
{
    public interface IViewObject
    {
        void Init(IEntity iEntity);

        void OnOnDestroyEntity(IEntity e);
    }
}