using UnityEngine;

namespace Levels
{
    public class LevelBehaviour : SceneObject
    {
        [SerializeField] private Transform cameraTarget;

        public Transform CameraTarget => cameraTarget;
    }
}
