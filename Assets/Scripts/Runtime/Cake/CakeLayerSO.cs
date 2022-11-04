using UnityEngine;

[CreateAssetMenu(fileName = "New Cake Piece", menuName = "Cake/Piece")]
public class CakeLayerSO : ScriptableObject
{
    public float Id;
    public string Name;
    public float Height;
    public Sprite Icon;
}
