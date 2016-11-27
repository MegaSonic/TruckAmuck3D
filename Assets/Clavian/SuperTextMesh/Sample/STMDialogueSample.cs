using UnityEngine;
using System.Collections;

public class STMDialogueSample : MonoBehaviour {
	public SuperTextMesh textMesh;
	public KeyCode advanceKey = KeyCode.Return;
	public string[] lines;
	private int currentLine = 0;

	void Start () {
		Apply();
	}
	public void CompletedDrawing(){
		Debug.Log("I completed reading! Done!");
	}
	public void CompletedUnreading(){
		Debug.Log("I completed unreading!! Bye!");
		Apply();
	}
	void Apply () {
		
		//isDoneFading = false;
		textMesh.Text = lines[currentLine]; //invoke accessor so rebuild() is called
		//simulate two people talking back and forth.
		if(currentLine % 2 != 0){//odd number, person #1
			textMesh.minPitch = 0.6f; //change the way the character "speaks"!
			textMesh.maxPitch = 0.8f;
//			textMesh.drawAnimation = SuperTextMesh.DrawAnimation.Stamp;
		}else{//even number, person #2
			textMesh.minPitch = 1.4f;
			textMesh.maxPitch = 1.6f;
//			textMesh.drawAnimation = SuperTextMesh.DrawAnimation.Grow;
		}
		currentLine++; //move to next line of dialogue...
		currentLine %= lines.Length; //or loop back to first one
	}
	void Update () {
		if(Input.GetKeyDown(advanceKey)){
			if(textMesh.reading){ //is text being read out?
				textMesh.SpeedRead(); //show all text, or speed up
			}
			else if(!textMesh.unreading){
				textMesh.UnRead();
				//Apply();
			}
		}
		if(Input.GetKeyUp(advanceKey)){
			textMesh.RegularRead(); //return to normal reading speed, if possible.
		}
		
	}
}