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
        if(!gsc.IsWaiting()){
            if(!footstepAudio.isPlaying) {
                footstepAudio.pitch = Random.Range(0.8f, 1.2f);
                footstepAudio.Play();
            }

            if(Vector3.Distance(goblin.transform.position, destination) > 0.5f) {
                gsc.GetAnim().SetBool("isWalking", true);
                Vector3 moveTo = new Vector3(destination.x, goblin.transform.position.y, destination.z);
                goblin.transform.position = Vector3.MoveTowards(goblin.transform.position, moveTo, 0.05f);
                goblin.transform.LookAt(moveTo);
            } else {
                gsc.GetAnim().SetBool("isWalking", false);
                NewDestination();
            }

        } else {
            gsc.GetAnim().SetBool("isWalking", false);
        }
    }

    public override void CheckTransitions() {}

    public override void OnStateEnter() {
        footstepAudio = gsc.GetFootstepAudio();
        goblin = gsc.gameObject;
        currentRoom = gsc.GetCurrentRoom().GetComponent<DungeonRoom>();
        NewDestination();
        gsc.StartExploring();
    }

    public override void OnStateExit() {}

    private void NewDestination() {
        gsc.StartExploring();
        Vector3 roomPos = currentRoom.GetRoomPosition();
        float randX = Random.Range(roomPos.x - 5.0f, roomPos.x + 5.0f);
        float randZ = Random.Range(roomPos.z - 5.0f, roomPos.z + 5.0f);
        destination = new Vector3(randX, goblin.transform.position.y, randZ);
    }
}
