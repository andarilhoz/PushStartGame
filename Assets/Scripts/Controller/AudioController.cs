using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour {
	[Tooltip("Slider controlador do volume")]
	public Slider sfxSlider;	
	public GameObject modalVolume;

	void Start(){
		AudioListener.volume =  PlayerPrefController.getSfxVolume();

		sfxSlider.onValueChanged.AddListener(delegate {
			ChangeVolume();
		});
	}


	public void OpenVolumeModal(){
		modalVolume.SetActive(true);
		sfxSlider.value = PlayerPrefController.getSfxVolume();
	}
	public void CloseVolumeModal(){
		modalVolume.SetActive(false);
	}
	void ChangeVolume() {
		float value = sfxSlider.value;
		AudioListener.volume = value;
		PlayerPrefController.setSfxVolume(value);
	}

	public static void StopAllBuildingAudio() {
		foreach(BuildingController building in FindObjectsOfType<BuildingController>()) {
			building.StopAudio();
		}
	}

	public static void PlayAllBuildingAudio(){
		foreach(BuildingController building in FindObjectsOfType<BuildingController>()) {
			building.PlayAudio();
		}
	}
}
