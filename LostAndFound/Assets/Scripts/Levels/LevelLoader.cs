﻿using System.Collections.Generic;
using Cinemachine;
using JetBrains.Annotations;
using UnityEngine;

namespace Levels
{
    public class LevelLoader : SceneObject
    {
        [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
        [SerializeField] private List<LevelBehaviour> levels;

        [CanBeNull] private LevelBehaviour currentlyLoadedLevel;

        public int LastLevelIndex => levels.Count - 1;
    
        public void LoadLevel(int i)
        {
            if (i >= 0 && i < levels.Count)
            {
                LevelBehaviour newLevel = Instantiate(levels[i], transform);
                cinemachineVirtualCamera.Follow = newLevel.CameraTarget;
            
                CleanUp();

                currentlyLoadedLevel = newLevel;
            }
        }

        public void CleanUp()
        {
            if (currentlyLoadedLevel != null)
            {
                Destroy(currentlyLoadedLevel.gameObject);
                currentlyLoadedLevel = null;
            }
        }
    }
}
