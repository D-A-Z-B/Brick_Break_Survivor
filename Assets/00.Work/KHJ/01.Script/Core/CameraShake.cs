using KHJ.Core;
using Unity.Cinemachine;
using UnityEngine;

namespace KHJ.Camera
{
    public class CameraShake : MonoSingleton<CameraShake>
    {
        [SerializeField] private CinemachineImpulseSource _impulseSource;

        public void Shake(float strength)
        {
            _impulseSource.GenerateImpulse(strength);
        }
    }
}
