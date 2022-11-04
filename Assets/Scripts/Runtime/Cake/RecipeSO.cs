using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Cake/Recipe")]
public class RecipeSO : ScriptableObject
{
    [SerializeField] List<CakeLayerSO> _pieces;

    public event Action OnPieceAdded;
    public event Action OnPieceRemoved;
    public event Action OnRecipeCleared;

    public int PieceCount => _pieces.Count;

    public CakeLayerSO GetPieceAtIndex(int index)
    {
        if (_pieces == null || index > _pieces.Count - 1)
            return null;

        return _pieces[index];
    }

    public void AddPiece(CakeLayerSO piece)
    {
        _pieces.Add(piece);
        OnPieceAdded?.Invoke();
    }

    public void RemovePiece(CakeLayerSO piece)
    {
        _pieces.Remove(piece);
        OnPieceRemoved?.Invoke();
    }

    public void Clear()
    {
        _pieces.Clear();
        OnPieceRemoved?.Invoke();
    }

    public bool IdenticalTo(RecipeSO recipe)
    {
        if (_pieces.Count != recipe.PieceCount)
            return false;

        for (int i = 0; i < _pieces.Count; i++)
        {
            if (_pieces[i] != recipe.GetPieceAtIndex(i))
                return false;
        }

        return true;
    }

    public float GetOffsetAtIndex(int index)
    {
        float offset = 0f;

        for (int i = 0; i < index; i++)
            offset += _pieces[i].Height;

        return offset;
    }
}
