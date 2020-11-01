
using UnityEngine;
using Weapons;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private WeaponData currentWeaponData;
    [SerializeField] private Transform weaponRoot;

    private float timer = 0;

    bool flag;
    private void Start()
    {
        flag = currentWeaponData.FireMode == FireMode.Auto;
    }
    // Update is called once per frame
    private void Update()
    {
        if (timer < 10) timer += Time.deltaTime;
        if (Input.GetMouseButton(0) && timer > currentWeaponData.FireRate && flag)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        timer = 0;
        if (currentWeaponData.ProjType == ProjectileType.Projectile)
        {
            GameObject bullet = Instantiate(currentWeaponData.Projectile, weaponRoot.position, weaponRoot.rotation);
            bullet.GetComponent<ProjectileManager>().damage = currentWeaponData.Damage;
        }
    }
}
