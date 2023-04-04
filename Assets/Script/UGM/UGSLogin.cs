using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.CloudSave;



public class UGSLogin : MonoBehaviour
{
    public struct userAttributes {}
    public struct appAttributes {}


    async void Awake() {
        
        await UnityServices.InitializeAsync();

        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }

}


