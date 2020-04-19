using System;
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
        public GameObject spriteGO;
        public AudioSource explosionAudioSource;
        public ParticleSystem breakParticleSystem;
        public TrailRenderer trailRenderer;
#pragma warning restore CS0649

        public bool Broken { get; private set; }

        private Vector2 _velocity = Vector2.zero;

        private float _particleTime;

        public void Start()
        {
            //Set random ratation.
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f));
        }

        public void Update()
        {
            if(GameManager.Instance.GameRunning)
            {
                if (!Broken)
                {
                    blockContainer.localPosition += (Vector3)(_velocity * Time.deltaTime);
                    _velocity.y += -0.1f * Time.deltaTime;
                }
                else
                {
                    _particleTime -= Time.deltaTime;
                    
                    if(_particleTime < -10.0f)
                    {
                        Destroy(gameObject);
                    }
                    if(breakParticleSystem != null && _particleTime < 0)
                    {
                        ParticleSystem.EmissionModule e = breakParticleSystem.emission;
                        e.enabled = false;
                        breakParticleSystem = null;
                    }
                }
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Shield shield = collision.gameObject.GetComponent<Shield>();

            if(shield != null)
            {
                Break();
            }

            Planet planet = collision.gameObject.GetComponent<Planet>();

            if(planet != null)
            {
                GameManager.Instance.Lose(planet);
            }
        }

        void Break()
        {
            breakParticleSystem.gameObject.SetActive(true);
            _particleTime = 0.1f;
            explosionAudioSource.Play();
            spriteGO.SetActive(false);
            trailRenderer.emitting = false;
            Broken = true;
        }
    }
}
