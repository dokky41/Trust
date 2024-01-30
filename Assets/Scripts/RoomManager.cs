using JetBrains.Annotations;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public Button RoomCreate;
    public InputField RoomName;
    public InputField RoomPerson;
    public Transform RoomContent;

    // �� ����� �����ϱ� ���� �ڷᱸ��
    Dictionary<string,RoomInfo> RoomCatalog = new Dictionary<string,RoomInfo>();


    // Update is called once per frame
    void Update()
    {
        // �뿡 �̸��� �ο��� �Է����������� ��ư ��Ȱ��ȭ
        if (RoomName.text.Length > 0 && RoomPerson.text.Length > 0)
            RoomCreate.interactable = true;
        else
            RoomCreate.interactable = false;

    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("MainScene");
        DestroyObject(GameObject.Find("Global Audio"));
        DestroyObject(GameObject.Find("CursorManager"));
    }

    public void CreateRoomObject()
    {
        // RoomCatalog�� ���� ���� value���� ���ִٸ� RoomInfo�� �־��ݴϴ�.
        foreach (RoomInfo info in RoomCatalog.Values)
        {
            // ���� �����մϴ�.
            GameObject room = Instantiate(Resources.Load<GameObject>("Room"));

            // RoomContent�� ���� ������Ʈ�� �����մϴ�.
            room.transform.SetParent(RoomContent);

            // �� ������ �Է��մϴ�.
            room.GetComponent<Information>().SetInfo(info.Name, info.PlayerCount, info.MaxPlayers);


        }
    }


    public void OnClickCreateRoom()
    {
        // �� �ɼ��� ����
        RoomOptions Room = new RoomOptions();

        // �ִ� �������� ���� �����մϴ�.
        Room.MaxPlayers = byte.Parse(RoomPerson.text);

        // ���� ���� ���θ� �����մϴ�.
        Room.IsOpen = true;

        // �κ񿡼� �� ����� ���� ��ų�� �����մϴ�.
        Room.IsVisible = true;

        // ���� �����ϴ� �Լ�
        PhotonNetwork.CreateRoom(RoomName.text, Room);

    }


    public void AllDeleteRoom()
    {
        // Transform ������Ʈ�� �ִ� ���� ������Ʈ�� �����Ͽ� ��ü ������ �õ��մϴ�.
        foreach (Transform trans in RoomContent)
        {
            // Transform�� ������ �ִ� ���� ������Ʈ�� �蠫�ٴϴ�.
            Destroy(trans.gameObject);

        }

    }

    // �ش� �κ� �� ����� ���� ������ ������ ȣ��( �߰�, ���� ,����)
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        AllDeleteRoom();
        UpdateRoom(roomList);
        CreateRoomObject();
    }


    void UpdateRoom(List<RoomInfo> roomList)
    {
        for(int i =0; i< roomList.Count; i++)
        {
            // �ش� �̸��� RoomCatalog�� key������ �����Ǿ� �ִٸ�
            if (RoomCatalog.ContainsKey(roomList[i].Name))
            {
                // RemovedFromList : (true) �뿡�� ������ �Ǿ��� ��
                if (roomList[i].RemovedFromList)
                {
                    RoomCatalog.Remove(roomList[i].Name);
                    continue;
                }

            }

            RoomCatalog[roomList[i].Name] = roomList[i];
        }
        



    }


}
