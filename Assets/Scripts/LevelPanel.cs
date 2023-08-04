using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPanel : MonoBehaviour
{
    [SerializeField] GameObject[] scoreIcons;
    [SerializeField] private int myLevel;
    [SerializeField] GameObject button, lockImage;

    [SerializeField] bool resetPlayerSave;

    // Start is called before the first frame update
    void Start()
    {
        if (resetPlayerSave) PlayerSave.ResetPlayerSave();
        int levelScore = PlayerSave.GetSavedLevel(myLevel);
        if(levelScore >= 0 || myLevel == 1 || PlayerSave.GetSavedLevel(myLevel-1) >= 0)
        {
            lockImage.SetActive(false);
            button.SetActive(true);

        }
        else
        {
            button.SetActive(false);
        }
        Display(levelScore);
    }

    
    public void Display(int score)
    {
        
        for (int i = 0; i < scoreIcons.Length; i++)
        {
            scoreIcons[i].SetActive(score > i);
        }
        gameObject.SetActive(true);
    }

    public void ConfirmButtonClick()
    {
        LevelHandler.level = myLevel;
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}
