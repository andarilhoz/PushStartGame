using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour {

	public float speed;
	public GameController gameController;


	public int value;
	private Transform target;
	private Rigidbody2D rg2d;

	void Start () {
		rg2d = transform.GetComponent<Rigidbody2D>();;
		target = GameObject.Find("CoinTarget").transform;
	}
	void Update () {
		Vector3 direction = (target.transform.position - transform.position).normalized;
		rg2d.MovePosition(transform.position + direction * speed * Time.deltaTime);
		bool visibleByCamera = transform.GetComponent<SpriteRenderer>().IsVisibleFrom(Camera.main);
		if(!visibleByCamera)
			ReachTarget();		
		StartCoroutine(LifeTimeOut());
	}
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.transform == target.transform)
			ReachTarget();
	}

	void ReachTarget(){
		 gameController.AddMoney(value);
		 Destroy(gameObject);
	}

	IEnumerator LifeTimeOut(){
		yield return new WaitForSeconds(3);
		ReachTarget();
	}
}
