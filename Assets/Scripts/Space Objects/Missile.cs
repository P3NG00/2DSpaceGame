using System.ComponentModel;
using SpaceGame.Settings;
using UnityEngine;

namespace SpaceGame.SpaceObjects
{
    public class Missile : MonoBehaviour
    {
        [Header("Info", order = 0)]
        public Weapon Weapon;

        [Header("References", order = 99)]
        [SerializeField] private new Rigidbody2D rigidbody;

        public Rigidbody2D Rigidbody => rigidbody;
    }
}
