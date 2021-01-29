
using UnityEngine;

namespace General
{
    public class FollowCursorBehaviour : SceneObject
    {
        void Update()
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }
}
