using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GameState))]
public class GameController : MonoBehaviour {

	public Text nickname;
	public Text money;
	public List<Building> availableBuildings;
	


	private GameState gameState;
	private User loggedUser;

	void Start()
	{
		gameState = transform.GetComponent<GameState>();
		User dummyUser = new User();
		dummyUser.nickname = "Goku";
		dummyUser.money = 0;
		SetUser(dummyUser);
	}

	public void TogglePause(){
		if(gameState.getState() != GameState.State.Pause)
			gameState.changeState(GameState.State.Pause);
		else
			gameState.changeState(GameState.State.Play);
	}

	public void PlayGame(){
		gameState.changeState(GameState.State.Play);
	}

	public void SetUser(User user){
		loggedUser = user;
		money.text = user.money.ToString();
		nickname.text = loggedUser.nickname;
	}

	public void AddMoney(int moneyEarned){
		loggedUser.money += moneyEarned;
		money.text = loggedUser.money.ToString();
	}
}
