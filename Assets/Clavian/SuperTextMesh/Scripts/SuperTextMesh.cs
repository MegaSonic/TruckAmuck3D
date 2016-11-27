//Copyright (c) 2016 Kai Clavier [kaiclavier.com] Do Not Distribute
using UnityEngine;
using UnityEngine.Events; //for the OnComplete event
#if UNITY_EDITOR
using UnityEditor; //for loading default stuff and menu thing
#endif
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI; //for in-game UI stuff

using UnityEngine.SceneManagement; //for OnSceneLoaded

#if UNITY_EDITOR
[CustomEditor(typeof(SuperTextMesh))]
[CanEditMultipleObjects] //sure why not
public class SuperTextMeshEditor : Editor{
	override public void OnInspectorGUI(){
		serializedObject.Update(); //for onvalidate stuff!
		var stm = target as SuperTextMesh; //get this text mesh as a component
		//Gather variables to display and use
		//SerializedProperty uiMode = serializedObject.FindProperty("uiMode"); //not actually shown, but used to toggle appearance
		SerializedProperty text = serializedObject.FindProperty("text"); //get the inspector's text field
		SerializedProperty font = serializedObject.FindProperty("font");
		SerializedProperty color = serializedObject.FindProperty("color");
		SerializedProperty richText = serializedObject.FindProperty("richText");
		SerializedProperty readDelay = serializedObject.FindProperty("readDelay");
		SerializedProperty speedReadScale = serializedObject.FindProperty("speedReadScale");
		SerializedProperty ignoreTimeScale = serializedObject.FindProperty("ignoreTimeScale");
		SerializedProperty disableAnimatedText = serializedObject.FindProperty("disableAnimatedText");
		SerializedProperty drawOrder = serializedObject.FindProperty("drawOrder");

		SerializedProperty drawAnimName = serializedObject.FindProperty("drawAnimName"); //so changing value calls onvalidate
		
		SerializedProperty unreadDelay = serializedObject.FindProperty("unreadDelay");
		SerializedProperty undrawOrder = serializedObject.FindProperty("undrawOrder");
		SerializedProperty undrawAnimName = serializedObject.FindProperty("undrawAnimName");

		SerializedProperty audioSource = serializedObject.FindProperty("audioSource");
		SerializedProperty audioClips = serializedObject.FindProperty("audioClips");
		SerializedProperty stopPreviousSound = serializedObject.FindProperty("stopPreviousSound");
		SerializedProperty minPitch = serializedObject.FindProperty("minPitch");
		SerializedProperty maxPitch = serializedObject.FindProperty("maxPitch");
		SerializedProperty pitchMode = serializedObject.FindProperty("pitchMode");
		SerializedProperty overridePitch = serializedObject.FindProperty("overridePitch");
		SerializedProperty speedReadPitch = serializedObject.FindProperty("speedReadPitch");
		SerializedProperty perlinPitchMulti = serializedObject.FindProperty("perlinPitchMulti");

		SerializedProperty size = serializedObject.FindProperty("size");
		SerializedProperty quality = serializedObject.FindProperty("quality");
		SerializedProperty filterMode = serializedObject.FindProperty("filterMode");
		SerializedProperty style = serializedObject.FindProperty("style");
		SerializedProperty baseOffset = serializedObject.FindProperty("baseOffset");
		SerializedProperty lineSpacing = serializedObject.FindProperty("lineSpacing");
		SerializedProperty characterSpacing = serializedObject.FindProperty("characterSpacing");
		SerializedProperty tabSize = serializedObject.FindProperty("tabSize");
		SerializedProperty autoWrap = serializedObject.FindProperty("autoWrap");
		SerializedProperty wrapText = serializedObject.FindProperty("wrapText");

		SerializedProperty breakText = serializedObject.FindProperty("breakText");
		SerializedProperty insertHyphens = serializedObject.FindProperty("insertHyphens");
		SerializedProperty anchor = serializedObject.FindProperty("anchor");
		SerializedProperty alignment = serializedObject.FindProperty("alignment");
		SerializedProperty textShader = serializedObject.FindProperty("textShader");

		SerializedProperty onCompleteEvent = serializedObject.FindProperty("onCompleteEvent");
		SerializedProperty customEvent = serializedObject.FindProperty("customEvent");
		SerializedProperty onUndrawnEvent = serializedObject.FindProperty("onUndrawnEvent");
		SerializedProperty modifyVertices = serializedObject.FindProperty("modifyVertices");
		SerializedProperty vertexMod = serializedObject.FindProperty("vertexMod");

		SerializedProperty debugMode = serializedObject.FindProperty("debugMode");

		SerializedProperty shadowColor = serializedObject.FindProperty("shadowColor");
		SerializedProperty shadowDistance = serializedObject.FindProperty("shadowDistance");
		SerializedProperty shadowAngle = serializedObject.FindProperty("shadowAngle");
		SerializedProperty outlineColor = serializedObject.FindProperty("outlineColor");
		SerializedProperty outlineWidth = serializedObject.FindProperty("outlineWidth");
		SerializedProperty shaderBlend = serializedObject.FindProperty("shaderBlend");

	//Actually Drawing it to the inspector:
		Rect r = GUILayoutUtility.GetRect(EditorGUIUtility.fieldWidth, 0f); //get width on inspector, minus scrollbar

		GUIStyle foldoutStyle = new GUIStyle(EditorStyles.foldout); //create a new foldout style, for the bold foldout headers
		foldoutStyle.fontStyle = FontStyle.Bold; //set it to look like a header
	//TEXT DATA ICON
		Object textDataObject = Resources.Load("TextData"); //get text data object
		GUIStyle textDataStyle = new GUIStyle(EditorStyles.label);
		//textDataStyle.fixedWidth = 14;
		//textDataStyle.fixedHeight = 14;
		//Get Texture2D one of these two ways:
		//Texture2D textDataIcon = AssetDatabase.LoadAssetAtPath("Assets/Clavian/SuperTextMesh/Scripts/SuperTextMeshDataIcon.png", typeof(Texture2D)) as Texture2D;
		Texture2D textDataIcon = EditorGUIUtility.ObjectContent(textDataObject, typeof(TextMeshData)).image as Texture2D;
		textDataStyle.normal.background = textDataIcon; //apply
		textDataStyle.active.background = textDataIcon;
		if(GUI.Button(new Rect(r.width - 2, r.y, 16, 16), new GUIContent("", "Edit TextData"), textDataStyle)){ //place at exact spot
			//Selection.activeObject = textDataObject; //go to textdata!
			stm.data.textDataEditMode = !stm.data.textDataEditMode; //show textdata inspector!
		}
	//
		if(stm.data.textDataEditMode){//show textdata file instead
			var serializedData = new SerializedObject(stm.data);
			serializedData.Update();

			//Gather data again:
			SerializedProperty dataWaves = serializedData.FindProperty("waves");
			SerializedProperty dataJitters = serializedData.FindProperty("jitters");
			SerializedProperty dataDrawAnims = serializedData.FindProperty("drawAnims");

			SerializedProperty dataColors = serializedData.FindProperty("colors");
			SerializedProperty dataGradients = serializedData.FindProperty("gradients");
			SerializedProperty dataTextures = serializedData.FindProperty("textures");

			SerializedProperty dataDelays = serializedData.FindProperty("delays");
			SerializedProperty dataVoices = serializedData.FindProperty("voices");
			SerializedProperty dataFonts = serializedData.FindProperty("fonts");

			SerializedProperty dataAutoDelays = serializedData.FindProperty("autoDelays");
			SerializedProperty dataClips = serializedData.FindProperty("clips");

			SerializedProperty dataDisableAnimatedText = serializedData.FindProperty("disableAnimatedText");

		//Draw it!
			EditorGUILayout.Space(); //////////////////SPACE
			EditorGUILayout.Space(); //////////////////SPACE
			EditorGUILayout.Space(); //////////////////SPACE
			EditorGUILayout.HelpBox("Editing Text Data. Click the [T] to exit!", MessageType.None, true);

			stm.data.showEffectsFoldout = EditorGUILayout.Foldout(stm.data.showEffectsFoldout, "Effects", foldoutStyle);
			if(stm.data.showEffectsFoldout){
				EditorGUI.indentLevel++;
				EditorGUILayout.PropertyField(dataWaves, true); //yes, include children
				EditorGUILayout.PropertyField(dataJitters, true);
				EditorGUILayout.PropertyField(dataDrawAnims, true);
				EditorGUI.indentLevel--;
			}
			stm.data.showTextColorFoldout = EditorGUILayout.Foldout(stm.data.showTextColorFoldout, "Text Color", foldoutStyle);
			if(stm.data.showTextColorFoldout){
				EditorGUI.indentLevel++;
				EditorGUILayout.PropertyField(dataColors, true);
				EditorGUILayout.PropertyField(dataGradients, true);
				EditorGUILayout.PropertyField(dataTextures, true);
				EditorGUI.indentLevel--;
			}
			stm.data.showInlineFoldout = EditorGUILayout.Foldout(stm.data.showInlineFoldout, "Inline", foldoutStyle);
			if(stm.data.showInlineFoldout){
				EditorGUI.indentLevel++;
				EditorGUILayout.PropertyField(dataDelays, true);
				EditorGUILayout.PropertyField(dataVoices, true);
				EditorGUILayout.PropertyField(dataFonts, true);
				EditorGUI.indentLevel--;
			}
			stm.data.showAutomaticFoldout = EditorGUILayout.Foldout(stm.data.showAutomaticFoldout, "Automatic", foldoutStyle);
			if(stm.data.showAutomaticFoldout){
				EditorGUI.indentLevel++;
				EditorGUILayout.PropertyField(dataAutoDelays, true);
				EditorGUILayout.PropertyField(dataClips, true);
				EditorGUI.indentLevel--;
			}
			stm.data.showMasterFoldout = EditorGUILayout.Foldout(stm.data.showMasterFoldout, "Master", foldoutStyle);
			if(stm.data.showMasterFoldout){
				EditorGUI.indentLevel++;
				EditorGUILayout.PropertyField(dataDisableAnimatedText, true);
				EditorGUI.indentLevel--;
			}

			serializedData.ApplyModifiedProperties();
		}else{ //draw actual text mesh inspector:
		
			EditorGUILayout.PropertyField(text);

			stm.showAppearanceFoldout = EditorGUILayout.Foldout(stm.showAppearanceFoldout, "Appearance", foldoutStyle);
			if(stm.showAppearanceFoldout){
				EditorGUILayout.PropertyField(font);
				EditorGUILayout.PropertyField(color); //richtext default stuff...
				EditorGUILayout.PropertyField(size);

				EditorGUI.BeginDisabledGroup(!stm.UseThisFont.dynamic);
				EditorGUILayout.PropertyField(style);
				EditorGUI.EndDisabledGroup();

				EditorGUILayout.PropertyField(richText);

				EditorGUILayout.Space(); //////////////////SPACE

				EditorGUI.BeginDisabledGroup(!stm.UseThisFont.dynamic);
				EditorGUILayout.PropertyField(quality); //text rendering
				EditorGUI.EndDisabledGroup();

				EditorGUILayout.PropertyField(filterMode);
				EditorGUILayout.PropertyField(textShader); //appearance

				if(!stm.uiMode && (stm.r.sharedMaterial.HasProperty("_ShadowColor") || stm.r.sharedMaterial.HasProperty("_OutlineColor"))){
					stm.showShaderFoldout = EditorGUILayout.Foldout(stm.showShaderFoldout, "Shader", foldoutStyle);
					if(stm.showShaderFoldout){ //show shader settings
						if(stm.r.sharedMaterial.HasProperty("_ShadowColor")){
							EditorGUILayout.PropertyField(shadowColor);
						}
						if(stm.r.sharedMaterial.HasProperty("_ShadowDistance")){
							EditorGUILayout.PropertyField(shadowDistance);
						}
						if(stm.r.sharedMaterial.HasProperty("_ShadowAngle")){
							EditorGUILayout.PropertyField(shadowAngle);
						}
						if(stm.r.sharedMaterial.HasProperty("_OutlineColor")){
							EditorGUILayout.PropertyField(outlineColor);
						}
						if(stm.r.sharedMaterial.HasProperty("_OutlineWidth")){
							EditorGUILayout.PropertyField(outlineWidth);
						}
						if(stm.r.sharedMaterial.HasProperty("_Blend")){
							EditorGUILayout.PropertyField(shaderBlend);
						}
					}
				}
				//for UI mode:
				if(stm.uiMode && (stm.c.GetMaterial().HasProperty("_ShadowColor") || stm.c.GetMaterial().HasProperty("_OutlineColor"))){
					stm.showShaderFoldout = EditorGUILayout.Foldout(stm.showShaderFoldout, "Shader", foldoutStyle);
					if(stm.showShaderFoldout){ //show shader settings
						if(stm.c.GetMaterial().HasProperty("_ShadowColor")){
							EditorGUILayout.PropertyField(shadowColor);
						}
						if(stm.c.GetMaterial().HasProperty("_ShadowDistance")){
							EditorGUILayout.PropertyField(shadowDistance);
						}
						if(stm.c.GetMaterial().HasProperty("_ShadowAngle")){
							EditorGUILayout.PropertyField(shadowAngle);
						}
						if(stm.c.GetMaterial().HasProperty("_OutlineColor")){
							EditorGUILayout.PropertyField(outlineColor);
						}
						if(stm.c.GetMaterial().HasProperty("_OutlineWidth")){
							EditorGUILayout.PropertyField(outlineWidth);
						}
						if(stm.c.GetMaterial().HasProperty("_Blend")){
							EditorGUILayout.PropertyField(shaderBlend);
						}
					}
				}
			}
			//EditorGUILayout.Space(); //////////////////SPACE
			stm.showPositionFoldout = EditorGUILayout.Foldout(stm.showPositionFoldout, "Position", foldoutStyle);
			if(stm.showPositionFoldout){
				EditorGUILayout.PropertyField(baseOffset); //physical stuff
				EditorGUILayout.PropertyField(anchor);
				//if(!uiMode.boolValue){ //restrict this to non-ui only...?
					EditorGUILayout.PropertyField(alignment);
				//}
				EditorGUILayout.Space(); //////////////////SPACE
				EditorGUILayout.PropertyField(lineSpacing); //text formatting
				EditorGUILayout.PropertyField(characterSpacing);
				EditorGUILayout.PropertyField(tabSize);
				EditorGUILayout.Space(); //////////////////SPACE
				if(!stm.uiMode){ //wrapping text works differently for UI:
					EditorGUILayout.PropertyField(autoWrap); //automatic...
					if(autoWrap.floatValue > 0f){
						EditorGUILayout.PropertyField(breakText);
						if(breakText.boolValue){
							EditorGUILayout.PropertyField(insertHyphens);
						}
					}
				}else{
					EditorGUILayout.PropertyField(wrapText);
					if(wrapText.boolValue){
						EditorGUILayout.PropertyField(breakText);
						if(breakText.boolValue){
							EditorGUILayout.PropertyField(insertHyphens);
						}
					}
				}
				EditorGUILayout.Space(); //////////////////SPACE
				EditorGUILayout.PropertyField(modifyVertices);
				if(modifyVertices.boolValue){
					EditorGUILayout.PropertyField(vertexMod);
				}
			}
			//EditorGUILayout.Space(); //////////////////SPACE
			stm.showTimingFoldout = EditorGUILayout.Foldout(stm.showTimingFoldout, "Timing", foldoutStyle);
			if(stm.showTimingFoldout){
				EditorGUILayout.PropertyField(ignoreTimeScale);
				EditorGUILayout.PropertyField(disableAnimatedText);
				EditorGUILayout.Space(); //////////////////SPACE
				EditorGUILayout.PropertyField(readDelay); //technical stuff
				if(readDelay.floatValue > 0f){
					EditorGUILayout.PropertyField(drawOrder);
					EditorGUILayout.PropertyField(drawAnimName);
					//stuff that needs progamming to work:
					stm.showFunctionalityFoldout = EditorGUILayout.Foldout(stm.showFunctionalityFoldout, "Functionality", foldoutStyle);
					if(stm.showFunctionalityFoldout){
						EditorGUILayout.PropertyField(speedReadScale);
						EditorGUILayout.Space(); //////////////////SPACE
						EditorGUILayout.PropertyField(unreadDelay);
						EditorGUILayout.PropertyField(undrawOrder);
						EditorGUILayout.PropertyField(undrawAnimName);
					}
					//GUIContent drawAnimLabel = new GUIContent("Draw Animation", "What draw animation will be used. Can be customized with TextData.");
					//selectedAnim.intValue = EditorGUILayout.Popup("Draw Animation", selectedAnim.intValue, stm.DrawAnimStrings());
					stm.showAudioFoldout = EditorGUILayout.Foldout(stm.showAudioFoldout, "Audio", foldoutStyle);
					if(stm.showAudioFoldout){
						//EditorGUILayout.LabelField("Audio", EditorStyles.boldLabel); //HEADER
						EditorGUILayout.PropertyField(audioSource);
						if(audioSource.objectReferenceValue != null){ //flag
							EditorGUILayout.PropertyField(audioClips, true); //yes, show children
							EditorGUILayout.PropertyField(stopPreviousSound);
							EditorGUILayout.PropertyField(pitchMode);
							if(pitchMode.enumValueIndex == (int)SuperTextMesh.PitchMode.Normal){
								//nothing!
							}
							else if(pitchMode.enumValueIndex == (int)SuperTextMesh.PitchMode.Single){
								EditorGUILayout.PropertyField(overridePitch);
							}
							else{ //random between two somethings
								EditorGUILayout.PropertyField(minPitch);
								EditorGUILayout.PropertyField(maxPitch);
							}
							if(pitchMode.enumValueIndex == (int)SuperTextMesh.PitchMode.Perlin){
								EditorGUILayout.PropertyField(perlinPitchMulti);
							}
							if(speedReadScale.floatValue < 1000f){
								EditorGUILayout.PropertyField(speedReadPitch);
							}
						}
					}
					stm.showEventFoldout = EditorGUILayout.Foldout(stm.showEventFoldout, "Events", foldoutStyle);
					if(stm.showEventFoldout){
						EditorGUILayout.PropertyField(onCompleteEvent);
						EditorGUILayout.PropertyField(onUndrawnEvent);
						EditorGUILayout.PropertyField(customEvent);
					}
				}
			}
			//EditorGUILayout.Space(); //////////////////SPACE
			EditorGUILayout.PropertyField(debugMode);
		}

		serializedObject.ApplyModifiedProperties();
	}
}
#endif

