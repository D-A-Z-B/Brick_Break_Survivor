using System.Collections.Generic;
using BBS.Bullets;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public enum CardType : short {
    left = 0, right, middle
}

namespace BBS.UI.Skills {
    public class SkillSelectionUI : MonoBehaviour {
        [SerializeField] private List<SkillCard> skillCards;
        [SerializeField] private List<BulletDataSO> bulletDataList;
        [SerializeField] private Image background;

        private void Update() {
            if (Keyboard.current.oKey.wasPressedThisFrame) {
                Open();
            }
        }

        public void Open() {
            for (int i = 0; i < skillCards.Count; ++i) {
                int rand = Random.Range(0, skillCards.Count);
                skillCards[i].SetCard(bulletDataList[rand]);
            }

            Sequence sq = DOTween.Sequence();
            sq.Append(background.DOFade(0.4f, 1));
            sq.Join(skillCards[(int)CardType.left].transform.parent.DOScale(new Vector3(1, 1, 1), 1));
            sq.Join(skillCards[(int)CardType.left].RectTrm.DOAnchorPos(new Vector2(-280, 70), 1));
            sq.Join(skillCards[(int)CardType.middle].RectTrm.DOAnchorPos(new Vector2(0, 100), 1));
            sq.Join(skillCards[(int)CardType.right].RectTrm.DOAnchorPos(new Vector2(280, 70), 1));
            sq.Join(skillCards[(int)CardType.left].RectTrm.DORotate(new Vector3(0, 0, 8), 1));
            sq.Join(skillCards[(int)CardType.right].RectTrm.DORotate(new Vector3(0, 0, -8), 1));
        }

        public void Close() {

        }

        public void Selection() {
            
        }
    }
}
