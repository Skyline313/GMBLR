using Kuhpik;
using UnityEngine;
using UnityEngine.UI;

namespace _Source.Code.UI
{
    public class SettingsScreen : UIScreen
    {
        public Button BackButton;
        public Button SoundButton;
        public Button VibroButton;
        public Button ClearDataButton;
        public Button PrivacyButton;
        public Button TermsButton;

        public Image SoundToggleImage;
        public Image VibroToggleImage;

        public Sprite ToggleOn;
        public Sprite ToggleOff;
    }
}