[HelpURL("Assets/Clavian/SuperTextMesh/Documentation/SuperTextMesh.html")] //make the help open local documentation
[AddComponentMenu("Mesh/Super Text Mesh", 3)] //allow it to be added as a component
[ExecuteInEditMode]
public class SuperTextMesh : MonoBehaviour { //MaskableGraphic... rip
	//foldout bools for editor. not on the GUI script, cause they get forgotten
	public bool showAppearanceFoldout = true;
	public bool showShaderFoldout = true;
	public bool showPositionFoldout = true;
	public bool showTimingFoldout = false;
	public bool showFunctionalityFoldout = false;
	public bool showAudioFoldout = false;
	public bool showEventFoldout = false;
	
	#if UNITY_EDITOR
	//Add to the gameobject menu:
	[MenuItem("GameObject/3D Object/Super 3D Text", false, 4000)] //instantiate a prefab of this
	private static void MakeNewText(MenuCommand menuCommand){
	    //Create a game object
	    GameObject textFab = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Clavian/SuperTextMesh/Prefabs/New Super Text.prefab", typeof(GameObject));
	    GameObject newTextMesh = Instantiate(textFab); //instantiate prefab from assets
	    newTextMesh.transform.name = textFab.name; //so it doesn't have "(Clone)" after
		GameObjectUtility.SetParentAndAlign(newTextMesh, menuCommand.context as GameObject); //Ensure it gets reparented if this was a context click (otherwise does nothing)
		Undo.RegisterCreatedObjectUndo(newTextMesh, "Create " + newTextMesh.name); //Register the creation in the undo system
		Selection.activeObject = newTextMesh;
	}
	[MenuItem("GameObject/UI/Super Text", false, 2001)] //instantiate a prefab of this
	private static void MakeNewUIText(MenuCommand menuCommand){
	    //Create a game object
	    GameObject textFab = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Clavian/SuperTextMesh/Prefabs/Super Text.prefab", typeof(GameObject));
	    GameObject newTextMesh = Instantiate(textFab); //instantiate prefab from assets
	    newTextMesh.transform.name = textFab.name; //so it doesn't have "(Clone)" after
		GameObjectUtility.SetParentAndAlign(newTextMesh, menuCommand.context as GameObject); //Ensure it gets reparented if this was a context click (otherwise does nothing)
		Undo.RegisterCreatedObjectUndo(newTextMesh, "Create " + newTextMesh.name); //Register the creation in the undo system
		Selection.activeObject = newTextMesh;
		//TODO: force-attach to canvas if it exists, or auto-create new one. And auto-child to existing canvas.
	}
	#endif
	
	private TextMeshData _data; 
	public TextMeshData data{
		get{
			if(_data == null) _data = Resources.Load("TextData") as TextMeshData; //load textdata
			return _data;
		}
	}
	private Transform _t;
	public Transform t{
		get{
			if(_t == null) _t = this.transform;
			return _t;
		}
	}
	private MeshFilter _f;
	public MeshFilter f{
		get{
			if(_f == null) _f = t.GetComponent<MeshFilter>();
			if(_f == null) _f = t.gameObject.AddComponent<MeshFilter>();
			return _f;
		}
	}
	private MeshRenderer _r;
	public MeshRenderer r{
		get{
			if(_r == null) _r = t.GetComponent<MeshRenderer>();
			if(_r == null) _r = t.gameObject.AddComponent<MeshRenderer>();
			return _r;
		}
	}
	private CanvasRenderer _c;
	public CanvasRenderer c{
		get{
			if(_c == null) _c = t.GetComponent<CanvasRenderer>();
			if(_c == null) _c = t.gameObject.AddComponent<CanvasRenderer>();
			return _c;
		}
	}
	public bool uiMode; //is it in UI mode? please don't change this manually

	private List<TextInfo> info = new List<TextInfo>(); //will sync w/ PROCESSED text... or maybe BE procesed text?
	private List<int> lineBreaks = new List<int>(); //what characters are line breaks
	[TextArea(3,10)] //[Multiline] also works, but i like this better
	public string text = "<c=rainbow><w>Hello, World!";
	public string Text{
		get{
			return this.text;
		}
		set{
			this.text = value;
			Rebuild(); //auto-rebuild
		}
	}
	[HideInInspector] public string drawText; //text, after removing junk
	[HideInInspector] public string hyphenedText; //text, with junk added to it
	[Tooltip("Font to be used by this text mesh. .rtf and .otf fonts are supported.")]
	public Font font;
	public Font useFont; //a font given by the <f> tag
	public Font UseThisFont{ //ref this to get the right font
		get{
			return useFont != null ? useFont : font;
		}
	}
	[Tooltip("Default color of the text mesh. This can be changed with the <c> tag! See the docs for more info.")]
	public Color32 color = Color.white;
	[Tooltip("Will the text listen to tags like <b> and <i>? See docs for a full list of tags.")]
	public bool richText = true; //care about formatting like <b>?
	[Tooltip("Delay in seconds between letters getting read out. Disabled if set to 0.")]
	public float readDelay = 0f; //disabled if 0.
	[Tooltip("Multiple of time for when speeding up text. Set it to a big number like 1000 to show all text immediately.")]
	public float speedReadScale = 2f; //for speeding thru text, this will be the delay. set to 0 to display instantly.
	[Tooltip("Whether reading uses deltaTime or fixedDeltaTime")]
	public bool ignoreTimeScale = true;
	public float GetDeltaTime{
		get{
			return data.disableAnimatedText || disableAnimatedText ? 0f : ignoreTimeScale ? Time.unscaledDeltaTime : Time.deltaTime;
		}
	}
	public float GetTime{
		get{
			return data.disableAnimatedText || disableAnimatedText ? 0f : ignoreTimeScale ? Time.unscaledTime : Time.time;
		}
	}
	public float GetDeltaTime2{//for when the text is getting read out
		get{
			return ignoreTimeScale ? Time.unscaledDeltaTime : Time.deltaTime;
		}
	}
	public bool disableAnimatedText = false; //disable for just this mesh
	/*
	public float GetTime2{ 
		get{
			return ignoreTimeScale ? Time.unscaledTime : Time.time;
		}
	}*/

	//public int selectedAnim = 0; //what draw animation is selected currently.....
	[Tooltip("Name of what draw animation will be used. Case-sensitive.")]
	public string drawAnimName = "Appear"; //this is a string instead of a custom dropdown so reordering saved animations can't change it

	[Tooltip("Delay between letters, for undrawing.")]
	public float unreadDelay = 0.05f;
	[Tooltip("Undraw order.")]
	public DrawOrder undrawOrder = DrawOrder.AllAtOnce;
	[Tooltip("Undraw animation name.")]
	public string undrawAnimName = "Appear";

	[Tooltip("Audio source for read sound clips. Sound won't be played if null.")]
	public AudioSource audioSource;
	[Tooltip("Default sound to be read by the above audio source. Can be left null to make no sound by default.")]
	public AudioClip[] audioClips;
	[Tooltip("Should a new letter's sound stop a previous one and play, or let the old one keep playing?")]
	public bool stopPreviousSound = true;

	[Tooltip("Pitch options for reading out text.")]
	public PitchMode pitchMode = PitchMode.Normal;
	public enum PitchMode{
		Normal,
		Single,
		Random,
		Perlin
	}
	[Tooltip("New pitch for the sound clip.")]
	[Range(0f,3f)]
	public float overridePitch = 1f;
	[Tooltip("Minimum pitch for random pitches. If same or greater than max pitch, this will be the pitch.")]
	[Range(0f,3f)]
	public float minPitch = 0.9f;
	[Tooltip("Maximum pitch for random pitches.")]
	[Range(0f,3f)]
	public float maxPitch = 1.2f;
	[Range(-2f,2f)]
	[Tooltip("This amount will be ADDED to the pitch when speedreading. Speedreading uses the delay from 'Fast Delay'")]
	public float speedReadPitch = 0f;
	[Tooltip("Multiple for how fast the perlin noise will advance.")]
	public float perlinPitchMulti = 1.0f;
	private bool speedReading = false;
	private bool skippingToEnd = false; //alt version of speedread that just skips to the end

	[HideInInspector] public bool reading = false; //is text currently being read out? this is public so it can be used by other scripts!
	private Coroutine readRoutine; //coroutine that handles reading out text
	[HideInInspector] public bool unreading = false;

	[Tooltip("Size in local space for letters, by default. Can be changed with the <s> tag.")]
	public float size = 1f; //size of letter in local space! not percentage of quality. letters can have diff sizes individually
	[Range(1,500)]
	[Tooltip("Point size of text. Try to keep it as small as possible while looking crisp!")]
	public int quality = 64; //actual text size. point size
	[Tooltip("Choose 'Point' for a crisp look. You'll probably want that for pixel fonts!")]
	public FilterMode filterMode = FilterMode.Bilinear;
	//TODO: completely redraw text texture upon quality change. 2016-06-07 note: might have already done this
	[Tooltip("Default letter style. Can be changed with the <i> and <b> tags, using rich text.")]
	public FontStyle style = FontStyle.Normal;
	[Tooltip("Additional offset for the text mesh from the transform, in local space.")]
	public Vector3 baseOffset = Vector3.zero; //for offsetting z, mainly
	[Tooltip("Adjust line spacing between multiple lines of text. 1 is the default for the font.")]
	public float lineSpacing = 1.0f;
	[Tooltip("Adjust additional spacing between characters. 0 is default.")]
	public float characterSpacing = 0.0f;
	[Tooltip("How far tabs indent.")]
	public float tabSize = 4.0f;
	[Tooltip("Distance in local space before a line break is automatically inserted at the previous space. Disabled if set to 0.")]
	public float autoWrap = 12f; //if text on one row exceeds this, insert line break at previously available space
	private float AutoWrap{ //get autowrap limit OR ui bounds
		get{
			if(uiMode && wrapText) return (float)((RectTransform)t).rect.width; //get wrap limit, within left and right bounds!
			return autoWrap;
		}
	}

	[Tooltip("Should text wrap at the edge of the bounding box, or go over?")]
	public bool wrapText = true; 
	[Tooltip("With auto wrap, should large words be split to fit in the box?")]
	public bool breakText = true;
	[Tooltip("When large words are split, Should a hyphen be inserted?")]
	public bool insertHyphens = true;
	[Tooltip("The anchor point of the mesh. For UI text, this also controls the alignment.")]
	public TextAnchor anchor = TextAnchor.UpperLeft;
	[Tooltip("Decides where text should align to. Uses the Auto Wrap box as bounds.")]
	public Alignment alignment = Alignment.Left;
	public enum Alignment{
		Left,
		Center,
		Right,
		Justified,
		ForceJustified
	}
	[Tooltip("The shader to be used by this text mesh. Check the assets for more shaders, and the docs for more info.")]
	public Shader textShader; //material to use on all submeshes or whatever

	private bool areWeAnimating = false; //do we need to update every frame?

	[HideInInspector] public Vector3 topLeftBounds = Vector3.zero;
	[HideInInspector] public Vector3 bottomRightBounds = Vector3.zero;

