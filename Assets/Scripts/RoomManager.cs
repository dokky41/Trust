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

    // 룸 목록을 저장하기 위한 자료구조
    Dictionary<string,RoomInfo> RoomCatalog = new Dictionary<string,RoomInfo>();


    // Update is called once per frame
    void Update()
    {
        // 룸에 이름과 인원을 입력하지않으면 버튼 비활성화
        if (RoomName.text.Length > 0 && RoomPerson.text.Length > 0)
            RoomCreate.interactable = true;
        else
            RoomCreate.interactable = false;

    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("MainScene");
    }

    public void CreateRoomObject()
    {
        // RoomCatalog에 여러 개의 value값이 들어가있다면 RoomInfo에 넣어줍니다.
        foreach (RoomInfo info in RoomCatalog.Values)
        {
            // 룸을 생성합니다.
            GameObject room = Instantiate(Resources.Load<GameObject>("Room"));

            // RoomContent의 하위 오브젝트로 설정합니다.
            room.transform.SetParent(RoomContent);

            // 룸 정보를 입력합니다.
            room.GetComponent<Information>().SetInfo(info.Name, info.PlayerCount, info.MaxPlayers);


        }
    }


    public void OnClickCreateRoom()
    {
        // 룸 옵션을 설정
        RoomOptions Room = new RoomOptions();

        // 최대 접속자의 수를 설정합니다.
        Room.MaxPlayers = byte.Parse(RoomPerson.text);

        // 룸의 오픈 여부를 설정합니다.
        Room.IsOpen = true;

        // 로비에서 룸 목록을 노출 시킬지 설정합니다.
        Room.IsVisible = true;

        // 룸을 생성하는 함수
        PhotonNetwork.CreateRoom(RoomName.text, Room);

    }


    public void AllDeleteRoom()
    {
        // Transform 오브젝트에 있는 하위 오브젝트에 접근하여 전체 삭제를 시도합니다.
        foreach (Transform trans in RoomContent)
        {
            // Transform이 가지고 있는 게임 오브젝트를 삭젷바니다.
            Destroy(trans.gameObject);

        }

    }

    // 해당 로비에 방 목록의 변경 사항이 있으면 호출( 추가, 삭제 ,참가)
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
            // 해당 이름이 RoomCatalog의 key값으로 설정되어 있다면
            if (RoomCatalog.ContainsKey(roomList[i].Name))
            {
                // RemovedFromList : (true) 룸에서 삭제가 되었을 때
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
