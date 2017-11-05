using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {
  
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
