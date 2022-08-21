using UnityEngine;
using Cinemachine;
using UnityEngine.Assertions;

namespace Assets.DungeonRogue.Scripts
{ 
    public class CinemachinePOVExtension : CinemachineExtension
    {
        [SerializeField]
        private Transform targetRotationTransform = default;

        protected override void Awake()
        {
            base.Awake();

            Assert.IsNotNull(targetRotationTransform);
        }

        protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
        {
            if ((vcam.Follow != null) &&
                (stage == CinemachineCore.Stage.Aim))
            {            
                state.RawOrientation = targetRotationTransform.rotation;
            }
        }
    }
}