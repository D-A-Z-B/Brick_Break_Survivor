using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace BBS
{
    public class TitleUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private float speed;

        private float angle;

        private void Update() {
            angle += Time.deltaTime * speed;
            if (angle >= 360f) {
                angle -= 360f;
            }

            float cosValue = Mathf.Cos(angle * Mathf.Deg2Rad);
            float alpha = (cosValue + 1) / 2f;

            text.color = new Color(1f, 1f, 1f, alpha);

            if (Mouse.current.leftButton.wasPressedThisFrame || Mouse.current.rightButton.wasPressedThisFrame) {
                SceneManager.LoadScene("Dazb_Test");
            }
        }

    }
}
