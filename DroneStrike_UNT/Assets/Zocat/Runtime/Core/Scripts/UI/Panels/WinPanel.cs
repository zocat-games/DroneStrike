using TMPro;

namespace Zocat
{
    public class WinPanel : UIPanel
    {
        public TextMeshProUGUI LevelTmp;
        public CustomButton GoBtn;

        public override void Initialize()
        {
            base.Initialize();
            GoBtn.InitializeClick(StreamingManager.ShowUi);
        }

        public override void Show()
        {
            base.Show();

            LevelTmp.text = $"{LevelIndexManager.CurrentIndex.PlusOne()}. Level";
            AudioManager.PlaySfx(SfxType.collect_item_16);
        }
    }
}