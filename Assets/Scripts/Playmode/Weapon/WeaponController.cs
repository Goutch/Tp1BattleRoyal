﻿using System;
using Playmode.Bullet;
using Playmode.Ennemy;
using UnityEngine;
using UnityEngine.UI;

namespace Playmode.Weapon
{
    public class WeaponController : MonoBehaviour
    {
        [Header("Behaviour")] [SerializeField] protected GameObject bulletPrefab;
        [SerializeField] protected float fireDelayInSeconds = 1f;
        [SerializeField] private float knockBackForce = 1;
        [SerializeField]private float damageModifier=1;
        public float KnockBackForce
        {
            get { return knockBackForce; }
            set { knockBackForce = value; }
        }

        

        public float DamageModifier
        {
            get { return damageModifier; }
            set { damageModifier = value; }
        }

        public virtual int NbBullet
        {
            get { return 1; }
            set { }
        }

        public enum WeaponType
        {
            Base,
            Shotgun,
            Uzi,
            Sniper
        }

        [SerializeField] private WeaponType type;
        
        public WeaponType Type => type;
        

        public float FireDelayInSeconds
        {
            get { return fireDelayInSeconds; }
            set { fireDelayInSeconds = value; }
        }

        protected float lastTimeShotInSeconds;

        protected bool CanShoot => Time.time - lastTimeShotInSeconds > fireDelayInSeconds;

        private void Awake()
        {
            ValidateSerialisedFields();
            InitializeComponent();
        }

        private void ValidateSerialisedFields()
        {
            if (fireDelayInSeconds < 0)
                throw new ArgumentException("FireRate can't be lower than 0.");
        }

        private void InitializeComponent()
        {
            lastTimeShotInSeconds = 0;
        }

        public virtual void Shoot()
        {
            if (CanShoot)
            {

                GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
                bullet.GetComponentInChildren<BulletController>().Damage*=DamageModifier;
                bullet.GetComponentInChildren<BulletController>().Source=transform.root;
                KnockBackRoot();

                lastTimeShotInSeconds = Time.time;
            }
        }

        public virtual void KnockBackRoot()
        {
            float radianAngle = (transform.rotation.eulerAngles.z+90) * (3.1416f / 180);
            Vector2 dir=new Vector2(-Mathf.Cos(radianAngle),-Mathf.Sin(radianAngle));
            this.transform.root.GetComponent<Rigidbody2D>().AddForce(dir*knockBackForce,ForceMode2D.Impulse);
        }
    }
}