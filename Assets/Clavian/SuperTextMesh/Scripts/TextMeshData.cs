//Copyright (c) 2016 Kai Clavier [kaiclavier.com] Do Not Distribute
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
///NOTE: only re-create TextInfo if RAW TEXT changes
#if UNITY_EDITOR
using UnityEditor; //for loading default stuff and menu thing
#endif
[System.Serializable]
public class TextInfo{ //info for an individual letter. used internally by super text mesh. References back to textdata. (info[i], per-letter)
	//MAKE SURE TO ADD VARIABLE TO CONSTRUCTOR WHEN MAKING A NEW ONE
	public Font font; //what font this character info is...
	public CharacterInfo ch; //contains uv data, point size (quality), style, etc
	//public char c; //nah, just sync it w/ hyphenedText[]. it works better cause hyphenedtext will always be shorter
	public Vector3 pos; //where the bottom-left corner is

	public float readTime = -1f; //at what time it will start to get read.
	public float unreadTime = -1f;
	public STMDrawAnimData drawAnimData; //which draw animation will be used
	public float size; //localspace size
	public SuperTextMesh.Alignment alignment; //how this text will be aligned


	public List<string> ev = new List<string>(); //event strings.
	public List<string> ev2 = new List<string>(); //repeating event strings.
	public STMColorData colorData = new STMColorData(); //reference or store...
	public STMGradientData gradientData; //reference stuff??
	public STMTextureData textureData;

	//vvvADDITIONAL delay!
	public STMDelayData delayData = new STMDelayData(0); //units to delay for, before this text is shown. multiple of read speed
	
	public STMWaveData waveData;
	public STMJitterData jitterData;

	public STMImageData imageData; //if not null, put this referenced image inline
	
	public STMVoiceData voiceData; //if not null, this will override the text mesh's settings????? ??????
	public STMFontData fontData;
	public Vector3 TopLeftVert{ //return position in local space
		get{
			return pos + new Vector3(ch.minX, ch.maxY, 0) * (size / ch.size); //ch.size is quality
		}
	}
	public Vector3 TopRightVert{ //return position in local space
		get{
			return pos + new Vector3(ch.maxX, ch.maxY, 0) * (size / ch.size);
		}
	}
	public Vector3 BottomRightVert{ //return position in local space
		get{
			return pos + new Vector3(ch.maxX, ch.minY, 0) * (size / ch.size);
		}
	}
	public Vector3 BottomLeftVert{ //return position in local space
		get{
			return pos + new Vector3(ch.minX, ch.minY, 0) * (size / ch.size);
		}
	}
	public Vector3 Middle{
		get{
			return pos + new Vector3((ch.minX + ch.maxX) * 0.5f, (ch.minY + ch.maxY) * 0.5f, 0f) * (size / ch.size);
		}
	}
	public TextInfo(){ //dont use this unless ur gonna override it
		this.ch = new CharacterInfo();
		this.pos = Vector3.zero;
		this.colorData.color = Color.white;
		this.size = 16;
		this.delayData = new STMDelayData(0);
		this.ev = new List<string>();
		this.ev2 = new List<string>();
		this.readTime = -1f;
		this.unreadTime = -1f;
	}
	public TextInfo(FontStyle style, Color col, float size, SuperTextMesh.Alignment alignment){ //for setting "defaults"
		this.ch.style = style;
		this.colorData.color = col;
		this.size = size;
		this.alignment = alignment;
	}
	
