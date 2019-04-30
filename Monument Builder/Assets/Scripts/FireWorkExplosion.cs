using UnityEngine;

namespace Assets.Scripts
{
    public class FireWorkExplosion : MonoBehaviour
    {
        public Color[] Colors =
        {
            Color.red,
            Color.white,
            Color.blue
        };

        private ParticleSystem.MainModule _main;
        void Start()
        {
            var particle = GetComponent<ParticleSystem>();
            _main = particle.main;
        }

        private readonly float _interval = 1f;
        private float _currentTime;
        private int _i = 0;
        void Update()
        {
            if (_currentTime >= _interval)
            {
                _currentTime = 0;

                _main.startColor = new ParticleSystem.MinMaxGradient(Colors[_i]);

                _i += _i == 2 ? -2 : 1;
            }
            _currentTime += Time.deltaTime;
        }
    }
}
