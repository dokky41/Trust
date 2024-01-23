using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ConnectServer : MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_Dropdown server;

    private void Awake()
    {
        server.options[0].text = "Union";
        server.options[1].text = "Aether";
        server.options[2].text = "Haselo";
    }

    public void SelectServer()
    {
        // ���� ����
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnJoinedLobby()
    {
        // �Ϲ� LoadLevel�� �� ����ȭ�� ���� �ʽ��ϴ�.
        PhotonNetwork.LoadLevel("PhotonRoom");
    }

    public override void OnConnectedToMaster()
    {
        // JoinLobby : Ư�� �κ� �����Ͽ� �����ϴ� ���
        PhotonNetwork.JoinLobby
        (
            new TypedLobby
            (
               server.options[server.value].text,
               LobbyType.Default
            )
        );


    }



}