	public TextInfo(TextInfo clone, CharacterInfo ch){ //clone everything but character
		this.ch = ch;
		this.pos = clone.pos;
		this.ev = new List<string>(clone.ev);
		this.ev2 = new List<string>(clone.ev2);
		this.colorData = clone.colorData;
		this.gradientData = clone.gradientData;
		this.textureData = clone.textureData;
		this.size = clone.size;
		this.delayData = clone.delayData;
		this.waveData = clone.waveData;
		this.jitterData = clone.jitterData;
		this.alignment = clone.alignment;
		this.voiceData = clone.voiceData;

		this.readTime = clone.readTime;
		this.unreadTime = clone.unreadTime;
		this.drawAnimData = clone.drawAnimData;
	}
	
	
	public TextInfo(TextInfo clone){
		this.ch = clone.ch;
		this.pos = clone.pos;
		this.ev = new List<string>(clone.ev);
		this.ev2 = new List<string>(clone.ev2);
		this.colorData = clone.colorData;
		this.gradientData = clone.gradientData;
		this.textureData = clone.textureData;
		this.size = clone.size;
		this.delayData = clone.delayData;
		this.waveData = clone.waveData;
		this.jitterData = clone.jitterData;
		this.alignment = clone.alignment;
		this.voiceData = clone.voiceData;

		this.readTime = clone.readTime;
		this.unreadTime = clone.unreadTime;
		this.drawAnimData = clone.drawAnimData;
	}
}
/*
NEW TAGS TO ADD:
character spacing tag
line height tag? (work automatically with size)
midway center alignment?? oh boy... maybe make it a special tag that can only be used at the start?

*/


[System.Serializable]
public class STMColorData{
	public string name;
	public Color color;
	public STMColorData(){
		this.color = Color.white; //sure?
	}
	public STMColorData(Color color){
		this.color = color;
	}
}
[System.Serializable]
public class STMGradientData{
	public string name;
	public Gradient gradient;
	public float gradientSpread = 0.1f;
	public float scrollSpeed = 0.0f; //can be negative, or 0
	public GradientDirection direction = GradientDirection.Horizontal; //TODO vertical gradients are kinda dumb, maybe remove them? or make em grab from diff, more consistent positions...
	public enum GradientDirection{
		Horizontal,
		Vertical
	}
	public bool smoothGradient = true;
}
[System.Serializable]
public class STMTextureData{
	public string name;
	public Texture texture;
	public bool relativeToLetter = false; //will the texture be relative to each letter
	public bool scaleWithText = false;
	public Vector2 tiling = Vector2.one; //or scale
	public Vector2 offset = Vector2.zero;
	public Vector2 scrollSpeed = Vector2.one;
	//public float speed = 0.5f; //scroll speed
	//public float spread = 0.1f; //how far it stretches, in local
}
[System.Serializable]
public class STMImageData{ //inline images
	public string name;
	public Texture[] frames;
	public int rate;
}
[System.Serializable]
public class STMJitterData{
	public string name;
	public float amount;
	public bool perlin = false;
	public float perlinTimeMulti = 20f;
	public AnimationCurve distance; //how far it can travel, on average
	public AnimationCurve distanceOverTime;
	[Range(0.0001f, 100f)]
	public float distanceOverTimeMulti = 1f;
}
[System.Serializable]
public class STMFontData{
	public string name;
	public Font font;
}
/*
//this isn't having any effect... because it's nested??
#if UNITY_EDITOR
[CustomPropertyDrawer( typeof( STMWaveData ) )]
public class STMWaveDataEditor : PropertyDrawer {
	
	override public void OnGUI(Rect position, SerializedProperty property, GUIContent label){

		SerializedProperty name = property.FindPropertyRelative("name");
		SerializedProperty main = property.FindPropertyRelative("main");
		SerializedProperty individualVertexControl = property.FindPropertyRelative("individualVertexControl");
		SerializedProperty topLeft = property.FindPropertyRelative("topLeft");
		SerializedProperty topRight = property.FindPropertyRelative("topRight");
		SerializedProperty bottomLeft = property.FindPropertyRelative("bottomLeft");
		SerializedProperty bottomRight = property.FindPropertyRelative("bottomRight");

		Rect contentPosition = EditorGUI.PrefixLabel(position, label);
		EditorGUI.indentLevel = 0;
		EditorGUI.PropertyField(contentPosition, name, GUIContent.none);
		contentPosition.y += contentPosition.height;
		//contentPosition.width /= 3f;
		EditorGUI.PropertyField(contentPosition, main, true);
		
		EditorGUI.PropertyField(individualVertexControl);
		if(individualVertexControl.boolValue){
			EditorGUI.PropertyField(topLeft);
			EditorGUI.PropertyField(topRight);
			EditorGUI.PropertyField(bottomLeft);
			EditorGUI.PropertyField(bottomRight);
		}
		
	}
	
}
#endif
*/