	private Mesh uiMesh; //keep track of mesh assigned to UI

	public UnityEvent onCompleteEvent; //when the mesh is done drawing
	public UnityEvent onUndrawnEvent; //for when undrawing finishes

	[System.Serializable] public class CustomEvent : UnityEvent<string, int, Vector3, Vector3>{} //tag, index in string, center position in world, bottom-left corner position in world
	public CustomEvent customEvent;
	public bool modifyVertices = false;
	[System.Serializable] public class VertexMod : UnityEvent<Vector3[], Vector3[], Vector3[]>{}
	public VertexMod vertexMod;

	public bool debugMode = false; //pretty much just here to un-hide inspector stuff

	[HideInInspector] public float totalReadTime = 0f;
	[HideInInspector] public float totalUnreadTime = 0f;
	[HideInInspector] public float currentReadTime = 0f; //what position in the mesh it's currently at. Right now, this is just so jitters don't animate more than they should when you speed past em.

	//generate these with ur vert calls or w/e!!!
	private Vector3[] endVerts = new Vector3[0];
	private Color32[] endCol32 = new Color32[0];
	private Vector3[] startVerts = new Vector3[0];
	private Color32[] startCol32 = new Color32[0];

	private float timeDrawn; //Time.time when the mesh was drawn. or Time.unscaledTime, depending

	public enum DrawOrder{
		LeftToRight,
		AllAtOnce,
		OneWordAtATime,
		Random,
		RightToLeft,
		ReverseLTR
	}
	[Tooltip("What order the text will draw in. 'All At Once' will ignore read delay. 'Robot' displays one word at a time. If set to 'Random', Read Delay becomes the time it'll take to draw the whole mesh.")]
	public DrawOrder drawOrder = DrawOrder.LeftToRight;

	private bool callReadFunction = false; //will the read function need to be called?

	private bool initMaterial = false;

	//special shader stuff!
	public Color32 shadowColor = Color.black;
	public float shadowDistance = 0.05f;
	[Range(0f,360f)]
	public float shadowAngle = 135f;
	public Color32 outlineColor = Color.black;
	public float outlineWidth = 0.05f;
	[Range(0f,1f)]
	public float shaderBlend = 0.025f;

