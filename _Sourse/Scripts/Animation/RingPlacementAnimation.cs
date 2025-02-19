using UnityEngine;

namespace Scripts
{
    public class RingPlacementAnimation : BaseAnimation
    {
        private ParticleSystem particleEffect;

        public RingPlacementAnimation(ParticleSystem particleEffect)
        {
            this.particleEffect = particleEffect;
        }

        public override void Play()
        {
            if (particleEffect != null)
            {
                particleEffect.Play();
                particleEffect.GetComponent<ParticleSystem>().Play();
            }
        }

        public override void Stop()
        {
            if (particleEffect != null)
            {
                particleEffect.Stop();
            }
        }
    }
}