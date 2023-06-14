using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Core.Input
{
    public enum SwipeDirection
    {
        Up,
        Down,
        Left,
        Right
    }

    public class SwipeSystem : ReactiveSystem<InputEntity>
    {
        private readonly InputContext _inputContext;
        private InputEntity _inputDataEntity;
        private const float MinSwipeDist = 100f;

        public SwipeSystem(InputContext inputContext) : base(inputContext)
        {
            _inputContext = inputContext;
        }

        protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
        {
            return context.CreateCollector(InputMatcher.TouchPhase);
        }

        protected override bool Filter(InputEntity entity)
        {
            return _inputContext.touchDataEntity.touchPhase.value == TouchPhase.Ended;
        }

        protected override void Execute(List<InputEntity> entities)
        {
            _inputDataEntity = _inputContext.touchDataEntity;

            if (!SwipeDistanceCheckMet()) return;

            SwipeDirection direction;

            if (IsVerticalSwipe())
            {
                direction = _inputDataEntity.touchDownPosition.value.y - _inputDataEntity.touchUpPosition.value.y > 0
                    ? SwipeDirection.Up
                    : SwipeDirection.Down;
            }
            else
            {
                direction = _inputDataEntity.touchDownPosition.value.x - _inputDataEntity.touchUpPosition.value.x > 0
                    ? SwipeDirection.Right
                    : SwipeDirection.Left;
            }

            _inputDataEntity.ReplaceTouchSwipeDirection(direction);
        }

        private bool IsVerticalSwipe()
        {
            return VerticalMovementDistance() > HorizontalMovementDistance();
        }

        private bool SwipeDistanceCheckMet()
        {
            return VerticalMovementDistance() > MinSwipeDist || HorizontalMovementDistance() > MinSwipeDist;
        }

        private float VerticalMovementDistance()
        {
            return Mathf.Abs(_inputDataEntity.touchDownPosition.value.y - _inputDataEntity.touchUpPosition.value.y);
        }

        private float HorizontalMovementDistance()
        {
            return Mathf.Abs(_inputDataEntity.touchDownPosition.value.x - _inputDataEntity.touchUpPosition.value.x);
        }
    }
}