[System.Serializable]
public class STMWaveControl{
	public AnimationCurve curveX;
	public AnimationCurve curveY;
	public AnimationCurve curveZ;
	[Range(0f,1f)]
	public float offset; //to be multiplied by 6. timing offset
	public Vector3 speed; //how wide the curve is... so how fast it'll animate
	public Vector3 strength; //how far the curve will move the letters
	public Vector3 density; //how wide the curve is on letters?
}
[System.Serializable]
public class STMWaveData{
	public string name;
	public bool animateFromTimeDrawn = false;
	public STMWaveControl main;
	[Tooltip("Use these below values?")] //this should be getting hidden but it's not...
	public bool individualVertexControl = false;
	public STMWaveControl topLeft;
	public STMWaveControl topRight;
	public STMWaveControl bottomLeft;
	public STMWaveControl bottomRight;
}
[System.Serializable]
public class STMDelayData{
	public string name;
	public int count;
	public STMDelayData(int delay){
		count = delay;
	}
}
[System.Serializable]
public class STMAutoDelayData{
	public char character;
	public int count;
	public STMAutoDelayData(int delay){
		count = delay;
	}
}
[System.Serializable]
public class STMSoundClipData{ //for auto-clips. replacing text sounds
	[TextArea(2,3)]
	public string character;
	public AudioClip clip;
}

/*
[System.Serializable]
public class STMAudioData{ //for setting things like audio clips
	public float readDelay; //set to -1 to use text mesh's setting
	public AudioClip[] readSounds;
	public bool stopPreviousSound;
	[Range(-3f,3f)]
	public float minPitch;
	[Range(-3f,3f)]
	public float maxPitch;
}
*/
[System.Serializable]
public class STMDrawAnimData{
	public string name;
	[Tooltip("How long the Draw Animation will last.")]
	public float animTime = 0f; //time it will take to animate
	public AnimationCurve animCurve;
	public Vector3 startScale = Vector3.one; //how big the letter will start at, relative to letter size
	public Vector3 startOffset = Vector3.zero;
	[Tooltip("How long the fade animation will last.")]
	public float fadeTime = 0f; //time it will take to fade in
	public AnimationCurve fadeCurve;
	[Tooltip("Starting color for read out text.")]
	public Color32 startColor = Color.clear; //for fill/fade. 

	//add curves for this stuff!
}
/*
[System.Serializable]
public class AnimationCurve3{
	public AnimationCurve x;
	public AnimationCurve y;
	public AnimationCurve z;
	public Vector3 offset = Vector3.zero; //position, relative to curve
	public Vector3 multi = Vector3.one; //strength multiple
	public Vector3 Evaluate(float t){
		return new Vector3((x.Evaluate(t) + offset.x) * multi.x, (y.Evaluate(t) + offset.y) * multi.y, (z.Evaluate(t) + offset.z) * multi.z);
	}
}
*/
/*
[CustomPropertyDrawer (typeof (AnimationCurve3))]
public class AnimationCurve3Drawer : PropertyDrawer {
	public override void OnGUI (Rect position, SerializedProperty property, GUIContent label) {
		GUILayout.BeginHorizontal();
		EditorGUI.CurveField(position,property.FindPropertyRelative("x"));
		GUILayout.EndHorizontal();
	}
}
*/
[System.Serializable]
public class STMVoiceData{
	public string name;
	[Space(10)]
	public bool useReadDelay = false;
	public float readDelay = 0f; //if negative, default will be used
	[Space(4)]
	public bool useFastDelay = false;
	public float fastDelay = 0f;
	[Space(10)]
	public bool useDrawAnim;
	public string drawAnimName;
	[Space(10)]
	public bool useAudioClips = false;
	public AudioClip[] audioClips;
	[Space(4)]
	public bool useStopPreviousSound = false;
	public bool stopPreviousSound;
	[Space(4)]
	public bool usePitchMode = false;
	public SuperTextMesh.PitchMode pitchMode;
	[Space(4)]
	public bool useNewPitch = false;
	[Range(0f,3f)]
	public float overridePitch = 1f;
	[Range(-3f,3f)]
	public float minPitch = 1f;
	[Range(-3f,3f)]
	public float maxPitch = 1f;
	[Space(4)]
	public bool useSpeedReadPitch = false;
	[Range(-2f,2f)]
	public float speedReadPitch = 0f;
	[Space(10)]
	public bool useSoundClips = false;
	public List<STMSoundClipData> soundClips = new List<STMSoundClipData>(); //overrides for each letter?
}

