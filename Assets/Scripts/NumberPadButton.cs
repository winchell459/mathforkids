using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberPadButton : MonoBehaviour
{
    int num;
    [SerializeField] Text text;
    [SerializeField] Image image;
    [SerializeField] Sprite[] spriteNumbers;

    public void SetupButton(int num)
    {
        text.text = num.ToString();
        this.num = num;
        SetImage(spriteNumbers[num-1]);
    }

    private void SetImage(Sprite spriteNum)
    {
        image.sprite = spriteNum;
        image.rectTransform.sizeDelta = spriteNum.rect.size/5;
    }

    public void ButtonClicked()
    {
        FindObjectOfType<NumberPad>().ButtonPressed(num);
        GetComponent<Button>().interactable = false;
    }

}
