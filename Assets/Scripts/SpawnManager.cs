using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public float cooldownTime = 3f;
	public GameObject prefabToSpawn;

	[SerializeField]
	private GameObject crystalBall;
	[SerializeField]
	private DungeonRoom room;
	[SerializeField]
	private Material mat;
	[SerializeField]
	private Material cooldownMat;
	[SerializeField]
	private Material alertMat;

	private float nextSpawnTime = 0;
	private bool cooldown = false;
	private bool isActive;

	private GameController gc;

	private void Start() {
		gc = GameObject.Find("GameController").GetComponent<GameController>();
		ActivateCamera();
	}

	private void Update()
    {
		if (Time.time > nextSpawnTime)
		{
			if (cooldown)
			{
				cooldown = false;
				crystalBall.GetComponent<Renderer>().material = mat;
			} 
		}

		if(gc.IsGameRunning()) {
			crystalBall.GetComponent<Renderer>().material = mat;
			isActive = true;
		} else {
			crystalBall.GetComponent<Renderer>().material = cooldownMat;
			isActive = false;
		}
    }


	public void ActivateCamera() {
		if(isActive && !cooldown) {
			cooldown = true;

			// Raycast from camera to plane
			Quaternion spawnRotation = Quaternion.Euler(new Vector3(0, 0, 0));
			GameObject goblin = Instantiate(prefabToSpawn, VariedPosition(room.GetRoomPosition()) , spawnRotation);
			goblin.GetComponent<GoblinStateController>().SetCurrentRoom(gameObject.transform.parent.GetComponent<DungeonRoom>());
			room.AddGoblin(goblin);

			crystalBall.GetComponent<Renderer>().material = cooldownMat;
			nextSpawnTime = Time.time + cooldownTime;
		}
	}


	private Vector3 VariedPosition(Vector3 pos) {
		float newX = pos.x + Random.Range(-5f, 5f);
		float newY = pos.y;
		float newZ = pos.z + Random.Range(-5f, 5f);
		Vector3 newPos = new Vector3(newX, newY, newZ);
		return newPos;
	}

	public IEnumerator AlertFlash() {
		for(int i = 0; i < 3; i++) {
			crystalBall.GetComponent<Renderer>().material = alertMat;
			crystalBall.GetComponent<Light>().enabled = true;
			yield return new WaitForSeconds(0.5f);
			crystalBall.GetComponent<Renderer>().material = mat;
			crystalBall.GetComponent<Light>().enabled = false;
			yield return new WaitForSeconds(0.5f);
		}
	}
}