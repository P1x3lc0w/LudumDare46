using UnityEngine;
using Random = UnityEngine.Random;

namespace P1x3lc0w.LudumDare46
{
    internal class Shield : MonoBehaviour
    {
        private const float MOVEMENT_SPEED = -12.0f;

#pragma warning disable CS0649
        public Transform blockContainer;
        public SpriteRenderer sprite;
#pragma warning restore CS0649

        public float Height => blockContainer.transform.localPosition.y;
        public Color ShieldColor => sprite.color;

        public void SetSize(float height)
            => SetSize(height, height * 0.5f);

        public void SetSize(float height, float width)
        {
            blockContainer.transform.localPosition = new Vector3(0.0f, height, 0.0f);
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
            float rotationAmount = amount * MOVEMENT_SPEED * (1 / (Height * 4));
            transform.Rotate(0.0f, 0.0f, rotationAmount, Space.Self);
        }
    }
}