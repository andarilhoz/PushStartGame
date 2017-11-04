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

	public delegate void loggedUserEvent();
	public static event loggedUserEvent onLoggedUserChange;

	void Start()
	{
		gameState = transform.GetComponent<GameState>();
		User dummyUser = new User();
		dummyUser.nickname = "Goku";
		dummyUser.money = 250;
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
		ChangeUser();
	}

	public void AddMoney(int moneyEarned){
		loggedUser.money += moneyEarned;
		ChangeUser();
	}

	public void SpendMoney(int moneySpended){
		loggedUser.money -= moneySpended;
		ChangeUser();
	}

	public void ChangeUser(){
		if(onLoggedUserChange != null)
			onLoggedUserChange();
		money.text = loggedUser.money.ToString();
		nickname.text = loggedUser.nickname;
	}

	public float GetMoney(){
		return loggedUser.money;
	}
}
