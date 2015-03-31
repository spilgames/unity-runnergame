using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {

	//the level pieses
	public GameObject[] levelSections;
	//how many emojis to pool
	public int sectionPoolSize = 40;
	
	//pool of emojis
	List<GameObject> sectionPool;

	void Start(){
		sectionPool = new List<GameObject> ();
		for (int i = 0; i < sectionPoolSize; i ++) {
			GameObject section = (GameObject)Instantiate(levelSections[Random.Range(0,levelSections.Length)]);
			section.SetActive(false);
			sectionPool.Add(section);
		}
	}

	public void SpawnLevelSection(){
		List<GameObject> sectionsAvalible = new List<GameObject> ();
		for (int i = 0; i < sectionPool.Count; i++) {
			if (!sectionPool [i].activeInHierarchy) {
				sectionsAvalible.Add (sectionPool [i]);
			}
		}
		int sectionChosen = Random.Range (0, sectionsAvalible.Count);
		sectionsAvalible [sectionChosen].transform.position = new Vector3 (4.8f, 0, 0);
		sectionsAvalible [sectionChosen].transform.rotation = transform.rotation;
		sectionsAvalible [sectionChosen].SetActive (true);
		
	}
}
