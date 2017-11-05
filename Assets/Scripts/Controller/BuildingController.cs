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

	public GameObject timerSlider;

	private GameController gameController;
	private SpriteRenderer buildRenderer;
	private Animator animator;

	private SpriteSlider spriteSlider;
	private float timer = 0;
	private float totalTime = 0;
	private ParticleSystem pSystem;
	void FixedUpdate()
	{
		if(timer <= totalTime){
			timer += Time.deltaTime;
			spriteSlider.UpdateBar(timer/totalTime);
		}
	}

	void Awake()
	{
		gameController = GameObject.Find("GameController").GetComponent<GameController>();
		buildRenderer = transform.GetComponent<SpriteRenderer>();
		animator = transform.GetComponent<Animator>();
		audioSource = transform.GetComponent<AudioSource>();
		spriteSlider = timerSlider.GetComponent<SpriteSlider>();
		pSystem = transform.GetComponent<ParticleSystem>();
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
		timer = 0;
		totalTime = building.timeToProfit;
		yield return new WaitForSeconds(building.timeToProfit);
		StartCoroutine(EarnMoney());
	}


	IEnumerator BuildingIt(){
		audioSource.clip = buildingSound;
		audioSource.Play();
		timer = 0;
		totalTime = building.timeToBuild;
		yield return new WaitForSeconds(building.timeToBuild);
		audioSource.Stop();
		buildRenderer.color = readyColor;
		animator.SetTrigger("Complete");
		pSystem.Stop();
		active = true;
		StartCoroutine(EarnMoney());
	}

	public void StopAudio(){
		audioSource.Stop();
	}

	public void PlayAudio(){
		if(!active)
			audioSource.Play();
	}
}
