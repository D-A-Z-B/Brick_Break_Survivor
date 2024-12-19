using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BBS
{
    public class ResultSkillCard : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI levelText;
        [SerializeField] private Image iconImage;

        public void Init(int level, Sprite icon)
        {
            levelText.text = level.ToString();
            iconImage.sprite = icon;
        }
    }
}
