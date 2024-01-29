using Photon.Pun;
using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotonSetting : MonoBehaviour
{
    [SerializeField]
    InputField email;
    [SerializeField]
    InputField userID;
    [SerializeField]
    InputField password;

    public void LoginSuccess(LoginResult result)
    {
        // 자동으로 동기화를 시켜주지 않겠다. (카트라이더의 경우는 true가 좋을듯?)
        PhotonNetwork.AutomaticallySyncScene = false;

        PhotonNetwork.GameVersion = "1.0f";

        PhotonNetwork.NickName = PlayerPrefs.GetString("Name");

        PhotonNetwork.LoadLevel("PhotonLobby");

    }

    public void Login()
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = email.text,
            Password = password.text,
        };

        PlayFabClientAPI.LoginWithEmailAddress
        (
            request,
            LoginSuccess,
            (PlayFabError error)=> NotificationManager.NotificationWindow("Check your ID or PassWord")
        );

    }

    public void SignUp()
    {
        // RegisterPlayFabUserRequest 서버에 유저를 등록하기 위한 클래스
        var request = new RegisterPlayFabUserRequest
        {
            Email = email.text,
            Password = password.text,
            Username = userID.text,
        };

        PlayerPrefs.SetString("Name",userID.text);

        PlayFabClientAPI.RegisterPlayFabUser
        (
            request,
            (RegisterPlayFabUserResult result) => NotificationManager.NotificationWindow(result.ToString()),
            (PlayFabError error) => NotificationManager.NotificationWindow("Please register a password between 6 " +
            "and 12 characters, " +
            "and a username between 3 and 20 characters. Your provided information is not valid.")
        );


    }




}
