using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class GameManager : MonoBehaviour
    {
        public Text AgeText;

        public int CurrentYear;
        public int CurrentMonth;


        public double CurrentAge;
        public int Prestige; //TODO Remove?

        private int _maxAge;

        public void Start()
        {
            //TODO Change this to the actual level years
            CurrentYear = 1600;
            CurrentMonth = 1;
            CurrentAge = 20;

            _maxAge = 64 + Random.Range(1, 17);
        }

        public void Update()
        {
            AgeText.text = Mathf.FloorToInt((float)CurrentAge).ToString();

            //TODO Change to end game Screen
            if (Mathf.FloorToInt((float)CurrentAge) == _maxAge)
                Debug.Log("YOU ARE DEAD!!!");
        }

        public void AddTime(int i)
        {
            CurrentAge += (double)i / 12;
            CurrentMonth += i;

            if (CurrentMonth > 12)
            {
                CurrentYear++;
                CurrentMonth = 1;
            }
        }
    }
}
