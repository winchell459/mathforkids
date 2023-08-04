using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NumberPad : MonoBehaviour
{
    public LevelHandler levelHandler;
    public NumberPadButton buttonPrefab;

    [SerializeField] float spacing = 50;
    [SerializeField] float boarder = 5;

    public int moveCount;

    public void ButtonPressed(int num)
    {
        moveCount--;
        levelHandler.NumberPadInput(num, moveCount);
        
    }

    public void SetupNumPad(List<int> numbers)
    {
        moveCount = numbers.Count;
        for(int i = 0; i < numbers.Count; i++)
        {
            int x = i % 3;
            int y = i / 3;
            CreateButton(x, y, numbers[i]);
        }
    }

    private void CreateButton(int x, int y, int num)
    {
        NumberPadButton button = Instantiate(buttonPrefab,transform);
        button.transform.localPosition = new Vector2(boarder + spacing * x, -boarder - spacing * y);
        button.SetupButton(num);
    }
}
