using UnityEngine;

namespace Scripts
{
    public abstract class BaseAnimation : MonoBehaviour, IAnimation
    {
        public abstract void Play();
        public abstract void Stop();
    }
}