using BBS.Bullets;
using BBS.Core;
using DG.Tweening;
using KHJ.Core;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace BBS
{
    public class ResultPanel : MonoSingleton<ResultPanel>
    {
        [SerializeField] private Transform cardPrefab;
        [SerializeField] private Transform cardContainer;
        [SerializeField] private Transform LevelPanel;
        [SerializeField] private CanvasGroup group;
        [SerializeField] private TextMeshProUGUI killCountText;
        [SerializeField] private TextMeshProUGUI maxHitText;
        [SerializeField] private Button retryBtn;
        [SerializeField] private Button titleBtn;
        [SerializeField] private TextMeshProUGUI titleText;

        private void Start()
        {
            retryBtn.onClick.AddListener(() => {
                SceneManager.LoadScene("GameScene");
                Debug.Log("Click");
            });
            titleBtn.onClick.AddListener(() => SceneManager.LoadScene("Title"));
        }

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.Q))
            {
                OnResultPanel(false);
            }
            if (Input.GetKeyUp(KeyCode.R))
            {
                OnResultPanel(true);
            }
        }

        public void OnResultPanel(bool isClear)
        {
            if(isClear)
                titleText.text = "game clear";
            else
                titleText.text = "game end";

            ResultText();
        }

        public void ResultText()
        {
            titleText.color = Color.white; 
            titleText.transform.DOScale(Vector3.one, 1f).SetUpdate(true).SetEase(Ease.InSine)
                .OnComplete(() =>
                {
                    killCountText.text = LevelManager.Instance.GetKillCount().ToString();
                    maxHitText.text = GameManager.Instance.GetMaxHitCount().ToString();

                    GetResultSkillCard();
                    ShowPanel();
                });
        }

        private void GetResultSkillCard()
        {
            foreach (BulletDataSO skill in BulletManager.Instance.PlayerBulletList)
            {
                if (skill != null && skill.icon != null)
                {
                    ResultSkillCard card = Instantiate(cardPrefab, cardContainer).GetComponent<ResultSkillCard>();
                    card.Init(skill.currentLevel, skill.icon);
                }
            }
        }

        private void ShowPanel()
        {
            LevelPanel.GetComponent<CanvasGroup>().alpha = 1f;
            group.interactable = true;
            Sequence seq = DOTween.Sequence().SetUpdate(true);
            seq.Append(LevelPanel.DOScale(new Vector3(1.4f, 1.4f, 1f), 1f));
            seq.Join(LevelPanel.DOMoveY(25, 1f)
                .OnComplete(() => group.alpha = 1));
            seq.Append(LevelPanel.DOMoveY(940f, 1.5f));
            seq.Join(transform.DOMoveY(540, 1.5f));
        }
    }
}
