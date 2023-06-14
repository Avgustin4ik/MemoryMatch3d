using Entitas;
using UnityEngine;

namespace Core.Input
{
    public class TouchSystem : IExecuteSystem
    {
        private readonly InputEntity _inputDataEntity;
        
        public TouchSystem(InputContext inputContext)
        {
            inputContext.isTouchData = true;
            _inputDataEntity = inputContext.touchDataEntity;

            _inputDataEntity.ReplaceTouchDownPosition(Vector2.zero);
            _inputDataEntity.ReplaceTouchMovePosition(Vector2.zero);
            _inputDataEntity.ReplaceTouchUpPosition(Vector2.zero);
            //for debug
            // _inputDataEntity.AddLastTimeTouchDetected(0);
            // _inputDataEntity.AddTouchForDebugCount(0);
        }

        public void Execute()
        {
#if UNITY_EDITOR || PLATFORM_STANDALONE_OSX
            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                Began();
                return;
            }
            if (UnityEngine.Input.GetMouseButton(0))
            {
                Moved();
                return;
            }
            if (UnityEngine.Input.GetMouseButtonUp(0)) Ended();
#elif !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IOS)
            if (UnityEngine.Input.touchCount != 1) return;

            if (UnityEngine.Input.GetTouch(0).phase == TouchPhase.Began)  {
                Began();
                return;
            }
            if (UnityEngine.Input.GetTouch(0).phase == TouchPhase.Moved ||
                UnityEngine.Input.GetTouch(0).phase == TouchPhase.Stationary)
            {
                Moved();
                return;
            }
            if (UnityEngine.Input.GetTouch(0).phase == TouchPhase.Ended) Ended();
#endif
        }

        private void Began()
        {
            _inputDataEntity.ReplaceTouchPhase(TouchPhase.Began);
            _inputDataEntity.ReplaceTouchDownPosition(UnityEngine.Input.mousePosition);
        }

        private void Moved()
        {
            _inputDataEntity.ReplaceTouchPhase(TouchPhase.Moved);
            _inputDataEntity.ReplaceTouchMovePosition(UnityEngine.Input.mousePosition);
        }

        private void Ended()
        {
            _inputDataEntity.ReplaceTouchPhase(TouchPhase.Ended);
            _inputDataEntity.ReplaceTouchUpPosition(UnityEngine.Input.mousePosition);
        }
    }
}