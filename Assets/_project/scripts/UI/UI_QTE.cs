using CREMOT.GameplayUtilities;

namespace GFM2025
{
    public class UI_QTE : GenericSingleton<UI_QTE>
    {
        private void Start()
        {
            CloseUi();
        }

        public void OpenUi()
        {
            gameObject.SetActive(true);
        }

        public void CloseUi()
        {
            gameObject.SetActive(false);
        }
    }
}
