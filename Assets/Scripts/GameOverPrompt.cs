using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverPrompt : MonoBehaviour
{
    [SerializeField] GameObject[] scoreIcons;
    public void Display(int score)
    {
        for(int i = 0; i < scoreIcons.Length; i++)
        {
            scoreIcons[i].SetActive(score > i);
        }
        gameObject.SetActive(true);
    }

    public void ConfirmButtonClick()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
