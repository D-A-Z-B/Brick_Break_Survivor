using DG.Tweening;
using UnityEngine;

namespace KHJ.UI
{
    public class TestUI : MonoBehaviour
    {
        private RectTransform rectrm => transform as RectTransform;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                Sequence sq = DOTween.Sequence();
                sq.Append(rectrm.DORotate(new Vector3(180, 180, rectrm.rotation.z), 1)).SetEase(Ease.Linear);
                sq.Join(rectrm.DOScale(Vector3.zero, 1)).SetEase(Ease.Linear);
            }
        }
    }
}
