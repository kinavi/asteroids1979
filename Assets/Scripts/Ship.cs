using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Asteroids
{
    public class Ship : MonoBehaviour
    {
        public float moveSpeed;
        public float rotateSpeed;
        public float bulletSpeed;
        public Bullet Bullet;
        
        private Gun _gun;
        
        private Vector3 MovingDirection
        {
            get => _gun.getPosition();
        }
        private Rigidbody2D _rigidbody2D;
        private 
        
        void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _gun = gameObject.AddComponent<Gun>();
        }
    
        void Update()
        {
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");
            
            if (y != 0)
            {
                _rigidbody2D.AddForce(MovingDirection * moveSpeed * y * Time.deltaTime);
            }
    
            if (x != 0)
            {
                _rigidbody2D.MoveRotation(_rigidbody2D.rotation - x * rotateSpeed * Time.deltaTime);
            }
    
            if (Input.GetButtonDown("Jump"))
            {
                Fire();
            }
        }
    
        private void Fire()
        {
            Bullet bullet = GameObject.Instantiate(
                Bullet, 
                transform.position + MovingDirection/2, 
                Quaternion.identity
            );
            
            bullet.Launching(MovingDirection, bulletSpeed);
        }

        private void OnBecameInvisible()
        {
            transform.position = -gameObject.transform.position;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Enemy"))
            {
                Destroy(gameObject);
            }
        }
    }

}
