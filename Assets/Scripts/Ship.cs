using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Serialization;

namespace Asteroids
{
    public class Ship : MonoBehaviour
    {
        public Bullet bullet;
        
        private Gun _gun;
        private float _moveSpeed;
        private float _rotateSpeed;
        private float _bulletSpeed;
        
        private Vector3 MovingDirection
        {
            get => _gun.getPosition();
        }
        private Rigidbody2D _rigidbody2D;
        
        void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _gun = gameObject.AddComponent<Gun>();
            _rigidbody2D.mass = 2;
            _rigidbody2D.drag = 1;
            _rigidbody2D.angularDrag = 0.5f;
            _moveSpeed = 200;
            _rotateSpeed = 200;
            _bulletSpeed = 10;
        }
    
        void Update()
        {
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");
            
            if (y != 0)
            {
                _rigidbody2D.AddForce(MovingDirection * _moveSpeed * y * Time.deltaTime);
            }
    
            if (x != 0)
            {
                _rigidbody2D.MoveRotation(_rigidbody2D.rotation - x * _rotateSpeed * Time.deltaTime);
            }
    
            if (Input.GetButtonDown("Jump"))
            {
                Fire();
            }
        }
    
        private void Fire()
        {
            Bullet bullet = GameObject.Instantiate(
                this.bullet, 
                transform.position + MovingDirection/2, 
                Quaternion.identity
            );
            
            bullet.Launching(MovingDirection, _bulletSpeed);
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
                GameController.instance.onGameOver();
            }
        }
    }

}
