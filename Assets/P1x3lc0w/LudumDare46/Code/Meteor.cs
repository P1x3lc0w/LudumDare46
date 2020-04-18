﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace P1x3lc0w.LudumDare46
{
    class Meteor : MonoBehaviour
    {
#pragma warning disable CS0649
        public Transform blockContainer;
#pragma warning restore CS0649

        private Vector2 _velocity = Vector2.zero;

        public void Start()
        {
            //Set random ratation.
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f));
        }

        public void Update()
        {
            blockContainer.localPosition += (Vector3)(_velocity * Time.deltaTime);
            _velocity.y += -0.1f * Time.deltaTime;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Shield shield = collision.gameObject.GetComponent<Shield>();

            if(shield != null)
            {
                Destroy(gameObject);
            }

            Planet planet = collision.gameObject.GetComponent<Planet>();

            if(planet != null)
            {
                Debug.Log("GAME OVER");
            }
        }
    }
}
