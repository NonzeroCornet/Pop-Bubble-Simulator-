using UnityEngine;

[CreateAssetMenu(fileName = "Narrator", menuName = "Scriptable Objects/Narrator")]
public class Narrator : ScriptableObject
{
    public string name;
    public AudioClip[] clips;
}
