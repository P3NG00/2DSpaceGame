using UnityEngine;

namespace SpaceGame.Audio
{
    public sealed class SoundManager : MonoBehaviour
    {
        // TODO make this able to control the volume of all sounds in game
        // that includes background music
        // connect to in-game interface

        // TODO switch everything that makes sound create a
        // new object to play the sound so the object that might
        // get destroyed doesn't stop playing the sound once destroyed

        [Header("Info [SoundManager]", order = 0)]

        [SerializeField, Range(0f, 1f)] private float volumeCollision = 1f;
        [SerializeField] private AudioClip[] clipsCollision;

        [SerializeField, Range(0f, 1f)] private float volumeMissile = 1f;
        [SerializeField] private AudioClip[] clipsMissile;

        [SerializeField, Range(0f, 1f)] private float volumeBooster = 1f;
        [SerializeField] private AudioClip[] clipsBooster;

        public void PlayCollision(AudioSource audioSource) => this.PlaySound(audioSource, this.clipsCollision, this.volumeCollision);
        public void PlayMissile(AudioSource audioSource) => this.PlaySound(audioSource, this.clipsMissile, this.volumeMissile);
        public void PlayBooster(AudioSource audioSource) => this.PlaySound(audioSource, this.clipsBooster, this.volumeBooster);

        private void PlaySound(AudioSource audioSource, AudioClip[] clips, float volume) => audioSource.PlayOneShot(clips[Random.Range(0, clips.Length)], volume);
    }
}
