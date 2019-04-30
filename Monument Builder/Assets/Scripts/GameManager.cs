using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Assets.Scripts
{
    public class GameManager : MonoBehaviour
    {
        public Text AgeText;
        public GameObject NewspaperPrefab;

        public int CurrentYear;
        public int CurrentMonth;
        
        public double CurrentAge;

        private int _maxAge;
        public bool IsGameEnded;

        public void Start()
        {
            //TODO Change this to the actual level years
            CurrentYear = 1611;
            CurrentMonth = 1;
            CurrentAge = 35;

            _maxAge = 64 + Random.Range(1, 17);
        }

        public void Update()
        {
            AgeText.text = Mathf.FloorToInt((float)CurrentAge).ToString();

            if (Mathf.FloorToInt((float)CurrentAge) >= _maxAge)
                CreateNewspaper("Architect found dead", "Game over!", "Back to menu", BackToMenu);
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

        public void CreateNewspaper(string title, string description, string buttonText, UnityAction buttonFunction)
        {
            var newspaper = Instantiate(NewspaperPrefab);
            newspaper.transform.SetParent(GameObject.Find("Canvas").transform);
            newspaper.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);

            newspaper.transform.GetChild(1).GetComponent<Text>().text = title;
            newspaper.transform.GetChild(2).GetComponent<Text>().text = description;

            var button = newspaper.transform.GetChild(3).GetComponent<Button>();
            button.transform.GetChild(0).GetComponent<Text>().text = buttonText;
            button.onClick.AddListener(buttonFunction);
            button.onClick.AddListener(() => { Destroy(newspaper); });
        }

        public void BackToMenu()
        {
            SceneManager.LoadScene("MenuScene");
        }
    }
}
