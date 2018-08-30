﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Playmode.Application;
using UnityEngine;
using Playmode.Ennemy.BodyParts;
using Playmode.Entity.Senses;
using Playmode.Movement;
using Playmode.Pickable;
using Random = UnityEngine.Random;


namespace Playmode.Ennemy.Strategies
{
    public class CowboyStrategy :NormalStrategy, IEnnemyStrategy
    {
        private Vector3 target;
        private PickableWeaponSensor weaponSensor;

        public CowboyStrategy(Mover mover, HandController handcontroller, GameObject sight) : base(mover,handcontroller,sight)
        {
            weaponSensor = sight.GetComponent<PickableWeaponSensor>();
        }

        public void Act()
        {
            if (ennemySensor.EnnemiesInSight.Count() == 0 && treath != null)
            {
                Defend();
            }
            else
            {
                FindSomethingToDo();
            }
        }

        protected override void FindSomethingToDo()
        {
            //Priorise la recherche d'arme.
            //Si aucune arme , chercher un ennemy.
            if (weaponSensor.WeaponsInSight.Any())
            {
                Debug.Log("I've found a weapon!! Ya'll motherfuckers dead!!!");
                Vector3 direction = weaponSensor.WeaponsInSight.First().transform.position -
                                    mover.transform.position;
                mover.Rotate(Vector2.Dot(direction, mover.transform.right));
                mover.MoveToward(weaponSensor.WeaponsInSight.First().transform.position);
            }
           else if (ennemySensor.EnnemiesInSight.Count() != 0)
           {
               Attack();
           }
           else if (Vector3.Distance(mover.transform.position, randomDestination) <= 0.5)
           {
               FindNewRandomDestination();
           }
           else
           {
               Roam();
           }
        }

    }
}