	STMDrawAnimData UndrawAnim{
		get{
			STMDrawAnimData tmpAnim = data.drawAnims.Find(x => x.name == undrawAnimName);
			if(tmpAnim == null){
				tmpAnim = data.drawAnims[0];
			}
			return tmpAnim;
		}
	}
	//TODO 2016-07-05 not calling r.sharedMaterials in this way ^^^ every frame will save GC Alloc.
	//but........ material juggling is hard?? so it doesn't do it right now
/*
	public string[] DrawAnimStrings(){ //get strings for the dropdown thing
		string[] myStrings = new string[data.drawAnims.Count];
		for(int i=0, iL=myStrings.Length; i<iL; i++){
			myStrings[i] = data.drawAnims[i].name;
		}
		if(selectedAnim >= myStrings.Length){
			selectedAnim = 0; //don't go over if one gets deleted
		}
		return myStrings;
	}
*/
	void OnDrawGizmosSelected(){ //draw boundsssss
		if(!uiMode && autoWrap > 0f){ //bother to draw bounds?
			Gizmos.color = Color.blue;
			Vector3 localTopLeft = topLeftBounds;
			Vector3 localTopRight = new Vector3(bottomRightBounds.x, topLeftBounds.y, topLeftBounds.z);
			Vector3 localBottomLeft = new Vector3(topLeftBounds.x, bottomRightBounds.y, bottomRightBounds.z);
			Vector3 localBottomRight = bottomRightBounds;
			localTopLeft = t.rotation * localTopLeft;
			localTopRight = t.rotation * localTopRight;
			localBottomLeft = t.rotation * localBottomLeft;
			localBottomRight = t.rotation * localBottomRight;
			localTopLeft.Scale(t.localScale);
			localTopRight.Scale(t.localScale);
			localBottomLeft.Scale(t.localScale);
			localBottomRight.Scale(t.localScale);
			localTopLeft = t.position - localTopLeft; //do this last, so previous transforms are based around 0
			localTopRight = t.position - localTopRight;
			localBottomLeft = t.position - localBottomLeft;
			localBottomRight = t.position - localBottomRight;
			Gizmos.DrawLine(localTopLeft, localTopRight); //top
			Gizmos.DrawLine(localTopLeft, localBottomLeft); //left
			Gizmos.DrawLine(localTopRight, localBottomRight); //right
			Gizmos.DrawLine(localBottomLeft, localBottomRight); //bottom
		}
	}
	void OnFontTextureRebuilt(Font changedFont){
		if(changedFont != UseThisFont){ //is the font that updated NOT the one attached to this mesh?
			return; //dont update all, nothing needs to change.
		}
		Rebuild(); //the font texture attached to this mesh has changed. a rebuild is neccesary.
	}
	void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode){
		//Rebuild(); //otherwise, texture goes missing
		//2016-07-30 Unity 5.4: forget this for now
	}
	void Start(){
		Init();
	}
	void OnEnable(){
		Rebuild();//or it won't animate
	}
	void OnDisable(){
		//Debug.Log("Disabled!");
		if(uiMode){
			DestroyImmediate(uiMesh);
			c.Clear();
		}else{
			DestroyImmediate(f.sharedMesh);
		}
		Font.textureRebuilt -= OnFontTextureRebuilt;
	}
	void Init(){
		uiMode = t is RectTransform;
		
		initMaterial = true; //tell the material it needs to be redone

		SceneManager.sceneLoaded += OnSceneLoaded;
		Font.textureRebuilt += OnFontTextureRebuilt;
	}
	void OnValidate() {
		if(!UseThisFont.dynamic){
			if(UseThisFont.fontSize > 0){
				quality = UseThisFont.fontSize;
			}else{
				Debug.Log("You're probably using a custom font! \n Unity's got an where cutsom fonts have their size set to 0 by default and there's no way to change that! So to avoid this error, here's a solution: \n * Drag any font into Unity. Set it to be 'Unicode' or 'ASCII' in the inspector, depending on the characters you want your font to have. \n * Set 'Font Size' to whatever size you want 'quality' to be locked at. \n * Click the gear in the corner of the inspector and 'Create Editable Copy'. \n * Now, under the array of 'Character Rects', change size to 0 to clear everything. \n * Now you have a brand new font to edit that has a font size that's not zero! Yeah!");
			}
			//quality = UseThisFont.fontSize == 0 ? 64 : UseThisFont.fontSize; //for getting around fonts with a default size of 0.
			//Debug.Log("Font size is..." + UseThisFont.fontSize);
			style = FontStyle.Normal;
		}
		if(size < 0f){size = 0f;}
		if(readDelay < 0f){readDelay = 0f;}
		if(autoWrap < 0f){autoWrap = 0f;}
		if(minPitch > maxPitch){minPitch = maxPitch;}
		if(maxPitch < minPitch){maxPitch = minPitch;}
		if(speedReadScale < 0.01f){speedReadScale = 0.01f;}
		if(Application.isPlaying && t.gameObject.activeInHierarchy == true){ //cause update will call this, in-editor
			Rebuild();
			UpdateShaderSettings();
			//TODO: only call here if stuff changed?
			#if UNITY_EDITOR
			HideInspectorStuff(); //this is the only time, you're really gonna need this, so OnValidate() makes sense...?
			#endif
		}
	}
	#if UNITY_EDITOR
	public void HideInspectorStuff(){
		HideFlags flag = HideFlags.HideInInspector;
		switch(debugMode && !uiMode){
			case true: flag = HideFlags.None; break;//don't hide!
		}
		for(int i=0, iL=r.sharedMaterials.Length; i<iL; i++){ //hide shared materials
			if(r.sharedMaterials[i] != null){
				r.sharedMaterials[i].hideFlags = flag;
			}
		}
		r.hideFlags = flag; //hide mesh renderer and filter.
		f.hideFlags = flag;
	}
	#endif
	void UpdateShaderSettings(){
		if(!uiMode && r.sharedMaterial != null){//it should never be, but...
			if(r.sharedMaterial.HasProperty("_ShadowColor")){
				r.sharedMaterial.SetColor("_ShadowColor", shadowColor);
			}
			if(r.sharedMaterial.HasProperty("_ShadowDistance")){
				r.sharedMaterial.SetFloat("_ShadowDistance", shadowDistance);
			}
			if(r.sharedMaterial.HasProperty("_ShadowAngle")){
				r.sharedMaterial.SetFloat("_ShadowAngle", shadowAngle);
			}
			if(r.sharedMaterial.HasProperty("_OutlineColor")){
				r.sharedMaterial.SetColor("_OutlineColor", outlineColor);
			}
			if(r.sharedMaterial.HasProperty("_OutlineWidth")){
				r.sharedMaterial.SetFloat("_OutlineWidth", outlineWidth);
			}
			if(r.sharedMaterial.HasProperty("_Blend")){
				r.sharedMaterial.SetFloat("_Blend", shaderBlend);
			}
		}
		if(uiMode && c.GetMaterial() != null){
			if(c.GetMaterial().HasProperty("_ShadowColor")){
				c.GetMaterial().SetColor("_ShadowColor", shadowColor);
			}
			if(c.GetMaterial().HasProperty("_ShadowDistance")){
				c.GetMaterial().SetFloat("_ShadowDistance", shadowDistance);
			}
			if(c.GetMaterial().HasProperty("_ShadowAngle")){
				c.GetMaterial().SetFloat("_ShadowAngle", shadowAngle);
			}
			if(c.GetMaterial().HasProperty("_OutlineColor")){
				c.GetMaterial().SetColor("_OutlineColor", outlineColor);
			}
			if(c.GetMaterial().HasProperty("_OutlineWidth")){
				c.GetMaterial().SetFloat("_OutlineWidth", outlineWidth);
			}
			if(c.GetMaterial().HasProperty("_Blend")){
				c.GetMaterial().SetFloat("_Blend", shaderBlend);
			}
		}
	}
	public void Rebuild(){
		Rebuild(0f);
	}
	public void Rebuild(float startTime){
		timeDrawn = GetTime; //remember what time it started!
		unreading = false; //reset this, incase it was fading out
		speedReading = false; //2016-06-09 thank u drak
		skippingToEnd = false;
		if(uiMode){ //UI mode
			if(c != null){
				if(textShader == null){ //init
					textShader = Shader.Find("STM/UI"); //default
					c.materialCount = 1;
					Material newMat = new Material(textShader);
					c.SetMaterial(newMat,0);
					
					//text = "<w><c=rainbow>Hello, World!";
					
					
				}
				if(UseThisFont == null){
					font = Resources.GetBuiltinResource<Font>("Arial.ttf");
					size = 32;
					color = new Color32(50,50,50,255);
				}
				RebuildTextInfo();
				Texture mainTex = UseThisFont.material.mainTexture;
				mainTex.filterMode = filterMode;
				c.SetTexture(mainTex);//main texture
				c.materialCount = 1;
				DestroyImmediate(c.GetMaterial());
				c.SetMaterial(new Material(textShader), 0); //might cause a memory leak
			}
		}else{
			if(initMaterial || r.sharedMaterial == null){ //initialize, also catch null
				if(r.sharedMaterial != null){
					//Debug.Log("Clearing old material...");
					DestroyImmediate(r.sharedMaterial);
				}
				//Debug.Log("Initializing!");
				initMaterial = false;
				textShader = textShader == null ? Shader.Find("STM/Unlit") : textShader; //default
				r.sharedMaterial = new Material(textShader); //create initial material to be edited
				//text = "<w><c=rainbow>Hello, World!";
			}
			if(UseThisFont == null){
				font = Resources.GetBuiltinResource<Font>("Arial.ttf");
			}
			
			RebuildTextInfo();
			//Debug.Log(r.sharedMaterial);
			r.sharedMaterial.mainTexture = UseThisFont.material.mainTexture; //TODO: this might not have to be called every time
			r.sharedMaterial.mainTexture.filterMode = filterMode;
			r.sharedMaterial.shader = textShader;
		}
		if(audioSource != null){//initialize an audio source, if there's one. these settings are needed to behave properly
			audioSource.loop = false;
			audioSource.playOnAwake = false;
		}
		if(callReadFunction && Application.isPlaying){
			Read(startTime);
		}else{
			if(reading){
				StopCoroutine(readRoutine); //stop routine, just in case
				reading = false;
			}
			if(uiMode){ //UI mode
				DestroyImmediate(uiMesh);
				uiMesh = CreateMesh();
				c.SetMesh(uiMesh);
			}else{
				DestroyImmediate(f.sharedMesh);
				f.sharedMesh = CreateMesh();
			}
		}
	}
	void Update () { //only gets updated when something changes in-scene
		if(!Application.isPlaying){ //do same thing as onvalidate
			Rebuild();
			UpdateShaderSettings();
			#if UNITY_EDITOR
			HideInspectorStuff(); //this is the only time, you're really gonna need this, so OnValidate() makes sense...?
			#endif
		}
		if(!reading){
			currentReadTime += GetDeltaTime; //keep updating this, for the jitters
		}
		if(UseThisFont != null){ //TODO: make this only get called if something changed, or it's animating
			if(!reading && !unreading && areWeAnimating){
				if(uiMode){ //UI mode
					DestroyImmediate(uiMesh);
					uiMesh = CreateMesh();
					c.SetMesh(uiMesh);
				}else{
					DestroyImmediate(f.sharedMesh); //destroy old one??
					f.sharedMesh = CreateMesh();
				}
			}
		}
	}
	Mesh CreatePreReadMesh(Mesh startMesh, bool undrawingMesh){ //modify existing mesh
		startCol32 = new Color32[hyphenedText.Length * 4];
		startVerts = new Vector3[hyphenedText.Length * 4];

		STMDrawAnimData myUndrawAnim = UndrawAnim; //just in case...
		for(int i=0, iL=hyphenedText.Length; i<iL; i++){
			STMDrawAnimData myAnimData = undrawingMesh ? myUndrawAnim : info[i].drawAnimData; //which animation data to use?

			if(info[i].drawAnimData.startColor != Color.clear){ //use a specific start color
				startCol32[4*i + 0] = myAnimData.startColor;
				startCol32[4*i + 1] = myAnimData.startColor;
				startCol32[4*i + 2] = myAnimData.startColor;
				startCol32[4*i + 3] = myAnimData.startColor;
			}else{ //same color but transparent, for better lerping
				startCol32[4*i + 0] = new Color32(endCol32[4*i + 0].r,endCol32[4*i + 0].g,endCol32[4*i + 0].b,0);
				startCol32[4*i + 1] = new Color32(endCol32[4*i + 1].r,endCol32[4*i + 1].g,endCol32[4*i + 1].b,0);
				startCol32[4*i + 2] = new Color32(endCol32[4*i + 2].r,endCol32[4*i + 2].g,endCol32[4*i + 2].b,0);
				startCol32[4*i + 3] = new Color32(endCol32[4*i + 3].r,endCol32[4*i + 3].g,endCol32[4*i + 3].b,0);
			}
			Vector3 middle = new Vector3((endVerts[4*i + 0].x + endVerts[4*i + 1].x + endVerts[4*i + 2].x + endVerts[4*i + 3].x) * 0.25f,
														(endVerts[4*i + 0].y + endVerts[4*i + 1].y + endVerts[4*i + 2].y + endVerts[4*i + 3].y) * 0.25f,
														(endVerts[4*i + 0].z + endVerts[4*i + 1].z + endVerts[4*i + 2].z + endVerts[4*i + 3].z) * 0.25f);
			
			startVerts[4*i + 0] = Vector3.Scale((endVerts[4*i + 0] - middle), myAnimData.startScale) + middle + (myAnimData.startOffset * info[i].size);
			startVerts[4*i + 1] = Vector3.Scale((endVerts[4*i + 1] - middle), myAnimData.startScale) + middle + (myAnimData.startOffset * info[i].size);
			startVerts[4*i + 2] = Vector3.Scale((endVerts[4*i + 2] - middle), myAnimData.startScale) + middle + (myAnimData.startOffset * info[i].size);
			startVerts[4*i + 3] = Vector3.Scale((endVerts[4*i + 3] - middle), myAnimData.startScale) + middle + (myAnimData.startOffset * info[i].size);
		}
		startMesh.vertices = startVerts;
		startMesh.colors32 = startCol32;

		return startMesh;
	}
	void Read(float startTime){
		if(readRoutine != null){ //stop previous
			StopCoroutine(readRoutine);
			reading = false;
		}
		Mesh startMesh = CreatePreReadMesh(CreateMesh(),false);
		
		if(uiMode){
			DestroyImmediate(uiMesh);
			uiMesh = startMesh;
			c.SetMesh(startMesh);
		}else{
			DestroyImmediate(f.sharedMesh);
			f.sharedMesh = startMesh; //empty-lookin mesh
		}
		
		readRoutine = StartCoroutine(ReadOutText(startMesh, startTime));
		//now we have a mesh with nothing on it!
	}
	//cause I keep accidentally typing this? I think this name is better, might swap this
	public void Unread(){UnRead();}
	public void Undraw(){UnRead();}
	public void UnDraw(){UnRead();}
	public void UnRead(){
		Mesh finalMesh = ShowAllText(); //this is working
		/*
		2016-09-23:
		Make this just use SkipToEnd()? Can you grab the mesh variable without rebuilding?
		Maybe like... the whole UImesh thing?
		*/
		readRoutine = StartCoroutine(UnReadOutText(finalMesh));
	}
	
	public void SpeedRead(){
		if(reading){
			speedReading = true;
		}
	}
	public void SkipToEnd(){
		if(reading){
			skippingToEnd = true;
		}
	}
	public void RegularRead(){ //return to normal reading speed
		speedReading = false;
	}
	public Mesh ShowAllText(){
		return ShowAllText(false); //actually show all text
	}
	private Mesh ShowAllText(bool unreadingMesh){ //returns the mesh now because why not
		speedReading = false;
		Mesh finalMesh = unreadingMesh ? CreatePreReadMesh(CreateMesh(), true) : CreateMesh(); //return pre-read mesh after unreading, or not
		if(readRoutine != null){ //stop previous
			StopCoroutine(readRoutine);
		}
		if(uiMode){
			DestroyImmediate(uiMesh);
			uiMesh = finalMesh;
			c.SetMesh(uiMesh);
		}else{
			DestroyImmediate(f.sharedMesh);
			f.sharedMesh = finalMesh;
		}
		//Invoke complete events:
		if(reading){
			reading = false;
			onCompleteEvent.Invoke();
		}
		if(unreading){
			//unreading = false; //nope! Gotta stay in this state til it gets drawn again
			onUndrawnEvent.Invoke();
		}
		return finalMesh;
	}
	public void Append(string newText){
		//Debug.Log(totalReadTime);
		float newStartTime = totalReadTime;
		text += newText;
		Rebuild(newStartTime);
		/*
		calling this on text with the appear animation makes the last letter get re-read, too.
		figure this out when you're less tired!!!!

		also it seems to add a longer delay at the start?
		also is there a better way to remove sounds from spaces. the "clip" thing isn't very good with \n
		*/
	}

	int lastNum = -1; //the last index to be invoked on the previous cycle, so sounds can't play twice for the same letter!
	List<int> alreadyInvoked = new List<int>(); //list on indexes that have already been invoked so events cant happen twice
	
	public int latestNumber = 0; //declare here as a public variable, so the current character drawn can be reached at any time
	void UpdateDrawnMesh(Mesh drawnMesh, float myTime, bool undrawingMesh){
		//just do these to update the publicly stored verts for animating text.
		Mesh endMesh = CreatePreReadMesh(CreateMesh(), undrawingMesh); //this just updates the vertices
		Mesh startMesh = CreateMesh(); //copy??
		//TODO: ^^^ all this stuff, you only have to call again if areweanimating is true.

		STMDrawAnimData myUndrawAnim = UndrawAnim; //get the undraw animation, locally
		//get modified drawnMesh!
		Vector3[] verts = new Vector3[hyphenedText.Length * 4];
		Color32[] cols32 = new Color32[hyphenedText.Length * 4];

		for(int i=0, iL=hyphenedText.Length; i<iL; i++){ //for each point
			//lerp between start and end
			//Debug.Log((myTime - info[i].readTime) / info[i].animTime);
			STMDrawAnimData myAnimData = undrawingMesh ? myUndrawAnim : info[i].drawAnimData;
			float myReadTime = undrawingMesh ? info[i].unreadTime : info[i].readTime;
			//animate properly! (is there a way to do this by manipulating anim time?? yeah probably tbh)
			float divideAnimAmount = myAnimData.animTime == 0f ? 0.0000001f : myAnimData.animTime; //so it doesn't get NaN'd
			float divideFadeAmount = myAnimData.fadeTime == 0f ? 0.0000001f : myAnimData.fadeTime;
			float myAnimPos = (myTime - myReadTime) / divideAnimAmount;
			float myFadePos = (myTime - myReadTime) / divideFadeAmount;

			if(undrawingMesh){ //flip the range! so it lerps backwards
				//Debug.Log("Before: " + myFadePos);
				myAnimPos = 1f - myAnimPos;
				myFadePos = 1f - myFadePos;
				//Debug.Log("After: " + myFadePos);
			}

			if(myAnimPos > 0f && !undrawingMesh){ //animating this frame... and hasn't animated yet?
				if(!alreadyInvoked.Contains(i)){ //not already?
					alreadyInvoked.Add(i);
					DoEvent(i);
					//latestTime = Mathf.Max(myAnimPos, latestTime);

				}
			}
			//TODO: you don't have to update every vertice
			/*
			only needs to refresh if: myAnimPos/myFadePos is between 0 and 1
			or this letter is animating in some way (gradient, texture, jitter/wave)
			but doing it this way could accidentally leave some behind?
			...unless you see if the vertices are the same as the end vertices..........
			ok don't do this, cause it looks bad when a mesh gets laggier as it goes on. Ugh
			*/
			//if(IsThisLetterAnimating(i) || (myAnimPos > 0f && startVerts[4*i] != endVerts[4*i])){ //does it need to update every frame, or is it currently animating?
				verts[4*i+0] = Vector3.Lerp(startVerts[4*i+0],endVerts[4*i+0],myAnimData.animCurve.Evaluate(myAnimPos));
				verts[4*i+1] = Vector3.Lerp(startVerts[4*i+1],endVerts[4*i+1],myAnimData.animCurve.Evaluate(myAnimPos));
				verts[4*i+2] = Vector3.Lerp(startVerts[4*i+2],endVerts[4*i+2],myAnimData.animCurve.Evaluate(myAnimPos));
				verts[4*i+3] = Vector3.Lerp(startVerts[4*i+3],endVerts[4*i+3],myAnimData.animCurve.Evaluate(myAnimPos));
			//}
			//if(myFadePos > 0f && !AreColorsTheSame(startCol32[4*i],endCol32[4*i])){
				cols32[4*i+0] = Color.Lerp(startCol32[4*i+0],endCol32[4*i+0],myAnimData.fadeCurve.Evaluate(myFadePos));
				cols32[4*i+1] = Color.Lerp(startCol32[4*i+1],endCol32[4*i+1],myAnimData.fadeCurve.Evaluate(myFadePos));
				cols32[4*i+2] = Color.Lerp(startCol32[4*i+2],endCol32[4*i+2],myAnimData.fadeCurve.Evaluate(myFadePos));
				cols32[4*i+3] = Color.Lerp(startCol32[4*i+3],endCol32[4*i+3],myAnimData.fadeCurve.Evaluate(myFadePos));
			//}
		}
		drawnMesh.vertices = verts;
		drawnMesh.colors32 = cols32;
		drawnMesh.uv2 = endMesh.uv2; //copy over this!
		DestroyImmediate(endMesh);
		DestroyImmediate(startMesh);

		int alreadyInvokedCount = alreadyInvoked.Count;
		if(alreadyInvokedCount > 0 && !undrawingMesh){ //dont play sounds if undrawing...
			latestNumber = alreadyInvoked[alreadyInvokedCount-1];
			if(latestNumber != lastNum){ //new number?
				//Debug.Log("Playing sound at " + latestNumber + "! last num was: " + lastNum);
				PlaySound(latestNumber); //only play one sound, from the most recent number
				lastNum = latestNumber;
			}
		}else{
			lastNum = -1;
		}
	}
	bool AreColorsTheSame(Color32 col1, Color32 col2){
		if(col1.r == col2.r && col1.g == col2.g && col1.b == col2.b && col1.a == col2.a){
			return true;
		}
		return false;
	}
	IEnumerator ReadOutText(Mesh startMesh, float startTime){
		//Lerp certain vertices betwwen startmesh and endmesh
		//like, the mesh made by CreatePreReadMesh() and CreateMesh().
		reading = true;
		float timer = startTime;
		currentReadTime = startTime;
		if(startTime.Equals(0f)){ //for append()
			alreadyInvoked.Clear();
			lastNum = -1;
		}
		while(timer < totalReadTime && startMesh != null){ //check for null incase the mesh gets deleted midway
			if(skippingToEnd){
				timeDrawn -= totalReadTime; //why not (solves jitters not catching up)
				timer = totalReadTime;
			}
			UpdateDrawnMesh(startMesh, timer, false);
			if(uiMode){
				c.SetMesh(startMesh);
			}
			float delta = GetDeltaTime2;
			delta *= speedReading ? speedReadScale : 1f;
			timer += delta;
			currentReadTime = timer; //I could just use this as the timer, but w/e
			yield return null;
		}
		currentReadTime = totalReadTime;
		ShowAllText(); //just in case!
		//reading = false; //this gets set under ShowAllText(), so no real point to have it here
	}
	IEnumerator UnReadOutText(Mesh drawnMesh){
		unreading = true;
		float timer = 0f; //always start at 0
		while(timer < totalUnreadTime && drawnMesh != null){ //check for null incase the mesh gets deleted midway
			UpdateDrawnMesh(drawnMesh, timer, true);
			if(uiMode){
				c.SetMesh(drawnMesh);
			}
			timer += GetDeltaTime2;
			yield return null;
		}
		ShowAllText(true);
	}
	bool IsThisLetterAnimating(int i){ //return true if this letter is animating in some way, not related to drawanim
		if(info[i].waveData != null || info[i].jitterData != null &&
			(info[i].gradientData != null && info[i].gradientData.scrollSpeed != 0) ||
			(info[i].textureData != null && info[i].textureData.scrollSpeed != Vector2.zero)){
			return true;
		}
		return false;
	}
	void DoEvent(int i){
		if(info[i].ev.Count > 0){ //invoke events...
			//Debug.Log(info[i].ev); //this works........!!!!!
			Vector3 myPos = info[i].pos + t.position;
			Vector3 centerPos = info[i].Middle + t.position;
			for(int j=0, jL=info[i].ev.Count; j<jL; j++){
				customEvent.Invoke(info[i].ev[j], i, centerPos, myPos); //call the event!
			}
		}
		if(info[i].ev2.Count > 0){ //end repeating events!
			Vector3 myPos = info[i].pos + t.position;
			Vector3 centerPos = info[i].Middle + t.position;
			for(int j=0, jL=info[i].ev2.Count; j<jL; j++){
				customEvent.Invoke(info[i].ev2[j], i, centerPos, myPos); //call the event!
			}
		}
	}
	void PlaySound(int i){
		if(audioSource != null){//audio stuff
			STMVoiceData voice = info[i].voiceData; //grab voice data from mesh default, or info's voice
			AudioClip[] myAudioClips = voice != null ? voice.useAudioClips ? voice.audioClips : audioClips : audioClips;
			bool myStopPreviousSound = voice != null ? voice.useStopPreviousSound ? voice.stopPreviousSound : stopPreviousSound : stopPreviousSound;
			PitchMode myPitchMode = voice != null ? voice.usePitchMode ? voice.pitchMode : pitchMode : pitchMode;
			float myOverridePitch = voice != null ? voice.useNewPitch ? voice.overridePitch : overridePitch : overridePitch;
			float myMinPitch = voice != null ? voice.useNewPitch ? voice.minPitch : minPitch : minPitch;
			float myMaxPitch = voice != null ? voice.useNewPitch ? voice.maxPitch : maxPitch : maxPitch;
			float mySpeedReadPitch = voice != null ? voice.useSpeedReadPitch ? voice.speedReadPitch : speedReadPitch : speedReadPitch;
			STMSoundClipData mySoundClip = voice != null ? voice.soundClips.Find(x => x.character[0] == hyphenedText[i]) : null;
			if(myStopPreviousSound || !audioSource.isPlaying){
				audioSource.Stop();
				//if first character is this character...
				STMSoundClipData thisSoundClip = data.clips.Find(x => x.character[0] == hyphenedText[i]); //use this character from string
				if(thisSoundClip != null){
					audioSource.clip = thisSoundClip.clip;
				}else if(mySoundClip != null){ //use the one from the voice
					audioSource.clip = mySoundClip.clip;
				}else if(audioClips.Length > 0){
					audioSource.clip = myAudioClips[Random.Range(0,myAudioClips.Length)]; //get one of the clips
				}else{
					audioSource.clip = null;
				}
				if(audioSource.clip != null){
					switch(myPitchMode){
						case PitchMode.Perlin:
							audioSource.pitch = (Mathf.PerlinNoise(GetTime * perlinPitchMulti, 0f) * (myMaxPitch - myMinPitch)) + myMinPitch; //perlin noise
							break;
						case PitchMode.Random:
							audioSource.pitch = Random.Range(myMinPitch,myMaxPitch);
							break;
						case PitchMode.Single:
							audioSource.pitch = myOverridePitch;
							break;
						default:
							audioSource.pitch = 1f; //because of speedread pitch
							break;
					}
					if(speedReading){
						audioSource.pitch += mySpeedReadPitch;
					}
					audioSource.Play();
				}
			}
		}
	}
	//TODO: 2016-06-11 actually, im guessing that these values are a bitmask? you could prob just add & subtract em! but w/e
	FontStyle AddStyle(FontStyle original, FontStyle newStyle){
		switch(!UseThisFont.dynamic){
			case true: //act normally, add
				switch(original){
					case FontStyle.Bold:
						switch(newStyle){
							case FontStyle.Italic:
								return FontStyle.BoldAndItalic;
							default:
								return original;
						}
					case FontStyle.Italic:
						switch(newStyle){
							case FontStyle.Bold:
								return FontStyle.BoldAndItalic;
							default:
								return original;
						}
					case FontStyle.BoldAndItalic:
						return original;
					default: //normal
						return newStyle;
				}
			default:
				return FontStyle.Normal; //non-dynamic fonts can't handle bold/italic
		}
	}
	FontStyle SubtractStyle(FontStyle original, FontStyle subStyle){ //only bold and italic can be added and removed
		switch(original){
			case FontStyle.Bold:
				switch(subStyle){
					case FontStyle.Bold:
						return FontStyle.Normal;
					default:
						return original;
				}
			case FontStyle.Italic:
				switch(subStyle){
					case FontStyle.Italic:
						return FontStyle.Normal;
					default:
						return original;
				}
			case FontStyle.BoldAndItalic:
				switch(subStyle){
					case FontStyle.Bold:
						return FontStyle.Italic;
					case FontStyle.Italic:
						return FontStyle.Bold;
					default:
						return original; //just in case?
				}
			default: //normal
				return FontStyle.Normal;
		}
	}
	bool ValidHexcode (string hex){ //check if a hex code contains the right amount of characters, and allowed characters
		if(hex.Length != 3 && hex.Length != 4 && hex.Length != 6 && hex.Length != 8){ //hex code, alpha hex
			return false;
		}
		string allowedLetters = "0123456789ABCDEFabcdef";
		for(int i=0; i<hex.Length; i++){
			if(!allowedLetters.Contains(hex[i].ToString())){
				return false; //invalid string!!!
			}
		}
		return true;
	}
	Color32 HexToColor(string hex){ //convert a hex code string to a color
		if(hex.Length == 8){ //RGBA (FF00FF00)
			byte r = byte.Parse(hex.Substring(0,2), System.Globalization.NumberStyles.HexNumber);
			byte g = byte.Parse(hex.Substring(2,2), System.Globalization.NumberStyles.HexNumber);
			byte b = byte.Parse(hex.Substring(4,2), System.Globalization.NumberStyles.HexNumber);
			byte a = byte.Parse(hex.Substring(6,2), System.Globalization.NumberStyles.HexNumber);
			return new Color32(r,g,b,a);
		}
		if(hex.Length == 4){ //single-bytle for RGBA (F0F0)
			byte r = byte.Parse(hex.Substring(0,1) + hex.Substring(0,1), System.Globalization.NumberStyles.HexNumber);
			byte g = byte.Parse(hex.Substring(1,1) + hex.Substring(1,1), System.Globalization.NumberStyles.HexNumber);
			byte b = byte.Parse(hex.Substring(2,1) + hex.Substring(2,1), System.Globalization.NumberStyles.HexNumber);
			byte a = byte.Parse(hex.Substring(3,1) + hex.Substring(3,1), System.Globalization.NumberStyles.HexNumber);
			return new Color32(r,g,b,a);
		}
		if(hex.Length == 3){ //single-bytle for RGB (F0F)
			byte r = byte.Parse(hex.Substring(0,1) + hex.Substring(0,1), System.Globalization.NumberStyles.HexNumber);
			byte g = byte.Parse(hex.Substring(1,1) + hex.Substring(1,1), System.Globalization.NumberStyles.HexNumber);
			byte b = byte.Parse(hex.Substring(2,1) + hex.Substring(2,1), System.Globalization.NumberStyles.HexNumber);
			return new Color32(r,g,b,255);
		}
		else{ //RGB (FF00FF)
			byte r = byte.Parse(hex.Substring(0,2), System.Globalization.NumberStyles.HexNumber);
			byte g = byte.Parse(hex.Substring(2,2), System.Globalization.NumberStyles.HexNumber);
			byte b = byte.Parse(hex.Substring(4,2), System.Globalization.NumberStyles.HexNumber);
			return new Color32(r,g,b,255);
		}
	}
	Color32 GetColor(string myCol){
		STMColorData thisCol = data.colors.Find(x => x.name == myCol); //is it a custom color tag?
		if(thisCol != null){ //check textdata for a named color
			return thisCol.color;
		}
		if(ValidHexcode(myCol)){ //might be a hexcode?
			return HexToColor(myCol);
		}
		switch(myCol){ //see if it's a default unity color
			case "red": return Color.red;
			case "green": return Color.green;
			case "blue": return Color.blue;
			case "yellow": return Color.yellow;
			case "cyan": return Color.cyan;
			case "magenta": return Color.magenta;
			case "grey": return Color.grey;
			case "gray": return Color.gray;
			case "black": return Color.black;
			case "clear": return Color.clear;
			case "white": return Color.white;
			default: return color; //default color of mesh
		}
	}
	string ParseText(string myText){//return a cleaned up string and update textinfo!
		info.Clear();
		//set defaults:
		TextInfo myInfo = new TextInfo(style, color, size, alignment); //info for this one character, carried over from last
		useFont = null; //clear any applied fonts
		for(int i=0; i<myText.Length; i++){ //for each character to parse thru,
			if(info.Count == i && i > 0){ //no other delay yet...? /hasnt checkedAgain yet
				STMAutoDelayData myAutoDelay = data.autoDelays.Find(x => x.character == myText[i-1]);
				if(myAutoDelay != null && (myText[i] == ' ' || myText[i] == '\n' || myText[i] == '\t')){ //only if next character is "free"
					myInfo.delayData = new STMDelayData(myAutoDelay.count);
				}
			}
			bool checkAgain = false;
			if(richText && myText[i] == '\\'){ //might be a special character?
				if(myText.IndexOf("n") == i+1){ //line break
					myText = myText.Remove(i,2);
					myText = myText.Insert(i,'\n'.ToString());
					checkAgain = true;
				}
			}
			else if(richText && myText[i] == '<'){ //might be the start of a tag?
				if(myText.IndexOf("\\") == i+1){//Failsafe
					//don't parse this!
					myText = myText.Remove(i+1,1); //remove this backslash
				}
			//Line Break ["<br>"]
				else if(myText.IndexOf("<br>") == i){
					myText = myText.Remove(i,4);
					myText = myText.Insert(i,'\n'.ToString());
					checkAgain = true;
				}
			//Color ["<c>"]
				else if(myText.IndexOf("<c=") == i){
					int endIndex = myText.IndexOf(">",i);
					if(endIndex != -1){ //is there an end yet?
						//cancel old effect
						myInfo.colorData = new STMColorData(color);  //default
						myInfo.gradientData = null;
						myInfo.textureData = null;

						string myCol = myText.Substring(i+3, endIndex-i-3);
						STMTextureData thisTex = data.textures.Find(x => x.name == myCol);
						if(thisTex != null){// is this a texture?
							myInfo.textureData = thisTex;
							myInfo.colorData = new STMColorData(Color.white); //don't stack with mesh's default color. aka: be white so the color of the texture doesn't get multiplied!
						}else{
							STMGradientData thisGrad = data.gradients.Find(x => x.name == myCol); //is it a custom color tag?
							if(thisGrad != null){ //is this a gradient?
								myInfo.gradientData = thisGrad;
								//myInfo.useGradient = true;
							}
							else{ //no? try for HEX code & default color
								STMColorData thisCol = new STMColorData(GetColor(myCol));
								myInfo.colorData = thisCol;
							}
						}
						myText = myText.Remove(i,endIndex+1-i);
						checkAgain = true; //check this spot again.
					}
				}
				else if(myText.IndexOf("</c>") == i){
					myInfo.colorData = new STMColorData(color);  //default
					myInfo.gradientData = null;
					myInfo.textureData = null;
					myText = myText.Remove(i,4);
					checkAgain = true; //do it this way, incase the same tag is used twice in one spot...
				}
				/*
			//Image ["<i>"]
				else if(myText.IndexOf("<i=") == i){
					int endIndex = myText.IndexOf(">",i);
					if(endIndex != -1){ //is there an end yet?
						string myImage = myText.Substring(i+3, endIndex-i-3);
						STMImageData thisImage = data.images.Find(x => x.name == myImage);
						if(thisImage != null){ //is there an image?
							myInfo.imageData = thisImage;
						}
						//myInfo.col = GetColor(myCol); //mark this character as bold
						myText = myText.Remove(i,endIndex+1-i);
						iL -= endIndex+1-i;
						checkAgain = true; //check this spot again.
					}
				}
				*/
			//Relative Size ["<s>"]
				else if(myText.IndexOf("<s=") == i){
					int endIndex = myText.IndexOf(">",i);
					if(endIndex != -1){ //is there an end?
						string mySize = myText.Substring(i+3, endIndex-i-3);
						float thisSize;
						if(float.TryParse(mySize, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out thisSize)){ //parse as a float
							myInfo.size = thisSize * size; //set size relative to the one set in inspector!
						}
						//else{
						//	myInfo.size = size; //default
						//}
						myText = myText.Remove(i,endIndex+1-i);
						checkAgain = true; //check this spot again.
					}
				}
				else if(myText.IndexOf("</s>") == i){
					myInfo.size = size; //return to default
					myText = myText.Remove(i,4);
					checkAgain = true;
				}
				
			//Size ["<size>"]
				else if(myText.IndexOf("<size=") == i){ //override the "size" variable directly
					int endIndex = myText.IndexOf(">",i);
					if(endIndex != -1){ //is there an end?
						string mySize = myText.Substring(i+6, endIndex-i-6);
						float thisSize;
						if(float.TryParse(mySize, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out thisSize)){ //parse as a float
							myInfo.size = thisSize; //set size directly!
						}
						//else{
						//	myInfo.size = size; //default
						//}
						myText = myText.Remove(i,endIndex+1-i);
						checkAgain = true; //check this spot again.
					}
				}
				else if(myText.IndexOf("</size>") == i){
					myInfo.size = size; //return to default
					myText = myText.Remove(i,7);
					checkAgain = true;
				}
				
			//Delay ["<d>"]
				else if(myText.IndexOf("<d=") == i){ //overrides autodelays?
					int endIndex = myText.IndexOf(">",i);
					if(endIndex != -1){ //is there an end?
						string myDelay = myText.Substring(i+3, endIndex-i-3);
						STMDelayData thisDelay = data.delays.Find(x => x.name == myDelay);
						if(thisDelay != null){ //is there a delay defined?
							//NOTE: delays get overridden, not added
							myInfo.delayData = thisDelay;
						}else{ //see if it's an integer
							int thisDelay2;
							if(int.TryParse(myDelay, out thisDelay2)){ //parse as a float
								myInfo.delayData = new STMDelayData(thisDelay2);
							}
						}
						myText = myText.Remove(i,endIndex+1-i);
						checkAgain = true; //check this spot again.
					}
				}
				else if(myText.IndexOf("<d>") == i){ //default delay
					STMDelayData thisDelay = data.delays.Find(x => x.name == "default");
					if(thisDelay != null){ //is there a delay defined?
						myInfo.delayData = thisDelay;
					}else{
						Debug.Log("Default delay isn't defined!");
					}
					myText = myText.Remove(i,3);
					checkAgain = true;
				}
			//Timing ["<t>"]
				else if(myText.IndexOf("<t=") == i){
					int endIndex = myText.IndexOf(">",i);
					if(endIndex != -1){ //is there an end?
						string myTiming = myText.Substring(i+3, endIndex-i-3);
						float thisTiming;
						if(float.TryParse(myTiming, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out thisTiming)){ //parse as a float
							if(thisTiming < 0f){
								thisTiming = 0f; //or else it'll cause a loop!
							}
							myInfo.readTime = thisTiming; //set time to be this float
						}
						myText = myText.Remove(i,endIndex+1-i);
						checkAgain = true; //check this spot again.
					}
				}
			//Event ["<e>"]
				else if(myText.IndexOf("<e=") == i){
					int endIndex = myText.IndexOf(">",i);
					if(endIndex != -1){ //is there an end?
						string myEvent = myText.Substring(i+3, endIndex-i-3); //get the event string
						myInfo.ev.Add(myEvent); //remember the event!
						
						myText = myText.Remove(i,endIndex+1-i);
						checkAgain = true; //check this spot again.
					}
				}
			//Repeating Event ["<e2>"]
				else if(myText.IndexOf("<e2=") == i){
					int endIndex = myText.IndexOf(">",i);
					if(endIndex != -1){ //is there an end?
						string myEvent = myText.Substring(i+4, endIndex-i-4); //get the event string
						myInfo.ev2.Add(myEvent); //remember the event!
						
						myText = myText.Remove(i,endIndex+1-i);
						checkAgain = true; //check this spot again.
					}
				}
				else if(myText.IndexOf("</e2>") == i){
					myInfo.ev2.Clear(); //forget all. Kinda janky, but whatever. It'll do for now!
					myText = myText.Remove(i,5);
					checkAgain = true;
				}
			//Voice ["<v>"]
				else if(myText.IndexOf("<v=") == i){
					int endIndex = myText.IndexOf(">",i);
					if(endIndex != -1){ //is there an end?
						string myVoice = myText.Substring(i+3, endIndex-i-3); //get the event string
						STMVoiceData thisVoice = data.voices.Find(x => x.name == myVoice);
						if(thisVoice != null){
							myInfo.voiceData = thisVoice; //remember the event!
						}
						myText = myText.Remove(i,endIndex+1-i);
						checkAgain = true; //check this spot again.
					}
				}
				else if(myText.IndexOf("</v>") == i){
					myInfo.voiceData = null; //forget it!
					myText = myText.Remove(i,4);
					checkAgain = true;
				}
			//Font ["<f>"]
				else if(myText.IndexOf("<f=") == i){ //change font of WHOLE mesh. this one works kinda different
					int endIndex = myText.IndexOf(">",i);
					if(endIndex != -1){ //is there an end?
						string myFont = myText.Substring(i+3, endIndex-i-3); //get the event string
						STMFontData thisFont = data.fonts.Find(x => x.name == myFont);
						if(thisFont != null){
							useFont = thisFont.font; //remember the font in this wayyy
						}
						myText = myText.Remove(i,endIndex+1-i);
						checkAgain = true; //check this spot again.
					}
				}
				else if(myText.IndexOf("</f>") == i){ //theres no real point to this??
					useFont = null; //forget it!
					myText = myText.Remove(i,4);
					checkAgain = true;
				}
			//Backspace ["<x>"]
				/*
				else if(myText.IndexOf("<x>") == i){ //ILLEGAL!!
					myText = myText.Remove(i,3);
					myText = myText.Insert(i,"\b");
					iL -= 3;
					//checkAgain = true; //check this spot again.
				}
				*/
			//Bold ["<b>"]
				else if(myText.IndexOf("<b>") == i){
					myInfo.ch.style = AddStyle(myInfo.ch.style, FontStyle.Bold); //mark this character as bold
					myText = myText.Remove(i,3);
					checkAgain = true; //check this spot again.
				}
				else if(myText.IndexOf("</b>") == i){
					myInfo.ch.style = SubtractStyle(myInfo.ch.style, FontStyle.Bold);
					myText = myText.Remove(i,4);
					checkAgain = true; //do it this way, incase the same tag is used twice in one spot...
				}
			//Italic ["<i>"]
				else if(myText.IndexOf("<i>") == i){
					myInfo.ch.style = AddStyle(myInfo.ch.style, FontStyle.Italic); //mark this character as bold
					myText = myText.Remove(i,3);
					checkAgain = true; //check this spot again.
				}
				else if(myText.IndexOf("</i>") == i){
					myInfo.ch.style = SubtractStyle(myInfo.ch.style, FontStyle.Italic);
					myText = myText.Remove(i,4);
					checkAgain = true;
				}
			//Wave ["<w>"]
				else if(myText.IndexOf("<w=") == i){
					int endIndex = myText.IndexOf(">",i);
					if(endIndex != -1){ //is there an end?
						string myWave = myText.Substring(i+3, endIndex-i-3);
						STMWaveData myWaveData = data.waves.Find(x => x.name == myWave);
						if(myWaveData != null){ //is it a preset?
							myInfo.waveData = myWaveData;
						}
						myText = myText.Remove(i,endIndex+1-i);
						checkAgain = true; //check this spot again.
					}
				}
				else if(myText.IndexOf("<w>") == i){
					STMWaveData thisWave = data.waves.Find(x => x.name == "default");
					if(thisWave != null){
						myInfo.waveData = thisWave; //mark this character as bold
					}else{
						Debug.Log("Default wave isn't defined!");
					}
					myText = myText.Remove(i,3);
					checkAgain = true; //check this spot again.
				}
				else if(myText.IndexOf("</w>") == i){
					myInfo.waveData = null;
					myText = myText.Remove(i,4);
					checkAgain = true;
				}
			//Jitter ["<j>"]
				else if(myText.IndexOf("<j=") == i){
					int endIndex = myText.IndexOf(">",i);
					if(endIndex != -1){ //is there an end?
						string myJitter = myText.Substring(i+3, endIndex-i-3);
						STMJitterData myJitterData = data.jitters.Find(x => x.name == myJitter);
						if(myJitterData != null){ //is it a preset?
							myInfo.jitterData = myJitterData;
						}
						myText = myText.Remove(i,endIndex+1-i);
						checkAgain = true; //check this spot again.
					}
				}
				else if(myText.IndexOf("<j>") == i){
					STMJitterData thisJitter = data.jitters.Find(x => x.name == "default");
					if(thisJitter != null){
						myInfo.jitterData = thisJitter;
					}else{
						Debug.Log("Default jitter isn't defined!");
					}
					myText = myText.Remove(i,3);
					checkAgain = true; //check this spot again.
				}
				else if(myText.IndexOf("</j>") == i){
					myInfo.jitterData = null;
					myText = myText.Remove(i,4);
					checkAgain = true;
				}
			//alignment! ["<a>"]
				else if(myText.IndexOf("<a=") == i){
					int endIndex = myText.IndexOf(">",i);
					if(endIndex != -1){ //is there an end?
						string myAlign = myText.Substring(i+3, endIndex-i-3).ToLower(); //get the event string, lowercase
						if(myAlign == "left"){
							myInfo.alignment = Alignment.Left;
						}
						if(myAlign == "right"){
							myInfo.alignment = Alignment.Right;
						}
						if(myAlign == "center" || myAlign == "centre"){
							myInfo.alignment = Alignment.Center;
						}
						if(myAlign == "just" || myAlign == "justify" || myAlign == "justified"){
							myInfo.alignment = Alignment.Justified;
						}
						if(myAlign == "just2" || myAlign == "justify2" || myAlign == "justified2"){
							myInfo.alignment = Alignment.ForceJustified;
						}
						myText = myText.Remove(i,endIndex+1-i);
						checkAgain = true; //check this spot again.
					}
				}
				else if(myText.IndexOf("</a>") == i){
					myInfo.alignment = alignment; //return to default for mesh
					myText = myText.Remove(i,4);
					checkAgain = true;
				}
			}
			if(info.Count - 1 == i){
				info[i] = new TextInfo(myInfo); //update older one
				//Debug.Log("Updating older character " + myText[i].ToString() + " to be " + info[i].style);
			}else{
				info.Add(new TextInfo(myInfo) ); //add new HAS TO BE NEW OR ELSE IT JUST REFERENCES
			}
			if(checkAgain){
				i--;
			}else{ //stuff that gets reset!! single-use tags.
				myInfo.delayData = new STMDelayData(0);// reset
				myInfo.imageData = null;
				myInfo.ev.Clear();
				myInfo.readTime = -1f;
			}
		}
		return myText;
	}
	/*
	//These don't really work in the way you'd want, unfortunately.
	void RequestChars(string chars, int myQuality, FontStyle myStyle){
		switch(UseThisFont.dynamic){
			case false:
				//UseThisFont.RequestCharactersInTexture(chars);
				break;
			default:
				UseThisFont.RequestCharactersInTexture(chars, myQuality, myStyle);
				break;
		}
	}
	bool GetCharInfo(char myChar, out CharacterInfo returnThis, int myQuality, FontStyle myStyle){
		switch(UseThisFont.dynamic){
			case false:
				return UseThisFont.GetCharacterInfo(myChar, out returnThis, 0, FontStyle.Normal);
			default:
				return UseThisFont.GetCharacterInfo(myChar, out returnThis, myQuality, myStyle);
		}
	}
	*/
	void RebuildTextInfo(){ 
		drawText = ParseText(text); //remove parsing junk (<col>, <b>), and fill textinfo again
		Vector3 pos = Vector3.zero; //keep track of where to place this text
		lineBreaks.Clear(); //index of line break characters, for centering

		hyphenedText = string.Copy(drawText);
		if(AutoWrap > 0f){ //use autowrap?
			for(int i=0, iL=hyphenedText.Length; i<iL; i++){ //first, get character info...
				UseThisFont.RequestCharactersInTexture(hyphenedText[i].ToString(), quality, info[i].ch.style); //request characters to draw
				CharacterInfo ch;
				if(UseThisFont.GetCharacterInfo(hyphenedText[i], out ch, quality, info[i].ch.style)){ //does this character exist?
					info[i].ch = ch; //remember character info!
				}//else, don't draw anything! this charcter won't have info
			}
			CharacterInfo spaceCh;
			UseThisFont.GetCharacterInfo(' ', out spaceCh, quality, FontStyle.Normal); //get data for space
			CharacterInfo hyphenCh;
			UseThisFont.RequestCharactersInTexture("-", quality, FontStyle.Normal); //still call this, for when you're inserting hyphens anyway
			UseThisFont.GetCharacterInfo('-', out hyphenCh, quality, FontStyle.Normal);
			float lineWidth = 0f;
			int indexOffset = 0;
			for(int i=0, iL=hyphenedText.Length; i<iL; i++){
				//float hyphenWidth = hyphenCh.advance * (info[i].size / info[i].ch.size); //have hyphen size match last character in row
				if(hyphenedText[i] == '\n'){ //is this character a line break?
					lineWidth = 0f; //new line, reset
				}else if(hyphenedText[i] == '\t'){ // linebreak with a tab...
					lineWidth += 0.5f * tabSize * info[i].size;
				}else{
					lineWidth += (info[i].ch.advance + (characterSpacing * info[i].size)) * (info[i].size / info[i].ch.size);
				}
				//TODO: this still lets natural hyphens go over for some reason? ah well 2016-09-23: DOES TTHIS STILL HAPPEN??
				//float wrapLimit = breakText ? myWrap - hyphenWidth : myWrap;//hmm... it's still letting natural hyphens go over, with autowrap disabled
				//float wrapLimit = myWrap - hyphenWidth > hyphenWidth ? myWrap - hyphenWidth : myWrap;
				if(lineWidth > AutoWrap){
					int myBreak = hyphenedText.LastIndexOf(' ',i); //safe spot to do a line break, can be a hyphen
					int myHyphenBreak = hyphenedText.LastIndexOf('-',i);
					int myTabBreak = hyphenedText.LastIndexOf('\t',i); //can break at a tab, too!
					int myActualBreak = Mathf.Max(new int[]{myBreak, myHyphenBreak, myTabBreak}); //get the largest of all 3
					int lastBreak = hyphenedText.LastIndexOf('\n',i); //last place a ine break happened
					if(myActualBreak != -1 && myActualBreak > lastBreak){ //is there a space to do a line break? (and no hyphens...)
						hyphenedText = hyphenedText.Remove(myActualBreak, 1); //this is wrong, don't remove the space ooops
						hyphenedText = hyphenedText.Insert(myActualBreak, '\n'.ToString());
						i = myActualBreak; //go back
						lineWidth = 0f; //reset
					}else{ //no previous space?
						
						if(breakText && i != 0){ //split it here! but not if it's the first character
							if(insertHyphens){
								hyphenedText = hyphenedText.Insert(i, "-\n");
								//Debug.Log("This needs a hyphen: " + hyphenedText);
								info.Insert(i,new TextInfo(info[i - indexOffset], spaceCh));
								info.Insert(i,new TextInfo(info[i - indexOffset], spaceCh));
								if(AutoWrap < info[i - indexOffset].size){ //otherwise, it'll loop foreverrr
									i += 2;
								}
								iL += 2;
								indexOffset += 2;
							}else{
								hyphenedText = hyphenedText.Insert(i, "\n");
								info.Insert(i,new TextInfo(info[i - indexOffset], spaceCh));
								if(AutoWrap < info[i - indexOffset].size){ //otherwise, it'll loop foreverrr
									i += 1;
								}
								iL += 1;
								indexOffset += 1;
							}
							lineWidth = 0f; //reset
						}
						
					}//no need to check for following space, it'll come up anyway
				}
			}
		}else{
			for(int i=0, iL=hyphenedText.Length; i<iL; i++){ //from character info...
				//vvvv very important
				UseThisFont.RequestCharactersInTexture(hyphenedText[i].ToString(), quality, info[i].ch.style); //request characters to draw
				//font.RequestCharactersInTexture(System.Text.Encoding.UTF8.GetString(System.BitConverter.GetBytes(info[i].ch.index)), quality, info[i].ch.style); //request characters to draw
			}
		}
		//get position
		for(int i=0, iL=hyphenedText.Length; i<iL; i++){ //for each character to draw...
			CharacterInfo ch;
			if(UseThisFont.GetCharacterInfo(hyphenedText[i], out ch, quality, info[i].ch.style)){ //does this character exist?
				info[i].ch = ch; //remember character info!
			}//else, don't draw anything! this charcter won't have info
			info[i].pos = pos; //save this position data!
			if(hyphenedText[i] == '\n'){//drop a line
				if(i == 0){ //first character is a line break?
					lineBreaks.Add(0);
				}else{
					lineBreaks.Add(i-1);
				}
				pos = new Vector3(0, pos.y ,0); //assume left-orintated for now. go back to start of row
				pos -= new Vector3(0, quality * lineSpacing, 0) * (size / quality); //drop down
			}
			else if(iL - 1 == i){ //last character, and not a line break?
				lineBreaks.Add(i);
			}
			else if(hyphenedText[i] == '\t'){//tab?
				pos += new Vector3(quality * 0.5f * tabSize, 0,0) * (info[i].size / quality);
			}
			else{// Advance character position
				pos += new Vector3(info[i].ch.advance + (characterSpacing * info[i].size), 0,0) * (info[i].size / quality);
			}//remember position data for whatever
		}
		ApplyOffsetDataToTextInfo(); //just to clean up this very long function...
		UpdateRTLDrawOrder();
		ApplyTimingDataToTextInfo();
		ApplyUnreadTimingDataToTextInfo();
	}
	void ApplyOffsetDataToTextInfo(){ //this works!!! ahhhh!!!
		float[] allMaxes = new float[lineBreaks.Count];
		for(int i=0, iL=lineBreaks.Count; i<iL; i++){
			//get max x data from this line
			allMaxes[i] = info[lineBreaks[i]].TopRightVert.x;
			if(float.IsNaN(allMaxes[i])){
				allMaxes[i] = 0f; //for rows that are just linebreaks! take THAT
			}
		}
		float maxX = Mathf.Max(allMaxes);
		Vector3 offset = -baseOffset; //apply anchor offset
		if(uiMode){
			//offset = Vector3.zero;
			//ALIGN TO WHATEVER UI BOX HERE!!!
			RectTransform tr = t as RectTransform; //(RectTransform(t)) also works!
			Vector2 xtraOffset = Vector2.zero;
			//TODO: during play mode, this doesn't update right...
			switch(anchor){
				case TextAnchor.UpperLeft: xtraOffset = new Vector2(tr.rect.xMin, tr.rect.yMax); break;
				case TextAnchor.UpperCenter: xtraOffset = new Vector2((tr.rect.xMin + tr.rect.xMax) / 2f, tr.rect.yMax); break;
				case TextAnchor.UpperRight: xtraOffset = new Vector2(tr.rect.xMax, tr.rect.yMax); break;
				case TextAnchor.MiddleLeft: xtraOffset = new Vector2(tr.rect.xMin, (tr.rect.yMin + tr.rect.yMax) / 2f); break;
				case TextAnchor.MiddleCenter: xtraOffset = new Vector2((tr.rect.xMin + tr.rect.xMax) / 2f, (tr.rect.yMin + tr.rect.yMax) / 2f); break;
				case TextAnchor.MiddleRight: xtraOffset = new Vector2(tr.rect.xMax, (tr.rect.yMin + tr.rect.yMax) / 2f); break;
				case TextAnchor.LowerLeft: xtraOffset = new Vector2(tr.rect.xMin, tr.rect.yMin); break;
				case TextAnchor.LowerCenter: xtraOffset = new Vector2((tr.rect.xMin + tr.rect.xMax) / 2f, tr.rect.yMin); break;
				case TextAnchor.LowerRight: xtraOffset = new Vector2(tr.rect.xMax, tr.rect.yMin); break;
			}
			offset -= new Vector3(xtraOffset.x, xtraOffset.y, 0);
		}
		int rowStart = 0; //index of where this row starts
		for(int i=0, iL=lineBreaks.Count; i<iL; i++){ //for each line of text //2016-06-09 new alignment script
			Vector3 myOffsetCenter = new Vector3((maxX - info[lineBreaks[i]].TopRightVert.x) * 0.5f, 0f, 0f); //find offset for this row
			Vector3 myOffsetRight = new Vector3(maxX - info[lineBreaks[i]].TopRightVert.x, 0f, 0f);
			
			if(AutoWrap > 0f){
				myOffsetCenter += new Vector3((AutoWrap * 0.5f) - (maxX * 0.5f),0f,0f);
				myOffsetRight += new Vector3(AutoWrap - maxX,0f,0f);
			}
			//float emptySpace = myOffsetRight.x;
			int spaceCount = 0;
			for(int j=rowStart, jL=lineBreaks[i]+1; j<jL; j++){ //see how many spaces there are
				if(hyphenedText[j] == ' '){
					spaceCount++;
				}
			}
			Vector3 justifySpace = spaceCount > 0 ? new Vector3(myOffsetRight.x / (float)spaceCount, 0f, 0f) : Vector3.zero;
			int passedSpaces = 0;
			for(int j=rowStart, jL=lineBreaks[i]+1; j<jL; j++){//if this character is in the range...
				if(hyphenedText[j] == ' '){
					passedSpaces++;
				}
				switch(info[j].alignment){
					case Alignment.Center:
						info[j].pos += myOffsetCenter;
						break;
					case Alignment.Right:
						info[j].pos += myOffsetRight;
						break;
					case Alignment.Justified:
						if(jL != hyphenedText.Length && drawText[jL - (hyphenedText.Length - drawText.Length)] != '\n'){ //not the very last row, or a row with a linebreak?
							info[j].pos += justifySpace * passedSpaces;
						}
						break;
					case Alignment.ForceJustified:
						info[j].pos += justifySpace * passedSpaces; //justify no matter what
						break;
				}
			}
			rowStart = lineBreaks[i]+1;
			//max x in mesh
			//minus max x in this row
			//add half the difference to all verts in this row
		}
		float upperY = size; //not neccesary but w/e
		float lowerY = upperY - (((quality * (lineBreaks.Count - 1) * lineSpacing) + quality) * (size / quality)); //ok this works now
		if(autoWrap > 0f){//anchor to box instead of text
			switch(anchor){ //yep, just use autowrap limit
				case TextAnchor.UpperLeft: offset += new Vector3(0, upperY, 0); break;
				case TextAnchor.UpperCenter: offset += new Vector3(autoWrap * 0.5f, upperY, 0); break;
				case TextAnchor.UpperRight: offset += new Vector3(autoWrap, upperY, 0); break;
				case TextAnchor.MiddleLeft: offset += new Vector3(0, (upperY + lowerY) * 0.5f, 0); break;
				case TextAnchor.MiddleCenter: offset += new Vector3(autoWrap * 0.5f, (upperY + lowerY) * 0.5f, 0); break;
				case TextAnchor.MiddleRight: offset += new Vector3(autoWrap, (upperY + lowerY) * 0.5f, 0); break;
				case TextAnchor.LowerLeft: offset += new Vector3(0, lowerY, 0); break;
				case TextAnchor.LowerCenter: offset += new Vector3(autoWrap * 0.5f, lowerY, 0); break;
				case TextAnchor.LowerRight: offset += new Vector3(autoWrap, lowerY, 0); break;
			}
		}else{
			switch(anchor){
				case TextAnchor.UpperLeft: offset += new Vector3(0, upperY, 0); break;
				case TextAnchor.UpperCenter: offset += new Vector3(maxX * 0.5f, upperY, 0); break;
				case TextAnchor.UpperRight: offset += new Vector3(maxX, upperY, 0); break;
				case TextAnchor.MiddleLeft: offset += new Vector3(0, (upperY + lowerY) * 0.5f, 0); break;
				case TextAnchor.MiddleCenter: offset += new Vector3(maxX * 0.5f, (upperY + lowerY) * 0.5f, 0); break;
				case TextAnchor.MiddleRight: offset += new Vector3(maxX, (upperY + lowerY) * 0.5f, 0); break;
				case TextAnchor.LowerLeft: offset += new Vector3(0, lowerY, 0); break;
				case TextAnchor.LowerCenter: offset += new Vector3(maxX * 0.5f, lowerY, 0); break;
				case TextAnchor.LowerRight: offset += new Vector3(maxX, lowerY, 0); break;
			}
		}
		for(int i=0, iL=info.Count; i<iL; i++){ //apply all offsets
			info[i].pos -= offset;
		}
		if(!uiMode){//these don't matter in UI mode...
			topLeftBounds = Vector3.Scale(new Vector3(offset.x, offset.y - upperY, offset.z), t.lossyScale); //scale to show proper bunds even when parent is scaled weird
			bottomRightBounds = Vector3.Scale(new Vector3(offset.x - autoWrap, offset.y - lowerY, offset.z), t.lossyScale);
		}
	}

	private int[] drawOrderRTL;
	void UpdateRTLDrawOrder (){ //update the RTL draw info, if needed
		if(drawOrder == DrawOrder.RightToLeft || undrawOrder == DrawOrder.RightToLeft){ //actually calculate?
			drawOrderRTL = new int[hyphenedText.Length];
			int currentLine = 0;
			for(int i=0, iL=hyphenedText.Length; i<iL; i++){
				int lastEnd = currentLine > 0 ? lineBreaks[currentLine-1] + 1 : 0;
				drawOrderRTL[i] = -i + lineBreaks[currentLine] + lastEnd;
				if(lineBreaks[currentLine] == i){ //this was the last character in this row
					//Debug.Log("The end of this line was: " + lineBreaks[currentLine]);
					currentLine++;
				}
			}
		}
	}
	void ApplyTimingDataToTextInfo(){
		float currentTiming = 0f;
		float furthestPoint = 0f;
		bool needsToRead = false;
		for(int i=0, iL=hyphenedText.Length; i<iL; i++){
			int myIndex = i;
			switch(drawOrder){
				case DrawOrder.RightToLeft: myIndex = drawOrderRTL[i]; break;
				case DrawOrder.ReverseLTR: myIndex = -i + iL - 1; break;
			}
			STMVoiceData voice = info[myIndex].voiceData; //grab voice data from mesh default, or info's voice
			float myReadDelay = voice != null ? voice.useReadDelay ? voice.readDelay : readDelay : readDelay;
			string myDrawAnimationName = voice != null && voice.useDrawAnim ? voice.drawAnimName : drawAnimName;
			STMDrawAnimData myDrawAnim = data.drawAnims.Find(x => x.name == myDrawAnimationName);

			if(myDrawAnim == null){
				myDrawAnim = data.drawAnims[0];
			}
			if(myReadDelay > 0f){
				needsToRead = true;
			}

			STMDelayData delay = info[myIndex].delayData; //additional delay on this letter
			float additionalDelay = delay != null ? delay.count : 0f;
			//get the time it'll be drawn at...
			//Debug.Log("Letter " + hyphenedText[i] + "'s timing BEFORE is: " + currentTiming);
			if(info[myIndex].readTime < 0f){ //if a time hasn't been set for this letter yet
				switch(drawOrder){
					case DrawOrder.AllAtOnce:
						info[i].readTime = currentTiming;
						break;
					case DrawOrder.Random:
						info[i].readTime = Random.Range(0f,readDelay);
						break;
					case DrawOrder.OneWordAtATime:
						if(hyphenedText[i] == ' ' || hyphenedText[i] == '\n' || hyphenedText[i] == '\t' || hyphenedText[i] == '-'){ //only advance timing on a space, line break, or tab, or hyphen!
							currentTiming += i == 0 ? additionalDelay * myReadDelay : myReadDelay + (additionalDelay * myReadDelay);
						}
						info[i].readTime = currentTiming;
						break;	
					case DrawOrder.RightToLeft:
						info[myIndex].readTime = currentTiming; //reverse order!
						currentTiming += myIndex == 0 ? additionalDelay * myReadDelay : myReadDelay + (additionalDelay * myReadDelay);
						break;
					case DrawOrder.ReverseLTR:
						currentTiming += i == 0 ? additionalDelay * myReadDelay : myReadDelay + (additionalDelay * myReadDelay);
						info[myIndex].readTime = currentTiming;
						break;
					default: //Left To Right
						//dont add extra for first character
						currentTiming += i == 0 ? additionalDelay * myReadDelay : myReadDelay + (additionalDelay * myReadDelay);
						info[i].readTime = currentTiming;
						break;
				}
			}else{
				currentTiming = info[myIndex].readTime; //pick up from here
			}
			//Debug.Log("Letter " + hyphenedText[i] + "'s timing AFTER is: " + currentTiming);

			info[myIndex].drawAnimData = myDrawAnim; //assign draw animation data
			float maxAnimTime = Mathf.Max(myDrawAnim.animTime, myDrawAnim.fadeTime);
			furthestPoint = Mathf.Max(info[myIndex].readTime + maxAnimTime, furthestPoint);
		}
		totalReadTime = furthestPoint; //save it!
		callReadFunction = needsToRead;
	}
	void ApplyUnreadTimingDataToTextInfo(){
		float currentTiming = 0f;
		float furthestPoint = 0f;
		STMDrawAnimData myDrawAnim = UndrawAnim; //store undrawing animation since it'll be called multiple times
		
		for(int i=0, iL=hyphenedText.Length; i<iL; i++){
			int myIndex = i;
			switch(undrawOrder){
				case DrawOrder.RightToLeft: myIndex = drawOrderRTL[i]; break;
				case DrawOrder.ReverseLTR: myIndex = -i + iL - 1; break;
			}
			switch(undrawOrder){
				case DrawOrder.AllAtOnce:
					info[i].unreadTime = currentTiming;
					break;
				case DrawOrder.Random:
					info[i].unreadTime = Random.Range(0f,unreadDelay);
					break;
				case DrawOrder.OneWordAtATime:
					info[i].unreadTime = currentTiming;
					if(hyphenedText[i] == ' ' || hyphenedText[i] == '\n' || hyphenedText[i] == '\t' || hyphenedText[i] == '-'){ //only advance timing on a space, line break, or tab, or hyphen!
						currentTiming += unreadDelay;
					}
					break;	
				case DrawOrder.RightToLeft:
					currentTiming += unreadDelay;
					info[myIndex].unreadTime = currentTiming;
					break;
				case DrawOrder.ReverseLTR:
					currentTiming += unreadDelay;
					info[myIndex].unreadTime = currentTiming;
					break;
				default:
					info[i].unreadTime = currentTiming;
					currentTiming += unreadDelay; //<<< this is applied in opposide order as normal read info
					break;
			}
			float maxAnimTime = Mathf.Max(myDrawAnim.animTime, myDrawAnim.fadeTime);
			furthestPoint = Mathf.Max(info[myIndex].unreadTime + maxAnimTime, furthestPoint);
		}
		totalUnreadTime = furthestPoint; //save it!
	}
	
	Vector3 WaveValue(TextInfo myInfo, STMWaveControl wave){ //multiply offset by 6 because ??????? seems to work
		//float currentTime = GetTime;
		float myTime = myInfo.waveData.animateFromTimeDrawn ? GetTime - timeDrawn - myInfo.readTime : GetTime;
		return new Vector3(wave.curveX.Evaluate(((myTime * wave.speed.x) + wave.offset * 6f) + (myInfo.pos.x * wave.density.x / myInfo.size)) * wave.strength.x * myInfo.size, 
							wave.curveY.Evaluate(((myTime * wave.speed.y) + wave.offset * 6f) + (myInfo.pos.x * wave.density.y / myInfo.size)) * wave.strength.y * myInfo.size, 
							wave.curveZ.Evaluate(((myTime * wave.speed.z) + wave.offset * 6f) + (myInfo.pos.x * wave.density.z / myInfo.size)) * wave.strength.z * myInfo.size); //multiply by universal size;
	}

	Vector3 JitterValue(TextInfo myInfo, STMJitterData jit){
		Vector3 myJit = Vector3.zero;
		float myTime = currentReadTime - myInfo.readTime; //time that's different for each letter
		switch(jit.perlin){
			case true:
				myJit = new Vector3( //weird perlin jitter... could use some work, but it's a jitter effect that scales with time
						jit.distanceOverTime.Evaluate(myTime / jit.distanceOverTimeMulti) * (jit.distance.Evaluate(Mathf.PerlinNoise(jit.perlinTimeMulti*myTime+myInfo.pos.x, 0f)) * jit.amount * (Mathf.PerlinNoise(jit.perlinTimeMulti*myTime+myInfo.pos.x, 0f) - 0.5f)),
						jit.distanceOverTime.Evaluate(myTime / jit.distanceOverTimeMulti) * (jit.distance.Evaluate(Mathf.PerlinNoise(jit.perlinTimeMulti*myTime+myInfo.pos.x+30f, 0f)) * jit.amount * (Mathf.PerlinNoise(jit.perlinTimeMulti*myTime+myInfo.pos.x+30f, 0f) - 0.5f)),
						0) * myInfo.size; //multiply by universal size
				break;
			default:
				myJit = new Vector3(//ditance over time... so jitters can also only happen AS a letter is drawn.
						jit.distanceOverTime.Evaluate(myTime / jit.distanceOverTimeMulti) * jit.distance.Evaluate(Random.value) * jit.amount * (Random.value - 0.5f),//make jit follow curve
						jit.distanceOverTime.Evaluate(myTime / jit.distanceOverTimeMulti) * jit.distance.Evaluate(Random.value) * jit.amount * (Random.value - 0.5f),
						0) * myInfo.size; //multiply by universal size
				break;
		}
		return myJit;
	}
	Mesh CreateMesh(){
		Mesh mesh = new Mesh();
		areWeAnimating = false;
		if(hyphenedText.Length > 0){ //bother to draw it?
			// Generate a mesh for the characters we want to print.
			endVerts = new Vector3[hyphenedText.Length * 4];
			int[] triangles = new int[hyphenedText.Length * 6];
			Vector2[] uv = new Vector2[hyphenedText.Length * 4];
			Vector2[] uv2 = new Vector2[hyphenedText.Length * 4]; //overlay images
			endCol32 = new Color32[hyphenedText.Length * 4];

			List<SubMeshData> subMeshes = new List<SubMeshData>();

			for(int i=0, iL=hyphenedText.Length; i<iL; i++){
			//Vertex data:
			//animated stuffffff
				Vector3 jitterValue = Vector3.zero;
				if(info[i].jitterData != null && !data.disableAnimatedText && !disableAnimatedText){ //just dont jitter if animating text is overridden
					areWeAnimating = true;
					jitterValue = JitterValue(info[i], info[i].jitterData); //get jitter data
				}
				Vector3 waveValue = Vector3.zero; //universal
				Vector3 waveValueTopLeft = Vector3.zero;
				Vector3 waveValueTopRight = Vector3.zero;
				Vector3 waveValueBottomRight = Vector3.zero;
				Vector3 waveValueBottomLeft = Vector3.zero;
				if(info[i].waveData != null && info[i].size != 0){
					areWeAnimating = true;
					waveValue = WaveValue(info[i], info[i].waveData.main);
					if(info[i].waveData.individualVertexControl){
						waveValueTopLeft = WaveValue(info[i], info[i].waveData.topLeft);
						waveValueTopRight = WaveValue(info[i], info[i].waveData.topRight);
						waveValueBottomRight = WaveValue(info[i], info[i].waveData.bottomRight);
						waveValueBottomLeft = WaveValue(info[i], info[i].waveData.bottomLeft);
					}
				}
				//if text isn't different, you don't need to update UVs, or triangles
				//only need to update vertices of animated text
				//only need to update color of text w/ animated colors

				endVerts[4*i + 0] = (info[i].TopLeftVert + jitterValue + waveValueTopLeft + waveValue);
				endVerts[4*i + 1] = (info[i].TopRightVert + jitterValue + waveValueTopRight + waveValue);
				endVerts[4*i + 2] = (info[i].BottomRightVert + jitterValue + waveValueBottomRight + waveValue);
				endVerts[4*i + 3] = (info[i].BottomLeftVert + jitterValue + waveValueBottomLeft + waveValue);
				
			//Assign text UVs
				uv[4*i + 0] = info[i].ch.uvTopLeft;
				uv[4*i + 1] = info[i].ch.uvTopRight;
				uv[4*i + 2] = info[i].ch.uvBottomRight;
				uv[4*i + 3] = info[i].ch.uvBottomLeft;
				
			//Scrolling Textures:
				//make sure last character isn't a tab, space, or line break.
				if(info[i].textureData != null && (i != iL-1 || (i == iL-1 && info[i].TopRightVert != Vector3.zero))){ //not last character nothing!
					if(info[i].textureData.scrollSpeed != Vector2.zero){
						areWeAnimating = true; //update this every frame
					}
					Vector2 uvOffset = new Vector2(GetTime * info[i].textureData.scrollSpeed.x, GetTime * info[i].textureData.scrollSpeed.y); //animated offset
					//Vector2 uvMulti = Vector2.one;

					//NOTE TO ME TOMORROW:
					/*
					working on making the new texture data stuff work with this
					kinda hard to get uhhh
					scale with text kinda scales weird but w/e
					relative to letter should just use square UVs over the letters, no stretching to match size
					offset is easy, just subtract the vector2 data. already done
					*/
					
					float uv2Scale = 1f;
					if(info[i].textureData.scaleWithText){
						uv2Scale = 1f / info[i].size;
					}
					
					if(info[i].textureData.relativeToLetter){//keep uvs relative to each letter?
						//just draw texture as a square
						uv2[4*i + 0] = uv2Scale * ((Vector2)endVerts[4*i + 0] - (Vector2)info[i].pos) + uvOffset - info[i].textureData.offset;
						uv2[4*i + 1] = uv2Scale * ((Vector2)endVerts[4*i + 1] - (Vector2)info[i].pos) + uvOffset - info[i].textureData.offset;
						uv2[4*i + 2] = uv2Scale * ((Vector2)endVerts[4*i + 2] - (Vector2)info[i].pos) + uvOffset - info[i].textureData.offset;
						uv2[4*i + 3] = uv2Scale * ((Vector2)endVerts[4*i + 3] - (Vector2)info[i].pos) + uvOffset - info[i].textureData.offset;
					}else{
						uv2[4*i + 0] = uv2Scale * (Vector2)endVerts[4*i + 0] + uvOffset - info[i].textureData.offset;
						uv2[4*i + 1] = uv2Scale * (Vector2)endVerts[4*i + 1] + uvOffset - info[i].textureData.offset;
						uv2[4*i + 2] = uv2Scale * (Vector2)endVerts[4*i + 2] + uvOffset - info[i].textureData.offset;
						uv2[4*i + 3] = uv2Scale * (Vector2)endVerts[4*i + 3] + uvOffset - info[i].textureData.offset;
					}
					//does this texture already have a submesh?
					SubMeshData thisSubMesh = subMeshes.Find(x => x.name == info[i].textureData.name); //is there a submest for this texture yet?
					if(thisSubMesh == null){ //doesn't exist yet??
						thisSubMesh = new SubMeshData(info[i].textureData.name);
						subMeshes.Add(thisSubMesh);
					}
					thisSubMesh.tris.Add(4*i + 0);
					thisSubMesh.tris.Add(4*i + 1);
					thisSubMesh.tris.Add(4*i + 2);
					thisSubMesh.tris.Add(4*i + 0);
					thisSubMesh.tris.Add(4*i + 2);
					thisSubMesh.tris.Add(4*i + 3);
				}else{
					triangles[6*i + 0] = 4*i + 0;
					triangles[6*i + 1] = 4*i + 1;
					triangles[6*i + 2] = 4*i + 2;

					triangles[6*i + 3] = 4*i + 0;
					triangles[6*i + 4] = 4*i + 2;
					triangles[6*i + 5] = 4*i + 3;
				}

			//Color data:
				if(info[i].gradientData != null){ //gradient speed + gradient spread
					if(info[i].gradientData.scrollSpeed != 0){
						areWeAnimating = true;
					}
					switch(info[i].gradientData.direction){
						case STMGradientData.GradientDirection.Vertical:
							switch(info[i].gradientData.smoothGradient){
								case false: //hard gradient
									endCol32[4*i + 0] = info[i].gradientData.gradient.Evaluate(Mathf.Repeat((GetTime * info[i].gradientData.scrollSpeed) + (info[i].pos.y * info[i].gradientData.gradientSpread / info[i].size),1f));
									endCol32[4*i + 1] = info[i].gradientData.gradient.Evaluate(Mathf.Repeat((GetTime * info[i].gradientData.scrollSpeed) + (info[i].pos.y * info[i].gradientData.gradientSpread / info[i].size),1f));
									endCol32[4*i + 2] = info[i].gradientData.gradient.Evaluate(Mathf.Repeat((GetTime * info[i].gradientData.scrollSpeed) + (info[i].pos.y * info[i].gradientData.gradientSpread / info[i].size),1f));
									endCol32[4*i + 3] = info[i].gradientData.gradient.Evaluate(Mathf.Repeat((GetTime * info[i].gradientData.scrollSpeed) + (info[i].pos.y * info[i].gradientData.gradientSpread / info[i].size),1f));
									break;
								default:
									endCol32[4*i + 0] = info[i].gradientData.gradient.Evaluate(Mathf.Repeat((GetTime * info[i].gradientData.scrollSpeed) + ((info[i].pos.y + info[i].size) * info[i].gradientData.gradientSpread / info[i].size),1f));
									endCol32[4*i + 1] = info[i].gradientData.gradient.Evaluate(Mathf.Repeat((GetTime * info[i].gradientData.scrollSpeed) + ((info[i].pos.y + info[i].size) * info[i].gradientData.gradientSpread / info[i].size),1f));
									endCol32[4*i + 2] = info[i].gradientData.gradient.Evaluate(Mathf.Repeat((GetTime * info[i].gradientData.scrollSpeed) + (info[i].pos.y * info[i].gradientData.gradientSpread / info[i].size),1f));
									endCol32[4*i + 3] = info[i].gradientData.gradient.Evaluate(Mathf.Repeat((GetTime * info[i].gradientData.scrollSpeed) + (info[i].pos.y * info[i].gradientData.gradientSpread / info[i].size),1f));
									break;
							}
							break;
						default: //horizontal
							switch(info[i].gradientData.smoothGradient){
								case false:
									endCol32[4*i + 0] = info[i].gradientData.gradient.Evaluate(Mathf.Repeat((GetTime * info[i].gradientData.scrollSpeed) + (endVerts[4*i + 0].x * info[i].gradientData.gradientSpread / info[i].size),1f)); //this works!
									endCol32[4*i + 1] = info[i].gradientData.gradient.Evaluate(Mathf.Repeat((GetTime * info[i].gradientData.scrollSpeed) + (endVerts[4*i + 0].x * info[i].gradientData.gradientSpread / info[i].size),1f));
									endCol32[4*i + 2] = info[i].gradientData.gradient.Evaluate(Mathf.Repeat((GetTime * info[i].gradientData.scrollSpeed) + (endVerts[4*i + 0].x * info[i].gradientData.gradientSpread / info[i].size),1f));
									endCol32[4*i + 3] = info[i].gradientData.gradient.Evaluate(Mathf.Repeat((GetTime * info[i].gradientData.scrollSpeed) + (endVerts[4*i + 0].x * info[i].gradientData.gradientSpread / info[i].size),1f));
									break;
								default://smooth gradient
									endCol32[4*i + 0] = info[i].gradientData.gradient.Evaluate(Mathf.Repeat((GetTime * info[i].gradientData.scrollSpeed) + (endVerts[4*i + 0].x * info[i].gradientData.gradientSpread / info[i].size),1f)); //this works!
									endCol32[4*i + 1] = info[i].gradientData.gradient.Evaluate(Mathf.Repeat((GetTime * info[i].gradientData.scrollSpeed) + (endVerts[4*i + 1].x * info[i].gradientData.gradientSpread / info[i].size),1f));
									endCol32[4*i + 2] = info[i].gradientData.gradient.Evaluate(Mathf.Repeat((GetTime * info[i].gradientData.scrollSpeed) + (endVerts[4*i + 2].x * info[i].gradientData.gradientSpread / info[i].size),1f));
									endCol32[4*i + 3] = info[i].gradientData.gradient.Evaluate(Mathf.Repeat((GetTime * info[i].gradientData.scrollSpeed) + (endVerts[4*i + 3].x * info[i].gradientData.gradientSpread / info[i].size),1f));
									break;
							}
							break;
					}
				}else{
					endCol32[4*i + 0] = info[i].colorData.color;
					endCol32[4*i + 1] = info[i].colorData.color;
					endCol32[4*i + 2] = info[i].colorData.color;
					endCol32[4*i + 3] = info[i].colorData.color;
				}
			}
			//If you want to modify vertices (curve, offset, etc) you can do it directly, here?
			//ApplyCurveToVertices(endVerts);
			if(modifyVertices){
				Vector3[] middles = new Vector3[hyphenedText.Length]; //create an array with the middle of each letter
				Vector3[] positions = new Vector3[hyphenedText.Length];
				for(int i=0, iL=hyphenedText.Length; i<iL; i++){
					middles[i] = info[i].Middle;
					positions[i] = info[i].pos;
				}
				vertexMod.Invoke(endVerts, middles, positions); //modify end verts externally
			}
			mesh.vertices = endVerts;
			mesh.uv = uv;
			mesh.uv2 = uv2; //use 2nd texture...
			mesh.colors32 = endCol32;
			//set triangles
			if(subMeshes.Count != 0){ //diff count?? update it!
				mesh.subMeshCount = subMeshes.Count + 1;
				if(uiMode){
					c.materialCount = mesh.subMeshCount;
					Material[] uiMats = RendererMaterials(subMeshes);
					for(int j=0, jL=uiMats.Length; j<jL; j++){
						c.SetMaterial(uiMats[j],j);
					}
				}else{
					r.sharedMaterials = RendererMaterials(subMeshes); //update!
				}
				mesh.SetTriangles(triangles, 0);
				for(int i=0, iL=subMeshes.Count; i<iL; i++){
					mesh.SetTriangles(subMeshes[i].tris, i + 1);
				}
			}else{
				mesh.subMeshCount = 0;
				if(uiMode){
					c.materialCount = 1;
				}else{
					if(r.sharedMaterials.Length > 1){ //more than 1 on render mat?
						r.sharedMaterials = RendererMaterials(subMeshes); //cull old textures!
					}
				}
				mesh.triangles = triangles;
			}
			//TODO: assign normals by hand instead of using this. but really, whatever
			//mesh.RecalculateNormals(); //2016-07-05 i dont think I need to do this?
		}
		if(data.disableAnimatedText || disableAnimatedText){
			areWeAnimating = false; //override constant updates
		}

		return mesh;
	}
	Material[] RendererMaterials(List<SubMeshData> subMeshes){
		Material[] newMats = new Material[subMeshes.Count + 1]; //spots for each submesh to use, plus default mat
		if(uiMode){
			for(int j=0, jL=c.materialCount; j<jL; j++){ //for each material CURRENTLY on the renderer...
				if(j <= subMeshes.Count){ //already there?
					newMats[j] = c.GetMaterial(j); //pass on this mat
				}else{
					DestroyImmediate(c.GetMaterial(j)); //count went down, destroy
				}
			}
		}else{
			for(int j=0, jL=r.sharedMaterials.Length; j<jL; j++){ //for each material CURRENTLY on the renderer...
				if(j <= subMeshes.Count){ //already there?
					newMats[j] = r.sharedMaterials[j]; //pass on this mat
				}else{
					DestroyImmediate(r.sharedMaterials[j]); //count went down, destroy
				}
			}
		}
		for(int i=0, iL=subMeshes.Count; i<iL; i++){ //for each extra material to make...
			//if it doesn't exist yet, create new
			//update/delete/make new materials
			if(newMats[i+1] != null){ //is there already a slot to update here?
				//Debug.Log("This is being reached!");
				newMats[i+1].shader = textShader;
				//use submesh to get name, here!!!
				//update shader...
				STMTextureData newTextureData = data.textures.Find(x => x.name == subMeshes[i].name); //and what texture to use!
				newMats[i+1].mainTexture = UseThisFont.material.mainTexture; //assign font texture
				if(newTextureData != null){
					newMats[i+1].SetTexture("_MaskTex", newTextureData.texture);
					newMats[i+1].SetTextureScale("_MaskTex", newTextureData.tiling);
				}
			}else{
				Material newMat = new Material(textShader); //create new
				//use submesh to get name, here!!!
				STMTextureData newTextureData = data.textures.Find(x => x.name == subMeshes[i].name); //and what texture to use!
				newMat.mainTexture = UseThisFont.material.mainTexture; //assign font texture
				if(newTextureData != null){
					newMat.SetTexture("_MaskTex", newTextureData.texture);
					newMat.SetTextureScale("_MaskTex", newTextureData.tiling);
				}
				newMats[i+1] = newMat; //add to new array
			}
		}
		return newMats;
	}
}
