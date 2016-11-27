using UnityEngine;
using System.Collections;
/*
Make SURE!!!!!! that this is added as a DYNAMIC event!!!
When adding events, it'll give you two lists to choose from, with a divider in the middle.
Dynamic events are ABOVE static events!
*/
public class STMEventInterpreter : MonoBehaviour {
	public GameObject confetti;
	public AudioSource au;
	public AudioClip myClip;
	public string seperator = "=";
	public string audioTag = "a";
	public string playSoundString = "blegh";
	public void DoEvent(string s, int index, Vector3 pos, Vector3 cornerPos){ //the string from the event, index of the letter in the string, world position of this letter, position of bottom-left corner
		string myTag = audioTag + seperator;
		if(myTag.Length <= s.Length && s.Substring(0,myTag.Length) == myTag){ //first two characters are "a="?
			string playString = "mySound";
			if(myTag.Length + playString.Length <= s.Length && s.Substring(audioTag.Length + seperator.Length, playString.Length) == playString){
				Debug.Log("Playing sound!");
			}else{
				Debug.Log("Unknown audio event!");
			}
		}
		else if(s == "printpos"){
			Debug.Log(pos); //print the position of this letter.
			Debug.DrawLine(pos, pos+Vector3.down, Color.red, 5.0f, false);
		}
		else if(s == "confetti"){
			Instantiate(confetti,pos,confetti.transform.rotation);
		}
		else if(s == playSoundString){
			Debug.Log("Playing sound!");
			au.PlayOneShot(myClip,1f); //alt way of playing clips, for example
		}
		else{
			Debug.Log("Unknown event!");
		}
	}
}
