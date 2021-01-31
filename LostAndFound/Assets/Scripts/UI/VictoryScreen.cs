using JetBrains.Annotations;

namespace UI
{
    public class VictoryScreen : UIScreen
    {
        [UsedImplicitly]
        public void OnContinueButtonClicked()
        {
            Close(UIController.OpenMainMenu);
        }
    }
}
