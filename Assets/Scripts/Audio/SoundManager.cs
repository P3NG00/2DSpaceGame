using UnityEngine;

namespace SpaceGame.Audio
{
    public sealed class SoundManager : MonoBehaviour
    {
        // TODO make this able to control the volume of all sounds in game
        // that includes background music
        // connect to in-game interface

        [Header("Info [SoundManager]", order = 0)]
        [SerializeField, Range(0f, 1f)] private float volumeCollision = 1f;
        [SerializeField, Range(0f, 1f)] private float volumeMissile = 1f;
        [SerializeField] private AudioClip[] clipsCollision;
        [SerializeField] private AudioClip[] clipsMissile;

        public void PlayCollision(AudioSource audioSource) => audioSource.PlayOneShot(this.clipsCollision[Random.Range(0, this.clipsCollision.Length)], this.volumeCollision);
        public void PlayMissile(AudioSource audioSource) => audioSource.PlayOneShot(this.clipsMissile[Random.Range(0, this.clipsMissile.Length)], this.volumeMissile);
    }
}
