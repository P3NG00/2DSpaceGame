using System.Collections;
using SpaceGame.Ships;
using UnityEngine;

namespace SpaceGame.Items
{
    public abstract class ItemInfoWeapon : ItemInfoEffect
    {
        public abstract GameObject UseWeapon(Ship source);

        public abstract IEnumerator UseRoutine(Ship ship);
    }
}
