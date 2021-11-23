using UnityEngine;

namespace SpaceGame.Audio
{
    public sealed class SoundManager : MonoBehaviour
    {
        // TODO make this able to control the volume of all sounds in game
        // that includes background music
        // connect to in-game interface

        [Header("Info [SoundManager]", order = 0)]
        [SerializeField] private SoundInfo soundCollision;
        [SerializeField] private SoundInfo soundMissile;
        [SerializeField] private SoundInfo soundBooster;

        public SoundInfo SoundCollision => this.soundCollision;
        public SoundInfo SoundMissile => this.soundMissile;
        public SoundInfo SoundBooster => this.soundBooster;

        public void PlaySound(SoundInfo soundInfo, Vector2 position)
        {
            AudioSource audioSource = Instantiate(GameInfo.AudioInstancePrefab, position, Quaternion.identity).AudioSource;
            AudioClip clip = soundInfo.RandomClip;
            audioSource.PlayOneShot(clip, soundInfo.Volume);
            Destroy(audioSource.gameObject, clip.length);
        }

        public void PlaySound(SoundInfo soundInfo, AudioSource audioSource) => audioSource.PlayOneShot(soundInfo.RandomClip, soundInfo.Volume);
    }
}
