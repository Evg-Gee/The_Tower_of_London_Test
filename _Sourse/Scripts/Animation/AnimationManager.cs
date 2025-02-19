using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class AnimationManager : MonoBehaviour
    {
        public static AnimationManager Instance { get; private set; }

        [SerializeField] private GameObject successParticlePrefab;
        [SerializeField] private ParticleSystem ringPlacementParticle;
        private Dictionary<string, IAnimation> animations = new Dictionary<string, IAnimation>();

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        private void Start()
        {
            AnimationManager.Instance.RegisterAnimation("SuccessAnimation", new SuccessAnimation(successParticlePrefab));
            AnimationManager.Instance.RegisterAnimation("RingPlacementAnimation", new RingPlacementAnimation(ringPlacementParticle));
        }
        public void RegisterAnimation(string animationName, IAnimation animation)
        {
            if (!animations.ContainsKey(animationName))
            {
                animations[animationName] = animation;
            }
        }

        public void PlayAnimation(string animationName)
        {
            if (animations.ContainsKey(animationName))
            {
                animations[animationName].Play();
            }
            else
            {
                Debug.LogWarning($"Animation '{animationName}' not found.");
            }
        }

        public void StopAnimation(string animationName)
        {
            if (animations.ContainsKey(animationName))
            {
                animations[animationName].Stop();
            }
            else
            {
                Debug.LogWarning($"Animation '{animationName}' not found.");
            }
        }

        public void PlaySuccessAnimation()
        {
            PlayAnimation("SuccessAnimation");
        }

        public void PlayRingMoveAnimation(Transform ringTransform, Vector3 targetPosition, float duration)
        {
            var animation = new RingMoveAnimation(ringTransform, targetPosition, duration);
            animation.Play();
        }

        public void PlayRingPlacementAnimation(ParticleSystem particleEffect)
        {
            var animation = new RingPlacementAnimation(particleEffect);
            animation.Play();
        }
    }
}