using SpaceGame.SpaceObjects;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceGame.UI
{
    public sealed class UISpawnSlot : MonoBehaviour
    {
        [Header("References", order = 99)]
        [SerializeField] private Image image;
        [SerializeField] private RectTransform rectTransform;

        private SpaceObjectSpawnableInfo spawnableInfo;

        public RectTransform RectTransform => this.rectTransform;

        public void SetSpawnable(SpaceObjectSpawnableInfo spawnable)
        {
            this.spawnableInfo = spawnable;
            this.image.color = spawnable.Color;
        }

        public void CallbackButton_SpawnSpawnable() => GameInfo.SpawnSpaceObject(this.spawnableInfo, GameInfo.Player.Position);
    }
}
