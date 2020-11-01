using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Weapons;


[CreateAssetMenuAttribute]
public class WeaponData : ScriptableObject {
    // This will have base stats for the weapon
    // I'm creating two different names for the weapon. One is the full name to be used in the weapon's bio, the short one will show up during gameplay if you switch to is
    public WeaponType weaponType;
    public AudioClip ShotSound;
    public FireMode FireMode;
    public ProjectileType ProjType;
    public GameObject Projectile;
    public float ProjVelocity; // If the weapon uses physical projectiles, what is the muzzle velocity?
    public ElementType Element;
    public float Damage;
    public float FireDelay; // Delay between mouse button being pressed and the weapon firing, just in case
    public float FireRate;
}


namespace Weapons{

    public enum FireMode {
        Semi, Auto, Burst
    }

    public enum WeaponType
    {
        Melee, Ballistic, Explosive
    }

    public enum ProjectileType {
        Raycast, Projectile
    }

    public enum ElementType {
        None, Fire, Poison
    }
    [Serializable]
    public struct ExplosionSettings{
        public GameObject explosion;
        public float explosionRadius;
        public float damageRadius;
        public float explosionPower;
        public AnimationCurve damageDropoff;
    }
}