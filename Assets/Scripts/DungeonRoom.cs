using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;

public class DungeonRoom : MonoBehaviour {
    [SerializeField]
    public DungeonPath[] nextRooms;
    [SerializeField]
    private Transform roomPos;

    [SerializeField]
    private CrystalBallManager crystalBall;

    private List<GameObject> goblins;

    void Awake() {
        goblins = new List<GameObject>();
    }

    public bool HasPaths() {
        return nextRooms.Length != 0;
    }

    public DungeonPath[] GetPathOptions() {
        return nextRooms;
    }

    public Vector3 GetRoomPosition() {
        return roomPos.position;
    }

    public void AddGoblin(GameObject goblin) {
        goblins.Add(goblin);
    }

    public void RemoveGoblin() {
        GameObject goblinToRemove = goblins[goblins.Count-1];
        goblins.RemoveAt(goblins.Count-1);
        GameObject.Destroy(goblinToRemove);
    }

    public bool HasGoblins() {
        return goblins.Count > 0;
    }

    public void AlertCamera() {
        crystalBall.AlertFlash();
    }
}
