﻿using System;
using System.Collections;
using Playmode.Application;
using Playmode.Ennemy;
using Playmode.Entity.Destruction;
using UnityEngine;
using UnityEngine.Jobs;

namespace Playmode.Entity.Status
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float maxHealth = 100;
         private float healthPoints;

        private bool invincible = false;

        public float MaxHealth => maxHealth;

        public float HealthPoints
        {
            get { return healthPoints; }
            private set
            {
                healthPoints = value < 0 ? 0 : value;

                if (healthPoints <= 0)
                {
                    GameObject.FindGameObjectWithTag("GameController").GetComponent<EnnemyDeathEventChannel>().OnDeath(transform.root);
                    GetComponent<RootDestroyer>().Destroy();
                }
                    
            }
        }

        private void Awake()
        {
            ValidateSerialisedFields();
            healthPoints = maxHealth;
        }

        private void ValidateSerialisedFields()
        {
            if (healthPoints < 0)
                throw new ArgumentException("HealthPoints can't be lower than 0.");
        }

        public void Hit(float hitPoints)
        {
            if(invincible==false)
            HealthPoints -= hitPoints;
        }

        public void Heal(float healPoints)
        {
            HealthPoints += healPoints;
        }

        public void Invincibility(int durationInSeconds)
        {
            StartCoroutine(InvincibilityRoutine(durationInSeconds));
        }

        private IEnumerator InvincibilityRoutine(int durationInSeconds)
        {
            invincible = true;
            yield return new WaitForSeconds(durationInSeconds);
            invincible = false;
        }
    }
}