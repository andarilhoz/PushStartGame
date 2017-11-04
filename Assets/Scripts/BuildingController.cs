using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour {
	public Building building;
	public bool active;

	private BuildingType type;
	private GameController gameController;

	void Awake()
	{
		gameController = GameObject.Find("GameController").GetComponent<GameController>();
		type = (BuildingType) System.Enum.Parse (typeof (BuildingType), transform.name);
		building = gameController.availableBuildings.Find(building => building.type == type);
	}

	void Start () {
		StartCoroutine(BuildingIt());
	}
	
	
	IEnumerator EarnMoney () {
		yield return new WaitForSeconds(building.timeToProfit);
		if(active)
			gameController.AddMoney(building.moneyPerTime);
		StartCoroutine(EarnMoney());
	}


	IEnumerator BuildingIt(){
		yield return new WaitForSeconds(building.timeToBuild);
		active = true;
		StartCoroutine(EarnMoney());
	}

}
