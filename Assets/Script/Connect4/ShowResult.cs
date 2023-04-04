using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShowResult : MonoBehaviour
{
    [SerializeField] private GameObject ResultText;

    void Start()
    {
        ResultText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Map.gameResult == Map.GameResult.AWin){
                ResultText.SetActive(true);
                ResultText.GetComponentInChildren<Text>().text = "勝利";
                Map.gameResult = Map.GameResult.End;

        }else if(Map.gameResult == Map.GameResult.BWin){
                ResultText.SetActive(true);
                ResultText.GetComponentInChildren<Text>().text = "敗北";
                Map.gameResult = Map.GameResult.End;

        }else if(Map.gameResult == Map.GameResult.Draw){
                ResultText.SetActive(true);
                ResultText.GetComponentInChildren<Text>().text = "引き分け";
                Map.gameResult = Map.GameResult.End;
        }
    }

    //シーンをリロードする
    public void ReloadScene()
    {
        Map.gameResult = Map.GameResult.Connect;
        Map.GameMap = new Map.map[5, 5];
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
