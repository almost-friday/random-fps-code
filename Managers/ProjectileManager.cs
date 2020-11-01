using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    [SerializeField] private float velocity = 50;

    private Vector3 lastPosition;
    private RaycastHit hit;
    private float _damage;
    public float damage
    {
        private get { return _damage; }
        set {
            _damage = value;
            if (value > 200) _damage = 200;
            if (value < 0) _damage = 0;
        }
    }
 
    void Update()
    {
        lastPosition = transform.position;
        transform.Translate(transform.forward * velocity * Time.deltaTime, Space.World);
        if (Physics.Linecast(lastPosition, transform.position, out hit))
        {
            if (hit.collider.tag == "Enemy")
            {
                EnemyHealth health = hit.collider.GetComponent<EnemyHealth>();
                if (health != null) health.TakeDamage(_damage);
                Destroy(gameObject);
            }
            if (hit.collider.tag == "Untagged")
            {
                Destroy(gameObject);
            }
        }
    }
}
