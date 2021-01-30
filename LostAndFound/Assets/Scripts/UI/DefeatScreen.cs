using System;
using JetBrains.Annotations;
using UnityEngine;

namespace UI
{
    public class DefeatScreen : UIScreen
    {
        [UsedImplicitly]
        public void TryAgainButtonClicked()
        {
            GameMaster.ReloadCurrentLevel();
            Close();
        }
    }
}
