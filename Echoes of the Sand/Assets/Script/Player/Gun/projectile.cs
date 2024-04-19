using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    [SerializeField] private float speed = 10;
    [SerializeField] private float damage = 25;
    [SerializeField] private float lifeTime = 10;


      void Start()
    {
        Invoke(nameof(DestroyProjectile), lifeTime);
    }
    
    void Update()
    {
        transform.Translate(Vector3.forward * (speed * Time.deltaTime));
    }
 
    private void DestroyProjectile()
    {
        Destroy(this.gameObject);
    }
    public float getDamage() => damage;
}
