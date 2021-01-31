using JetBrains.Annotations;
using UnityEngine;

namespace UI
{
    public class MainMenuScreen : UIScreen
    {
        [SerializeField] private GameObject eyeAnimation;
        [SerializeField] private GameObject tutorial;

        private bool flag;
        
        [UsedImplicitly]
        public void OnPlayButtonClicked()
        {
            GameMaster.StartLevel(0);
            Close();
        }

        [UsedImplicitly]
        public void OnExitButtonClicked()
        {
            Application.Quit();
        }
        
        [UsedImplicitly]
        public void OnTutorialButtonClicked()
        {
            eyeAnimation.SetActive(flag);
            tutorial.SetActive(!flag);
            flag = !flag;
        }
    }
}
