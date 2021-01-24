using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Asteroids
{
    public class Enemy : Projectile
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Bullet"))
            {
                Destroy(gameObject);
                GameController.instance.SetPoint();
            }
        }
        
        private void OnBecameInvisible()
        {
            Destroy(gameObject);
        }
    }
}