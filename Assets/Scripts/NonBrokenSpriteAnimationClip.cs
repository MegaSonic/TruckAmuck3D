﻿using UnityEngine;
#if (UNITY_EDITOR)
using UnityEditor;
#endif
using System.Collections;

public class NonBrokenSpriteAnimationClip : ScriptableObject {
	public enum AnimationType { Once, Looping };

	public Sprite[] spriteFrames;
	public AnimationType animationType;
	public float animationDuration;
}

#if (UNITY_EDITOR)

public class MakeNonBrokenScriptableObject
{
	[MenuItem("Assets/Create/Non-Broken Sprite Animation Clip")]
	public static void CreateSpriteClip()
	{
		NonBrokenSpriteAnimationClip clip = ScriptableObject.CreateInstance<NonBrokenSpriteAnimationClip>();

		AssetDatabase.CreateAsset(clip, "Assets/NewSpriteAnimationClip.asset");
		AssetDatabase.SaveAssets();

		EditorUtility.FocusProjectWindow();

		Selection.activeObject = clip;
	}
}

#endif