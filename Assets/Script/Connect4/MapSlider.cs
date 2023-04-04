using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapSlider : MonoBehaviour
{
    //スライダー
    [SerializeField] private Slider player1AtkSlider;
    [SerializeField] private Slider player1HpSlider; 
    [SerializeField] private Slider player1ExpSlider;

    [SerializeField] private Slider player2AtkSlider;
    [SerializeField] private Slider player2HpSlider;
    [SerializeField] private Slider player2ExpSlider;

    //テキスト
    [SerializeField] private Text player1AtkText;
    [SerializeField] private Text player1HpText;
    [SerializeField] private Text player1LevText;
    [SerializeField] private Text player2AtkText;
    [SerializeField] private Text player2HpText;
    [SerializeField] private Text player2LevText;

    //名前のテキスト
    [SerializeField] private Text player1NameText;
    [SerializeField] private Text player2NameText;

    //キャラの画像
    [SerializeField] private Image player1Image;
    [SerializeField] private Image player2Image;

    // スライダーを動かす
    public void SetPlayer1AtkSlider(float value)
    {
        player1AtkSlider.value = value;
    }
    public void SetPlayer1HpSlider(float value)
    {
        player1HpSlider.value = value;
    }
    public void SetPlayer1ExpSlider(float value)
    {
        player1ExpSlider.value = value;
    }
    public void SetPlayer2AtkSlider(float value)
    {
        player2AtkSlider.value = value;
    }
    public void SetPlayer2HpSlider(float value)
    {
        player2HpSlider.value = value;
    }
    public void SetPlayer2ExpSlider(float value)
    {
        player2ExpSlider.value = value;
    }

    //テキストを変更する
    public void SetPlayer1AtkText(int val, int max)
    {
        player1AtkText.text = "Atk " + val + "/" + max;
    }
    public void SetPlayer1HpText(int val, int max)
    {
        player1HpText.text = "Hp " + val + "/" + max;
    }
    public void SetPlayer1LevText(int val)
    {
        player1LevText.text = "Lv " + val;
    }
    public void SetPlayer2AtkText(int val, int max)
    {
        player2AtkText.text = "Atk " + val + "/" + max;
    }
    public void SetPlayer2HpText(int val, int max)
    {
        player2HpText.text = "Hp " + val + "/" + max;
    }
    public void SetPlayer2LevText(int val)
    {
        player2LevText.text = "Lv " + val;
    }

    //名前を変更する
    public void SetPlayer1NameText(string name)
    {
        player1NameText.text = name;
    }
    public void SetPlayer2NameText(string name)
    {
        player2NameText.text = name;
    }

    //画像を変更する
    public void SetPlayer1Image(Sprite sprite)
    {
        player1Image.sprite = sprite;
    }
    public void SetPlayer2Image(Sprite sprite)
    {
        player2Image.sprite = sprite;
    }




    //インスタンス化
    public static MapSlider instance;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(this.gameObject);
        }
    }
}
