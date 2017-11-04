using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameState))]
public class GameController : MonoBehaviour {
	
	private GameState gameState;

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
}
