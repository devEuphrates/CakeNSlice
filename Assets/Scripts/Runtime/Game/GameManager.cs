using Euphrates;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] LevelHolderSO _levels;
    [SerializeField] RecipeSO _levelOrder;

    [Header("Game Triggers")]
    [SerializeField] TriggerChannelSO _levelSet;

    private void Start()
    {

        SetCurrentlevel();
    }

    void SetCurrentlevel()
    {
        LevelSO level = _levels.GetLevel(1);
        _levelOrder.Clear();

        for (int i = 0; i < level.Recipe.PieceCount; i++)
        {
            CakeLayerSO piece = level.Recipe.GetPieceAtIndex(i);
            _levelOrder.AddPiece(piece);
        }

        _levelSet.Invoke();
    }
}
