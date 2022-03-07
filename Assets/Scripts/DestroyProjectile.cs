using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyProjectile : MonoBehaviour
{
    AudioSource fireAudio;

    // Start is called before the first frame update
    void Start()
    {
        fireAudio = GetComponent<AudioSource>();
        fireAudio.Play();
        
    }

    // Update is called once per frame
    void Update()
    {
        //Destroy the projectile if it flies out of game area
        var projectileX = transform.position.x;
        var projectileZ = transform.position.z;
        var projectileY = transform.position.y;
        if (projectileX > 60 || projectileX < -30 || projectileZ > 30 || projectileZ < -200)
        {
            Destroy(gameObject);
        }
        if (projectileY > 80 || projectileY < -40 )
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision co)
    {
        Destroy(gameObject);
    }
}
