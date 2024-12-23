using UnityEngine;

namespace KHJ.Core
{
    public class ScreenManager : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            Screen.SetResolution(1920, 1080, true);
        }
    }
}
