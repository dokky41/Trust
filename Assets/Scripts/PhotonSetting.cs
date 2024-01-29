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
        // �ڵ����� ����ȭ�� �������� �ʰڴ�. (īƮ���̴��� ���� true�� ������?)
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
        // RegisterPlayFabUserRequest ������ ������ ����ϱ� ���� Ŭ����
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
