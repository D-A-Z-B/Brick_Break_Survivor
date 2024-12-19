using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace BBS.UI {
    public class HitCountUI : MonoBehaviour, IPoolable
    {
        private TextMeshProUGUI text;

        public PoolTypeSO PoolType {get; set;}

        public GameObject GameObject => gameObject;

        public RectTransform RectTrm => transform as RectTransform;

        private Pool myPool;

        public void SetText(string text) {
            this.text.text = text;
        }

        public void ResetItem()
        {
            Routine();
            text.color = new Color(1, 1, 1, 1);
            text.rectTransform.anchoredPosition = new Vector2(-210, 210);
        }

        public void SetUpPool(Pool pool)
        {
            text = GetComponentInChildren<TextMeshProUGUI>();
            myPool = pool;
        }

        private void Routine() {
            Sequence sq = DOTween.Sequence();
            sq.SetUpdate(true);
            sq.Append(text.rectTransform.DOAnchorPosY(text.rectTransform.anchoredPosition.y + 100, 1.5f));
            sq.Join(text.DOFade(0, 1.5f));
            sq.OnComplete(() => myPool.Push(this));
        }
    }
}
