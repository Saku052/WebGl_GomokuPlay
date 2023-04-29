using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.RemoteConfig;
using Newtonsoft.Json;

public class GetStatus : MonoBehaviour
{

    // プレイヤーのステータス
    public static Status MainPlayer;
    public static Status Enemy;
    public static Status EnemyRank1;
    public static Status EnemyRank2;
    public static Status EnemyRank3;

    // RemoteConfigの設定
    public struct userAttributes {}
    public struct appAttributes {}
    
    void Start()
    {
        
        //メインプレイヤーが定義されて無かったら取得する
        if(MainPlayer == null)
        {
            RemoteConfigService.Instance.FetchCompleted += ApplyRemoteSettings;
            RemoteConfigService.Instance.FetchConfigs(new userAttributes(), new appAttributes());
        }  
    }


    void ApplyRemoteSettings(ConfigResponse configResponse)
    {
        Debug.Log(RemoteConfigService.Instance.appConfig.GetJson("Forest1").ToString() + "end");

        var DemonLord = JsonConvert.DeserializeObject<List<Root>>(RemoteConfigService.Instance.appConfig.GetJson("MainInit").ToString());
        var Forest1 = JsonConvert.DeserializeObject<List<Root>>(RemoteConfigService.Instance.appConfig.GetJson("Forest1").ToString());

        // Name, MaxLevel, MaxExp, MaxHp, MaxAtk, MaxDef, MaxSpeed, CritRate, CritDamage, CharCode

        MainPlayer = new Status(DemonLord[0].Name,
        DemonLord[0].MaxLevel,
        DemonLord[0].MaxExp,
        DemonLord[0].MaxHp,
        DemonLord[0].MaxAtk,
        DemonLord[0].MaxDef,
        DemonLord[0].MaxSpeed,
        DemonLord[0].CritRate,
        DemonLord[0].CritDamage,
        DemonLord[0].CharCode);

        //敵のステータスを取得する
        EnemyRank1 = new Status(Forest1[0].Name,
        Forest1[0].MaxLevel,
        Forest1[0].MaxExp,
        Forest1[0].MaxHp,
        Forest1[0].MaxAtk,
        Forest1[0].MaxDef,
        Forest1[0].MaxSpeed,
        Forest1[0].CritRate,
        Forest1[0].CritDamage,
        Forest1[0].CharCode);

        EnemyRank2 = new Status(Forest1[1].Name,
        Forest1[1].MaxLevel,
        Forest1[1].MaxExp,
        Forest1[1].MaxHp,
        Forest1[1].MaxAtk,
        Forest1[1].MaxDef,
        Forest1[1].MaxSpeed,
        Forest1[1].CritRate,
        Forest1[1].CritDamage,
        Forest1[1].CharCode);

        EnemyRank3 = new Status(Forest1[2].Name,
        Forest1[2].MaxLevel,
        Forest1[2].MaxExp,
        Forest1[2].MaxHp,
        Forest1[2].MaxAtk,
        Forest1[2].MaxDef,
        Forest1[2].MaxSpeed,
        Forest1[2].CritRate,
        Forest1[2].CritDamage,
        Forest1[2].CharCode);

    }

    //インスタンス化
    public static GetStatus instance;
    private void Awake() {
        if(instance == null){
            instance = this;
        }
    }
}

// Root myDeserializedClass = JsonConvert.DeserializeObject<List<Root>>(myJsonResponse);
// Name, MaxLevel, MaxExp, MaxHp, MaxAtk, MaxDef, MaxSpeed, CritRate, CritDamage, CharCode
public class Root
{
    public string Name { get; set; }
    public int MaxLevel { get; set; }
    public int MaxExp { get; set; }
    public int MaxHp { get; set; }
    public int MaxAtk { get; set; }
    public int MaxDef { get; set; }
    public int MaxSpeed { get; set; }
    public float CritRate { get; set; }
    public float CritDamage { get; set; }
    public int CharCode { get; set; }
}

