using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BBS
{
    public class LevelPanelUI : MonoBehaviour
    {
        [SerializeField] private Slider expBar;
        [SerializeField] private TextMeshProUGUI levelText;

        public void UpdateExpBarLevel(float MaxExp, int level)
        {
            expBar.maxValue = MaxExp;
            expBar.value = 0;
            levelText.text = $"Lv.{level.ToString()}";
        }

        public void UpdateExp(float exp) => expBar.value = exp;
    }
}