[System.Serializable]
public class SubMeshData{ //used internally by STM for keeping track of submeshes
	public string name;
	public List<int> tris = new List<int>();
	public SubMeshData(string name){
		this.name = name;
	}
}
/*
[System.Serializable]
public class STMProfileData{
	public string name;
	public Color color;
	public float size;
	public float quality;
}
*/

/* //I don't need a custom editor for this oops
#if UNITY_EDITOR
[CustomEditor(typeof(TextMeshData))]
public class TextMeshDataEditor : Editor{
	override public void OnInspectorGUI(){
		serializedObject.Update();

		SerializedProperty waves = serializedObject.FindProperty("waves");
		SerializedProperty jitters = serializedObject.FindProperty("jitters");
		SerializedProperty colors = serializedObject.FindProperty("colors");
		SerializedProperty gradients = serializedObject.FindProperty("gradients");
		SerializedProperty textures = serializedObject.FindProperty("textures");

		EditorGUILayout.LabelField("Effects", EditorStyles.boldLabel);
		EditorGUILayout.PropertyField(waves, true); //yes, show children
		EditorGUILayout.PropertyField(jitters, true);

		EditorGUILayout.PropertyField(colors, true);
		EditorGUILayout.PropertyField(gradients, true);
		EditorGUILayout.PropertyField(textures, true);

		serializedObject.ApplyModifiedProperties();
	}
}
#endif
*/
//to create a new one, this line:
[CreateAssetMenu(fileName = "New Text Data", menuName = "Clavian/Text Mesh Data", order = 1)]
public class TextMeshData : ScriptableObject { //the actual textdata manager file
	[HideInInspector] public bool textDataEditMode = false; //whther this will show on objects or not
	[HideInInspector] public bool showEffectsFoldout = false;
	//[Header("Effects")]
	//public List<STMFontData> fonts = new List<STMFontData>();
	public List<STMWaveData> waves = new List<STMWaveData>();
	public List<STMJitterData> jitters = new List<STMJitterData>();
	public List<STMDrawAnimData> drawAnims = new List<STMDrawAnimData>();
	[HideInInspector] public bool showTextColorFoldout = false;
	//[Header("Text Color")]
    public List<STMColorData> colors = new List<STMColorData>();
    public List<STMGradientData> gradients = new List<STMGradientData>();
    public List<STMTextureData> textures = new List<STMTextureData>();
    [HideInInspector] public bool showInlineFoldout = false;
    //[Header("Inline")]
    public List<STMDelayData> delays = new List<STMDelayData>();
    public List<STMVoiceData> voices = new List<STMVoiceData>();
    public List<STMFontData> fonts = new List<STMFontData>();
    //public List<STMAudioData> audioSettings = new List<STMAudioData>(); //not yet!
    //public List<STMImageData> images = new List<STMImageData>(); //not implemented yet
    [HideInInspector] public bool showAutomaticFoldout = false;
    //[Header("Automatic")]
    public List<STMAutoDelayData> autoDelays = new List<STMAutoDelayData>();
    public List<STMSoundClipData> clips = new List<STMSoundClipData>();
    
    [HideInInspector] public bool showMasterFoldout = true;
    //[Header("Master")]
    public bool disableAnimatedText = false;
    

    //list of icons here...

    //TODO: profiles for fancy text presets
    /*
	<j=kevin> </j> //use kevin's jitter??
	or
	<j> </j>
	use jitter amount tied to the text mesh!!

	//yeah, like size
	perfect!! just make optional text tags so u don't have to parse numbers...
    */
}
