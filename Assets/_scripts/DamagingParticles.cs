using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagingParticles : MonoBehaviour
{
    public float damage = 10;

    ParticleSystem ps;
    List<ParticleCollisionEvent> collisionEvents;

    void Start() {
        ps = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    void OnParticleCollision(GameObject other) {
        int numCollisionEvents = ps.GetCollisionEvents(other, collisionEvents);

        Damageable toDamage = other.GetComponent<Damageable>();
        if (toDamage == null) {
            return;
        }

        for (int i = 0; i < numCollisionEvents; i++) {
            toDamage.OnDamage(damage);
        }
    }
}
