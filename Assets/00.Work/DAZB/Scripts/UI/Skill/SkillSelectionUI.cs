using System.Collections.Generic;
using BBS.Bullets;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


namespace BBS.UI.Skills {
    public enum CardType : short {
        left = 0, right, middle
    }
    public class SkillSelectionUI : MonoBehaviour {
        [SerializeField] private List<SkillCard> skillCards;
        [SerializeField] private List<BulletDataSO> bulletDataList;
        [SerializeField] private List<StatCardDataSO> statCardDataList;
        [SerializeField] private Image background;
        private int currentSelectedIndex = -1;
        private const int minIndex = 0;
        private const int maxIndex = 2;

        private void Update() {
            if (Keyboard.current.oKey.wasPressedThisFrame) {
                Open();
            }

            if (Keyboard.current.rightArrowKey.wasPressedThisFrame) {
    
                currentSelectedIndex = (currentSelectedIndex == -1) ? maxIndex : (currentSelectedIndex - 1 + maxIndex + 1) % (maxIndex + 1);
                UpdateCardUI();
            }
            else if (Keyboard.current.leftArrowKey.wasPressedThisFrame) {
                currentSelectedIndex = (currentSelectedIndex + 1) % (maxIndex + 1);
                UpdateCardUI();
            }

            if (Keyboard.current.enterKey.wasPressedThisFrame) {
                Selection();
            }
        }

        public void Open() {
            Time.timeScale = 0;

            List<BulletDataSO> shuffledBulletDataList = new List<BulletDataSO>(bulletDataList);
            List<StatCardDataSO> shuffledStatCardDataList = new List<StatCardDataSO>(statCardDataList);

            for (int i = 0; i < shuffledBulletDataList.Count; ++i) {
                int rand = Random.Range(0, shuffledBulletDataList.Count);
                (shuffledBulletDataList[i], shuffledBulletDataList[rand]) = (shuffledBulletDataList[rand], shuffledBulletDataList[i]);
            }

            for (int i = 0; i < shuffledStatCardDataList.Count; ++i) {
                int rand = Random.Range(0, shuffledStatCardDataList.Count);
                (shuffledStatCardDataList[i], shuffledStatCardDataList[rand]) = (shuffledStatCardDataList[rand], shuffledStatCardDataList[i]);
            }

            for (int i = 0; i < skillCards.Count; ++i) {
                if (shuffledBulletDataList[i].currentLevel >= 5) {
                    skillCards[i].SetCard(shuffledStatCardDataList[i]);
                }
                else {
                    skillCards[i].SetCard(shuffledBulletDataList[i]);
                }
            }

            Sequence sq = DOTween.Sequence();
            sq.SetUpdate(true);
            sq.Append(background.DOFade(0.4f, 1));
            sq.Join(skillCards[(int)CardType.left].transform.parent.DOScale(new Vector3(1, 1, 1), 1));
            sq.Join(skillCards[(int)CardType.left].RectTrm.DOAnchorPos(new Vector2(-280, 70), 1));
            sq.Join(skillCards[(int)CardType.middle].RectTrm.DOAnchorPos(new Vector2(0, 100), 1));
            sq.Join(skillCards[(int)CardType.right].RectTrm.DOAnchorPos(new Vector2(280, 70), 1));
            sq.Join(skillCards[(int)CardType.left].RectTrm.DORotate(new Vector3(0, 0, 8), 1));
            sq.Join(skillCards[(int)CardType.right].RectTrm.DORotate(new Vector3(0, 0, -8), 1));
        }

