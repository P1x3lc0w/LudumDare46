using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace P1x3lc0w.LudumDare46
{
    class Shield : MonoBehaviour
    {
        const float MOVEMENT_SPEED = -2.0f;

#pragma warning disable CS0649
        public Transform blockContainer;
        public SpriteRenderer sprite;
#pragma warning restore CS0649

        public void SetSize(float height)
            => SetSize(height, height * 5.0f);

        public void SetSize(float height, float width)
        {
            blockContainer.transform.position = new Vector3(0.0f, height, 0.0f);
            blockContainer.transform.localScale = new Vector3(width, 1.0f, 1.0f);
        }

        public void SetColor()
        {
            SetColor(Color.HSVToRGB(Random.Range(0.0f, 1.0f), 1.0f, 1.0f));
        }

        public void SetColor(Color color)
        {
            sprite.color = color;
        }

        public void Move(float amount)
        {
            float rotationAmount = amount * MOVEMENT_SPEED;
            transform.Rotate(0.0f, 0.0f, rotationAmount, Space.Self);
        }
    }
}
