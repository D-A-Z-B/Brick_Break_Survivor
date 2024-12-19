using BBS.Bullets;
using BBS.Core;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace BBS
{
    public class ResultPanel : MonoBehaviour
    {
        [SerializeField] private Transform cardPrefab;
        [SerializeField] private Transform cardContainer;
        [SerializeField] private Transform LevelPanel;
        [SerializeField] private CanvasGroup group;
        [SerializeField] private TextMeshProUGUI killCountText;
        [SerializeField] private TextMeshProUGUI maxHitText;
        [SerializeField] private Button retryBtn;
        [SerializeField] private Button titleBtn;

        private void Start()
        {
            //retryBtn.onClick.AddListener(() => SceneManager.LoadScene("GameScene"));
            //retryBtn.onClick.AddListener(() => SceneManager.LoadScene("TitleScene"));
        }

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.Q))
            {
                OnResultPanel();
            }
        }

        public void OnResultPanel()
        {
            killCountText.text = LevelManager.Instance.GetKillCount().ToString();
            maxHitText.text = GameManager.Instance.GetMaxHitCount().ToString();

            GetResultSkillCard();
            ShowPanel();
        }

        private void GetResultSkillCard()
        {
            foreach(BulletDataSO skill in BulletManager.Instance.PlayerBulletList)
            {
                if(skill != null && skill.icon != null)
                {
                    ResultSkillCard card = Instantiate(cardPrefab, cardContainer).GetComponent<ResultSkillCard>();
                    card.Init(skill.currentLevel, skill.icon);
                }
            }
        }

        private void ShowPanel()
        {
            Sequence seq = DOTween.Sequence();
            seq.Append(LevelPanel.DOScale(new Vector3(1.4f, 1.4f, 1f), 1f));
            seq.Join(LevelPanel.DOMoveY(25, 1f)
                .OnComplete(() => group.alpha = 1));
            seq.Append(LevelPanel.DOMoveY(940f, 1.5f));
            seq.Join(transform.DOMoveY(540, 1.5f));
        }
    }
}
