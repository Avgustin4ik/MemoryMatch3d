using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

namespace Core.Input
{
    [Input, Unique]
    public sealed class TouchDataComponent : IComponent // основной компонент держатель инпут компонентов
    {
    }

    [Input]
    public sealed class TouchPhaseComponent : IComponent
    {
        /// <summary>
        /// Фаза касания
        /// </summary>
        public TouchPhase value;
    }

    [Input]
    public sealed class TouchDownPositionComponent : IComponent
    {
        /// <summary>
        /// Вектор начала касания
        /// </summary>
        public Vector2 value;
    }

    [Input]
    public sealed class TouchMovePositionComponent : IComponent
    {
        /// <summary>
        /// Вектор движения тача
        /// </summary>
        public Vector2 value;
    }

    [Input]
    public sealed class TouchUpPositionComponent : IComponent
    {
        /// <summary>
        /// Вектор конца касания
        /// </summary>
        public Vector2 value;
    }

    [Input, Event(EventTarget.Self)]
    public sealed class TouchSwipeDirectionComponent : IComponent
    {
        /// <summary>
        /// Направления свайпа
        /// </summary>
        public SwipeDirection value;
    }

    [Input]
    public sealed class TouchJoystickDirectionComponent : IComponent
    {
        /// <summary>
        /// Значение джостика по осям
        /// </summary>
        public Vector2 value;
    }
}