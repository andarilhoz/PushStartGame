using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefController : MonoBehaviour {
	public static void setSfxVolume(float volume){
		PlayerPrefs.SetFloat("sfxVolume",volume);
	}

	public static float getSfxVolume(){
		return PlayerPrefs.GetFloat("sfxVolume",1);
	}

}