using UnityEngine;

namespace Scripts
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance { get; private set; }

        [SerializeField] private AudioSource successSound;
        [SerializeField] private AudioSource moveSound;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void PlaySuccessSound()
        {
            successSound?.Play();
        }

        public void PlayMoveSound()
        {
            moveSound?.Play();
        }
    }
}
