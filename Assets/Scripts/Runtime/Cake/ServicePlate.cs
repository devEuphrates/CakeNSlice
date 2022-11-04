using Euphrates;
using UnityEngine;

public class ServicePlate : MonoBehaviour
{
    CakeSlice _slice;

    public void AddSlice(CakeSlice slice)
    {
        _slice = slice;

        slice.transform.parent = transform;
        Tween.DoTween(slice.transform.localPosition, Vector3.zero, 1f, Ease.Lerp, (Vector3 pos) => slice.transform.localPosition = pos);
    }
}
