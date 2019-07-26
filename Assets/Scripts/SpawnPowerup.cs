using UnityEngine;
using System.Collections;

public class SpawnPowerup : MonoBehaviour {
	public GameObject Boost;
	public GameObject Barrier;
	public GameObject HealthPowerUp;
	public GameObject RangeDestructPowerUp;
	public float min = 0.2f;
    public float max = 1.48f;
	public float spawnTime = 1f;
	public float spawnDelay = 10f;

	void Start() {  
		//https://docs.unity3d.com/ScriptReference/MonoBehaviour.InvokeRepeating.html
		InvokeRepeating("Spawn", spawnTime, spawnDelay);
	}
	void Spawn() {  
		var spawnPoint = new Vector2(transform.position.x, Random.Range(max, min));
		//https://docs.unity3d.com/ScriptReference/Random.Range.html
		int rand = Random.Range( 0, 4 );
		//https://docs.unity3d.com/ScriptReference/Object.Instantiate.html
		switch (rand)
		{
			case 0:
				Instantiate(Barrier, spawnPoint, Quaternion.identity);
				break;
			case 1:
				Instantiate(Barrier, spawnPoint, Quaternion.identity);
				break;
			case 2:
				Instantiate(Barrier, spawnPoint, Quaternion.identity);
				break;
			case 3:
				Instantiate(Barrier, spawnPoint, Quaternion.identity);
				break;
			default:
				break;
		}
	}
}