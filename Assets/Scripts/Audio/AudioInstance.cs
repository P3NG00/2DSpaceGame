using UnityEngine;

namespace SpaceGame
{
    public sealed class AudioInstance : MonoBehaviour
    {
        [Header("References [AudioInstance]", order = 99)]
        [SerializeField] private AudioSource audioSource;

        public AudioSource AudioSource => this.audioSource;

        public static void PlaySoundAt(Vector2 position, AudioClip clip, float volume)
        {
            AudioInstance instance = Instantiate(GameInfo.AudioInstancePrefab, position, Quaternion.identity);
            instance.audioSource.PlayOneShot(clip, volume);
            Destroy(instance.gameObject, clip.length + 1);
        }
    }
}
