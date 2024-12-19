using DG.Tweening;
using UnityEngine;

namespace BBS
{
    public class ResultPanel : MonoBehaviour
    {
        [SerializeField] private Transform LevelPanel;
        [SerializeField] private CanvasGroup group;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                OnResultPanel();
            }
        }

        public void OnResultPanel()
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
