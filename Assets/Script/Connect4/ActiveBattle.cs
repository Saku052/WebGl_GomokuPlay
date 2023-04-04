using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveBattle : MonoBehaviour
{   
    [SerializeField]
    private Button BattleButton;
    [SerializeField]
    private GameObject mapPanel;


    public static int SelectedX;
    public static int SelectedY;
    public static BattleResult battleResult = BattleResult.None;

    //赤色にするべきか判断
    private bool isStart = false;



    //x, yを保存してテキストの色を変更する
    public void SetSelected(int x, int y)
    {
        //ボタンを既に選択していた場合、色を戻す
        if((SelectedX != x || SelectedY != y) && isStart){
        mapPanel.transform.GetChild(Map.GetButtonNumber(SelectedX, SelectedY)).GetComponent<Image>().color =
        new Color(1, 1, 1, 0.33f); //白色に戻す
        }
        
        //ボタンを選択した時の処理
        isStart = true;
        mapPanel.transform.GetChild(Map.GetButtonNumber(x, y)).GetComponent<Image>().color = Color.red; //赤色に変える
        SelectedX = x;
        SelectedY = y;
        BattleButton.interactable = true;
    }

    //RPGの戦闘シーンに移動する
    public void MoveRPG()
    {
        //RPGのシーンに移動する
        Map.gameResult = Map.GameResult.RpgGame;
        UnityEngine.SceneManagement.SceneManager.LoadScene("RPG");
    }

    //勝利した場合の処理
    public void Win()
    {
        BattleButton.interactable = false;
        mapPanel.transform.GetChild(Map.GetButtonNumber(SelectedX, SelectedY)).GetComponent<Image>().color =
        new Color(0.26f, 0.80f, 0.90f, 0.80f); //青色に変える
        Map.ChangeToA(SelectedX, SelectedY);
        
        //相手のターンになる。
        ConnectEnemy.instance.AIEnemyTurn(Map.GameMap);
    }

    //バトルから戻ってきた場合の処理
    private void Start()
    { 
       if(ActiveBattle.battleResult == BattleResult.Win){
            //勝利した場合の処理
            Map.gameResult = Map.GameResult.Connect;
            Win();
       }else if(ActiveBattle.battleResult == BattleResult.Lose){
            //敗北した場合の処理
            Map.gameResult = Map.GameResult.Connect;
            ConnectEnemy.instance.AIEnemyTurn(Map.GameMap);
       }
    }

    //インスタンス化
    public static ActiveBattle Instance;
    private void Awake() {
        if(Instance == null){
            Instance = this;
        }
    }
}

//勝敗の確認
public enum BattleResult
{
    Win,
    Lose,
    None
}
