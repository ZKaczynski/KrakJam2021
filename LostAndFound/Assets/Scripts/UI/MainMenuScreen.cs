using JetBrains.Annotations;
using UnityEngine;

namespace UI
{
    public class MainMenuScreen : UIScreen
    {
        [UsedImplicitly]
        public void OnPlayButtonClicked()
        {
            GameMaster.StartLevel(0);
            Close();
        }
    }
}
