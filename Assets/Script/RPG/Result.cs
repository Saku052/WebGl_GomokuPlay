using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Result : MonoBehaviour
{
    
   
    public void Win()
    {
        ActiveBattle.battleResult = BattleResult.Win;

        //勝利した場合expを与える
        addExp();
        UnityEngine.SceneManagement.SceneManager.LoadScene("Connect4");
    }


    public void Lose()
    {
        ActiveBattle.battleResult = BattleResult.Lose;
        
        //敗北した場合レベルを下げる
        if(GetStatus.MainPlayer.Level > 1)  GetStatus.MainPlayer.SubLevel(1);
        else GetStatus.MainPlayer.SubLevel(0);

        UnityEngine.SceneManagement.SceneManager.LoadScene("Connect4");
    }

    //勝利した場合expを与える
    private void addExp()
    {
        if(GetStatus.Enemy.Name == GetStatus.EnemyRank1.Name)   GetStatus.MainPlayer.AddExp(7);
        else if(GetStatus.Enemy.Name == GetStatus.EnemyRank2.Name)   GetStatus.MainPlayer.AddExp(10);
        else if(GetStatus.Enemy.Name == GetStatus.EnemyRank3.Name)   GetStatus.MainPlayer.AddExp(15);
    }

    //敗北した場合

    //インスタンス化
    public static Result instance;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(this.gameObject);
        }
    }
    
}
