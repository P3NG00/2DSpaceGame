using System.Collections;
using SpaceGame.Ships;
using UnityEngine;

namespace SpaceGame.Items
{
    public interface ItemWeapon
    {
        GameObject UseWeapon(Ship source);
        IEnumerator UseRoutine(Ship ship);
    }
}