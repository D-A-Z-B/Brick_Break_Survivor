using UnityEngine;

namespace BBS
{
    public class Test : MonoBehaviour
    {
        private void Update()
        {
            if(Input.GetMouseButton(0))
            {
                Vector3 mousePos = Input.mousePosition;
                mousePos.z = Camera.main.farClipPlane;
                LevelManager.Instance.CreateExp(Camera.main.ScreenToWorldPoint(mousePos));
            }
        }
    }
}
