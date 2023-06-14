using System;
using Cinemachine;
using Core.Extension;
using Entitas;
using UnityEngine;

namespace CinemachineCamera
{
    [RequireComponent(typeof(CinemachineVirtualCamera))]
    internal class VirtualCameraView : MonoBehAdvGame, IPriorityListener
    {
        private CinemachineVirtualCamera _cmCamera;

        private void Awake()
        {
            _cmCamera = GetComponent<CinemachineVirtualCamera>();
        }

        public override void Init(IEntity iEntity)
        {
            base.Init(iEntity);
            GameEntity.isVirtualCamera = true;
            GameEntity.AddPriority(0);
            GameEntity.AddPriorityListener(this);
        }

        public void OnPriority(GameEntity entity, int value)
        {
            _cmCamera.Priority = value;
        }
    }
}