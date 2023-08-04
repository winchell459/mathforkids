using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pickup : MonoBehaviour
{
    [SerializeField] Text myText;

    public void SetupPickupText(string text)
    {
        myText.text = text;
    }
}
