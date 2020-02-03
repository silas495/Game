using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class World : MonoBehaviour {

	public Transform agentPrefab;

	public int nAgents;

	public List<Agent> agents;

	void Start () {

		agents = new List<Agent>();
		spawn(agentPrefab, nAgents);

		agents.AddRange(FindObjectsOfType<Agent>());
	
	}
	
	void Update () {
	
	}

	void spawn(Transform prefab, int n){

		for(int i=0; i< n; i++){

			var obj = Instantiate(prefab,
			                      new Vector3(Random.Range(-10,10),0, Random.Range(-10,10)),
			                      Quaternion.identity);
		}
	}

	public List<Agent> getNeigh(Agent agent, float radius){
		List<Agent> agts = new List<Agent>();
		foreach (var a in agents)
		{
			if (a != agent && Vector3.Distance(a.xPosition, agent.xPosition) < radius)
			{
				agts.Add(a);
			}
		}
		return agts;
	}
}
