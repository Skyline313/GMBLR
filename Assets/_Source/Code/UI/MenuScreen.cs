using Kuhpik;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Source.Code.UI
{
    public class MenuScreen : UIScreen
    {
        public Button SettingsButton;
        public Button InfoButton;
        public Button StartGameButton;
        public Button ChangeGameButton;
        public TMP_Text CoinsText;
        public TMP_Text GameInfoText;
        public Image GameImage;

        public GameObject InfoPanel;
        public Button CloseInfoPanelButton;
        
        public override void Subscribe()
        {
            base.Subscribe();
        }
    }
}