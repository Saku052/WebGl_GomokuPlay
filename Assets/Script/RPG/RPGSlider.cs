using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RPGSlider : MonoBehaviour
{
    //スライダー
    [SerializeField] private Slider player1AtkSlider;
    [SerializeField] private Slider player1HpSlider;
    [SerializeField] private Slider player2AtkSlider;
    [SerializeField] private Slider player2HpSlider;

    //テキスト
    [SerializeField] private Text player1AtkText;
    [SerializeField] private Text player1HpText;
    [SerializeField] private Text player2AtkText;
    [SerializeField] private Text player2HpText;

    //名前
    [SerializeField] private Text player1Name;
    [SerializeField] private Text player2Name;

    //イラスト
    [SerializeField] private Image player1Image;
    [SerializeField] private Image player2Image;

    //順番を変更する
    [SerializeField] private GameObject Turn;

    // スライダーを動かす
    public void SetPlayer1AtkSlider(float value)
    {
        player1AtkSlider.value = value;
    }
    public void SetPlayer1HpSlider(float value)
    {
        player1HpSlider.value = value;
    }
    public void SetPlayer2AtkSlider(float value)
    {
        player2AtkSlider.value = value;
    }
    public void SetPlayer2HpSlider(float value)
    {
        player2HpSlider.value = value;
    }

    // テキストを変更する
    public void SetPlayer1AtkText(int val, int max)
    {
        player1AtkText.text = "Atk " + val + "/" + max;
    }
    public void SetPlayer1HpText(int val, int max)
    {
        player1HpText.text = "Hp " + val + "/" + max;
    }
    public void SetPlayer2AtkText(int val, int max)
    {
        player2AtkText.text = "Atk " + val + "/" + max;
    }
    public void SetPlayer2HpText(int val, int max)
    {
        player2HpText.text = "Hp " + val + "/" + max;
    }

    // 名前を変更する
    public void SetPlayer1Name(string name)
    {
        player1Name.text = name;
    }
    public void SetPlayer2Name(string name)
    {
        player2Name.text = name;
    }

    // イラストを変更する
    public void SetPlayer1Image(Sprite sprite)
    {
        player1Image.sprite = sprite;
    }
    public void SetPlayer2Image(Sprite sprite)
    {
        player2Image.sprite = sprite;
    }

    // 順番を変更する
    public void SetTurn(string name, int num)
    {
        Turn.transform.GetChild(num).GetComponentInChildren<Text>().text = name;
    }
    public string GetTurn(int num)
    {
        return Turn.transform.GetChild(num).GetComponentInChildren<Text>().text;
    }
    

    //インスタンス化
    public static RPGSlider instance;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(this.gameObject);
        }
    }
}
