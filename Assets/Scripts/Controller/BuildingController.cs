using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour {
	public Building building;
	public bool active;

	public GameObject coin;

	private GameController gameController;

	private SpriteRenderer buildRenderer;
	public Color constructingColor;
	public Color readyColor;

	void Awake()
	{
		gameController = GameObject.Find("GameController").GetComponent<GameController>();
		buildRenderer = transform.GetComponent<SpriteRenderer>();
	}

	void Start () {
		buildRenderer.color = constructingColor;
		StartCoroutine(BuildingIt());
	}
	
	
	IEnumerator EarnMoney () {
		yield return new WaitForSeconds(building.timeToProfit);
		if(active){
			GameObject coinInstance = GameObject.Instantiate(coin,transform.position,transform.rotation);
			coinInstance.GetComponent<CoinController>().value = building.moneyPerTime;
			coinInstance.GetComponent<CoinController>().gameController = gameController;
		}
		StartCoroutine(EarnMoney());
	}


	IEnumerator BuildingIt(){
		yield return new WaitForSeconds(building.timeToBuild);
		buildRenderer.color = readyColor;
		active = true;
		StartCoroutine(EarnMoney());
	}

}
