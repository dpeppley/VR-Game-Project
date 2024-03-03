using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinExplore : GoblinState {
    private bool exploringDone = false;
    private DungeonRoom currentRoom;

    private GameObject goblin;

    private Vector3 destination;

    private AudioSource footstepAudio;

    public GoblinExplore(GoblinStateController gsc) : base(gsc) {}

    public override void Act() {
        // if(gsc.exploring) {
        //     if(!gsc.IsWaiting()) {
        //         if(currentRoom.HasGoblins()) {
        //             gsc.FightGoblin();
        //         }
        //     }
        // } else {
        //     gsc.SetState(new GoblinMove());
        // }
        if(!gsc.IsWaiting()){
            if(!footstepAudio.isPlaying) {
                footstepAudio.pitch = Random.Range(0.8f, 1.2f);
                footstepAudio.Play();
            }

            if(Vector3.Distance(goblin.transform.position, destination) > 0.5f) {
                goblin.transform.position = Vector3.MoveTowards(goblin.transform.position, destination, 0.05f);
            } else {
                NewDestination();
            }

        }
    }

    public override void CheckTransitions() {
        // if(!gsc.IsExploring()) {
        //     gsc.SetState(new GoblinMove(gsc));
        // }
    }

    public override void OnStateEnter() {
        footstepAudio = gsc.GetFootstepAudio();
        goblin = gsc.gameObject;
        currentRoom = gsc.GetCurrentRoom().GetComponent<DungeonRoom>();
        NewDestination();
        gsc.StartExploring();
    }

    public override void OnStateExit() {}

    private void NewDestination() {
        Vector3 roomPos = currentRoom.GetRoomPosition();
        float randX = Random.Range(roomPos.x - 5.0f, roomPos.x + 5.0f);
        float randZ = Random.Range(roomPos.z - 5.0f, roomPos.z + 5.0f);
        destination = new Vector3(randX, goblin.transform.position.y, randZ);
    }
}
