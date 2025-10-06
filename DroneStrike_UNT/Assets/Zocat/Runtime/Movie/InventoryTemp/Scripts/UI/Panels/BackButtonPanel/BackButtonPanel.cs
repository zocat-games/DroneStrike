namespace Zocat
{
    public class BackButtonPanel : UIPanel
    {
        public CustomButton BackBtn;

        public override void Initialize()
        {
            base.Initialize();
            BackBtn.InitializeClick(Click);
        }

        private void Click()
        {
            StreamingManager.ExitLevel();
        }
    }
}