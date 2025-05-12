using System.Runtime.CompilerServices;
using UnityEngine;

namespace Managers
{
    public class EnvironmentManager : MonoBehaviour
    {
        public static EnvironmentManager Instance;

        //DVD Screensaver
        public GameObject DVDBox;
        private Rigidbody2D RbDVDBox;

        //Pulsations
        private float currentRatio = 1f;
        private float maxScale = 1.05f;
        private float minScale = 0.95f;
        private float pulsationSpeed = 0.5f;
        private bool growing = true;

        //Flipping
        private float flippingSpeed = 270f; //180f oznacza 1s, 360f oznacza 0.5s, 90f oznacza 2s, itd.
        private bool isFront = true;
        private bool isFlipping = false;

        //Rotating
        [SerializeField]
        private float rotationSpeed;
        private float rotationTime = 4f;

        private float timer = 0f;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(Instance);
        }

        public void Initialize()
        {
            RbDVDBox = DVDBox.GetComponent<Rigidbody2D>();
            //MoveDVDBox();
        }

        public void CustomUpdate()
        {
            PulsateGameBox();
            timer += Time.deltaTime;
            // Automatyczne spawnowanie co X sekund
            if (timer >= rotationTime)
            {
                rotationSpeed *= -1f;
                rotationTime = Random.Range(1f, 10f);
                timer = 0f;
            }
            RotateGameBox();
            if (Input.GetKeyDown(KeyCode.Space))
                FlipGameBox();
        }

        private void PulsateGameBox()
        {
            currentRatio = Mathf.MoveTowards(currentRatio, growing ? maxScale : minScale, pulsationSpeed * Time.deltaTime);
            if (currentRatio >= maxScale || currentRatio <= minScale)
                growing = !growing;
            GameManager.Instance.GameBox.transform.localScale = Vector3.one * currentRatio;
        }

        private void MoveDVDBox()
        {
            RbDVDBox.linearVelocity = new Vector2(1f, 1f);
        }

        private async Awaitable FlipGB()
        {
            isFlipping = true;
            Transform gbTransform = GameManager.Instance.GameBox.transform;
            float targetYRotation = isFront ? 180f : 0f;
            int beginRotation = 50;
            int i = 0;
            Vector3 currentAngles = new Vector3(0f, isFront ? 0f : 180f, 0f);
            while (isFlipping)
            {
                i++;
                if (isFront)
                    currentAngles += new Vector3(0f, flippingSpeed, 0f) * Time.deltaTime;
                else
                    currentAngles -= new Vector3(0f, flippingSpeed, 0f) * Time.deltaTime;
                gbTransform.eulerAngles = currentAngles;
                if (i > beginRotation && (gbTransform.eulerAngles.y < 1f || gbTransform.eulerAngles.y > 179f))
                {
                    gbTransform.eulerAngles = new Vector3(0f, targetYRotation, 0f);
                    isFront = !isFront;
                    isFlipping = false;
                }
                await Awaitable.NextFrameAsync();
            }
        }

        private void RotateGameBox()
        {
            Transform gbTransform = GameManager.Instance.GameBox.transform;
            float addRotation = rotationSpeed * Random.Range(0.5f, 1.5f);
            gbTransform.Rotate(0f, 0f, -addRotation * Time.deltaTime);
        }

        private async void FlipGameBox()
        {
            if (!isFlipping)
                await FlipGB();
        }
    }
}