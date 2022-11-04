using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "Level/Level", order = 0)]
public class LevelSO : ScriptableObject
{
    public string Name;
    public int Level;
    public GameObject LevelPrefab;
    public RecipeSO Recipe;
    public uint SliceCount;
}
