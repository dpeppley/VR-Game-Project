using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdventurerExplore : AdventurerState {
    private bool exploringDone = false;
    private DungeonRoom currentRoom;

    private GameObject adventurer;

    private Vector3 destination;

    public AdventurerExplore(AdventurerStateController asc) : base(asc) {}

    public override void Act() {
        if(asc.IsExploring()) {
            if(!asc.IsWaiting()) {
                if(currentRoom.HasGoblins()) {
                    asc.FightGoblin();
                }
            }
        } else {
            asc.SetState(new AdventurerMove(asc));
        }
        if(!asc.IsWaiting()){
            if(Vector3.Distance(adventurer.transform.position, destination) > 0.5f) {
                asc.SetWalkAvatar();
                asc.GetAnimator().SetBool("isWalking", true);
                adventurer.transform.position = Vector3.MoveTowards(adventurer.transform.position, destination, 0.02f);
            } else {
                asc.GetAnimator().SetBool("isWalking", false);
                NewDestination();
            }
        }
    }

    public override void CheckTransitions() {
        if(!asc.IsExploring()) {
            asc.SetState(new AdventurerMove(asc));
        }
    }

    public override void OnStateEnter() {
        adventurer = asc.gameObject;
        currentRoom = asc.GetCurrentRoom().GetComponent<DungeonRoom>();
        currentRoom.AlertCamera();
        NewDestination();
        asc.StartExploring();
    }

    public override void OnStateExit() {}

    private void NewDestination() {
        asc.StartIdling();
        Vector3 roomPos = currentRoom.GetRoomPosition();
        float randX = Random.Range(roomPos.x - 5.0f, roomPos.x + 5.0f);
        float randZ = Random.Range(roomPos.z - 5.0f, roomPos.z + 5.0f);
        destination = new Vector3(randX, adventurer.transform.position.y, randZ);
    }
}
