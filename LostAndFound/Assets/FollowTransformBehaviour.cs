using UnityEngine;

public class FollowTransformBehaviour : SceneObject
{
    [SerializeField] private Transform transformToFollow;

    void Update()
    {
        if (transformToFollow != null)
        {
            transform.position = transformToFollow.position;
        }
    }
}
