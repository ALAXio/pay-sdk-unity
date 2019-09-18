using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSpriteManager : MonoBehaviour
{
    public Sprite[] buttonState; //0 = normal, 1 = processing, 2 = purchased

    public static ButtonSpriteManager lastTappedButton;

    Image buttonImage;

    void Start()
    {
        buttonImage = GetComponent<Image>();
    }

    public void ButtonTap() {
        lastTappedButton = this;
    }

    public void ChangeButtonState(int state) { //0 = normal, 1 = processing, 2 = purchased
        buttonImage.sprite = buttonState[state];
    }

    public void TestPurchase() {
        ButtonSpriteManager.lastTappedButton.ChangeButtonState(2);
    }
}
