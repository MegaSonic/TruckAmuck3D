using UnityEngine;
using System.Collections;

/// <summary>
/// You should not actually be putting this component on a game object! Put one of the inherited objects on it!!
/// 
/// I can't make classes derived from MonoBehaviours virtual or else I would do it. :p
/// </summary>
public class EnemyBehaviour : MonoBehaviour {

    public virtual void UpdateEnemy() { Debug.Log("Virtual UpdateEnemy is being called!"); }
}
