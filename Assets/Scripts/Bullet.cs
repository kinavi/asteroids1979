using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Asteroids
{
    public class Bullet : Projectile
    {
        private void OnBecameInvisible()
        {
            Destroy(gameObject);
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Enemy"))
            {
                Destroy(gameObject);
            }
        }
    }
}