        public void Close() {
             float time = 0.5f;
             float scale = 1.5f;

            Sequence sq = DOTween.Sequence();
            sq.SetUpdate(true);
            sq.Append(skillCards[currentSelectedIndex].RectTrm.DORotate(new Vector3(0, 0, 0), time));
            sq.Join(skillCards[currentSelectedIndex].RectTrm.DOScale(scale, time));

            for (int i = 0; i < skillCards.Count; ++i) {
                if ((CardType)i == (CardType)currentSelectedIndex) {
                    continue;
                }
                sq.Join(skillCards[i].RectTrm.DOScale(new Vector3(1, 1, 1), time));
                sq.Join(skillCards[i].RectTrm.DORotate(new Vector3(0, 0, 160), time));
                sq.Join(skillCards[i].RectTrm.DOAnchorPosY(-1000, time));
            }
            sq.Append(background.DOFade(0, time));
            sq.Join(skillCards[currentSelectedIndex].RectTrm.DOAnchorPosX(0, time));

            time = 1f;
            sq.Append(skillCards[currentSelectedIndex].RectTrm.DOScale(0.0f, time));
            sq.Join(skillCards[currentSelectedIndex].RectTrm.DOAnchorPosY(0, time));
            sq.OnComplete(() => {
                Time.timeScale = 1;
                Refresh();
            });

        }

        public void Selection() {
            skillCards[currentSelectedIndex].Selection();
            Close();
        }

        private void Refresh() {
            skillCards[(int)CardType.left].transform.parent.localScale = new Vector3(0, 0, 0);

            skillCards[(int)CardType.left].RectTrm.localScale = new Vector3(1, 1, 1);
            skillCards[(int)CardType.right].RectTrm.localScale = new Vector3(1, 1, 1);
            skillCards[(int)CardType.middle].RectTrm.localScale = new Vector3(1, 1, 1);

            skillCards[(int)CardType.left].RectTrm.anchoredPosition = new Vector2(-280, -500);
            skillCards[(int)CardType.right].RectTrm.anchoredPosition = new Vector2(280, -500);
            skillCards[(int)CardType.middle].RectTrm.anchoredPosition = new Vector2(0, -500);

            skillCards[(int)CardType.left].RectTrm.DORotate(new Vector3(0, 0, 0), 0);
            skillCards[(int)CardType.right].RectTrm.DORotate(new Vector3(0, 0, 0), 0);
            skillCards[(int)CardType.middle].RectTrm.DORotate(new Vector3(0, 0, 0), 0);
        }

        private void UpdateCardUI() {
            float time = 0.2f;

            Sequence sq = DOTween.Sequence();
            sq.SetUpdate(true);
            sq.Append(skillCards[currentSelectedIndex].RectTrm.DOAnchorPosY(skillCards[currentSelectedIndex].RectTrm.localPosition.y + 30, time));
            switch ((CardType)currentSelectedIndex) {
                    case CardType.left: {
                        sq.Join(skillCards[currentSelectedIndex].transform.DORotate(new Vector3(0, 0, 15), time));
                        break;
                    }
                    case CardType.right: {
                        sq.Join(skillCards[currentSelectedIndex].transform.DORotate(new Vector3(0, 0, -15), time));
                        break;
                    }
                }

            for (int i = 0; i < skillCards.Count; ++i) {
                if ((CardType)i == (CardType)currentSelectedIndex) {
                    continue;
                }

                sq.Join(skillCards[i].transform.DOScale(new Vector3(1, 1, 1), time));
                switch ((CardType)i) {
                    case CardType.left: {
                        sq.Join(skillCards[i].transform.DORotate(new Vector3(0, 0, 8), time));
                        sq.Join(skillCards[i].RectTrm.DOAnchorPos(new Vector2(-280, 70), time));
                        break;
                    }
                    case CardType.right: {
                        sq.Join(skillCards[i].transform.DORotate(new Vector3(0, 0, -8), time));
                        sq.Join(skillCards[i].RectTrm.DOAnchorPos(new Vector2(280, 70), time));
                        break;
                    }
                    case CardType.middle: {
                        sq.Join(skillCards[i].RectTrm.DOAnchorPos(new Vector2(0, 100), time));
                        break;
                    }
                }
            }
        }
    }
}
