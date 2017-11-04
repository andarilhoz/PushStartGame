using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GameState))]
public class GameController : MonoBehaviour {

	public Text nickname;
	public Text money;
	
	private GameState gameState;
	private User loggedUser;

	void Start()
	{
		gameState = transform.GetComponent<GameState>();
	}

	public void PauseGame(){
		gameState.changeState(GameState.State.Pause);
	}

	public void PlayGame(){
		gameState.changeState(GameState.State.Play);
	}

	public void SetUser(User user){
		loggedUser = user;
		money.text = user.money.ToString();
		nickname.text = loggedUser.nickname;
	}
}
