using UnityEngine;

namespace SpaceGame.Audio
{
    [System.Serializable]
    public sealed class SoundInfo
    {
        [SerializeField, Range(0f, 1f)] private float volume = 1f;
        [SerializeField] private AudioClip[] clips;

        public float Volume => this.volume;
        public AudioClip RandomClip => this.clips[Random.Range(0, this.clips.Length)];
    }
}
