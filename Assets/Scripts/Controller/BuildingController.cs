using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour {
	public Building building;
	public bool active;
	public GameObject coin;

	public Color constructingColor;
	public Color readyColor;

	public AudioSource audioSource;
	public AudioClip buildingSound;
	public AudioClip moneySound;

	private GameController gameController;
	private SpriteRenderer buildRenderer;
	private Animator animator;

	void Awake()
	{
		gameController = GameObject.Find("GameController").GetComponent<GameController>();
		buildRenderer = transform.GetComponent<SpriteRenderer>();
		animator = transform.GetComponent<Animator>();
		audioSource = transform.GetComponent<AudioSource>();
	}

	void Start () {
		buildRenderer.color = constructingColor;
		StartCoroutine(BuildingIt());
	}
	
	
	IEnumerator EarnMoney () {
		if(active){
			GameObject coinInstance = GameObject.Instantiate(coin,transform.position,transform.rotation);
			coinInstance.GetComponent<CoinController>().value = building.moneyPerTime;
			coinInstance.GetComponent<CoinController>().gameController = gameController;
		}
		audioSource.PlayOneShot(moneySound);
		yield return new WaitForSeconds(building.timeToProfit);
		StartCoroutine(EarnMoney());
	}


	IEnumerator BuildingIt(){
		audioSource.clip = buildingSound;
		audioSource.Play();
		yield return new WaitForSeconds(building.timeToBuild);
		audioSource.Stop();
		buildRenderer.color = readyColor;
		animator.SetTrigger("Complete");
		active = true;
		StartCoroutine(EarnMoney());
	}

}
