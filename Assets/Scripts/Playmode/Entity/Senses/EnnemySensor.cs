﻿using System.Collections.Generic;
using Playmode.Ennemy;
using UnityEngine;

namespace Playmode.Entity.Senses
{
    public delegate void EnnemySensorEventHandler(EnnemyController ennemy);

    public class EnnemySensor : MonoBehaviour
    {
        private List<EnnemyController> ennemiesInSight;

        public event EnnemySensorEventHandler OnEnnemySeen;
        public event EnnemySensorEventHandler OnEnnemySightLost;


        public List<EnnemyController> EnnemiesInSight
        {
            get
            {
                ennemiesInSight.RemoveAll(it => it == null);
                return ennemiesInSight;
            }
        }


        private void Awake()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            ennemiesInSight = new List<EnnemyController>();
        }

        public void See(EnnemyController ennemy)
        {
            ennemiesInSight.Add(ennemy);

            NotifyEnnemySeen(ennemy);
        }


        public void LooseSightOf(EnnemyController ennemy)
        {
            ennemiesInSight.Remove(ennemy);

            NotifyEnnemySightLost(ennemy);
        }

        private void NotifyEnnemySeen(EnnemyController ennemy)
        {
            if (OnEnnemySeen != null) OnEnnemySeen(ennemy);
        }

        private void NotifyEnnemySightLost(EnnemyController ennemy)
        {
            if (OnEnnemySightLost != null) OnEnnemySightLost(ennemy);
        }
    }
}