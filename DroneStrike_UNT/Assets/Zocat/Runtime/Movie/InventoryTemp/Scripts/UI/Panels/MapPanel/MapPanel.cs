namespace Zocat
{
    public class MapPanel : UIPanel
    {
        public CustomButton Go;

        public override void Initialize()
        {
            base.Initialize();
            Go.InitializeClick(ClickGo);
        }

        private void ClickGo()
        {
            StreamingManager.CreateLevel();
        }
    }
}