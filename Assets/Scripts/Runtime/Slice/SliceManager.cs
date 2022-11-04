using Euphrates;
using System.Collections.Generic;
using UnityEngine;

public class SliceManager : MonoBehaviour
{
    [Header("Triggers")]
    [SerializeField] TriggerChannelSO _activateTrigger;
    [SerializeField] TriggerChannelSO _deactivateTrigger;

    [SerializeField] SliceableCake _cake;
    [SerializeField] IntSO _currentCut;
    [SerializeField] TriggerChannelSO _cut;

    [Space]
    [SerializeField] InputReaderSO _inputReader;
    [SerializeField] FloatSO _inputTreshold;
    [SerializeField] FloatSO _sliceSize;
    [SerializeField] SliceHolderSO _sliceHolder;

    List<int> _cuts = new List<int>();

    bool _enabled = false;

    private void OnEnable()
    {
        _activateTrigger.AddListener(EnableCutting);
        _deactivateTrigger.AddListener(DisableCutting);

        _inputReader.OnTouchDown += OnTouchDown;
        _inputReader.OnTouchMove += OnTouchMove;
        //_inputReader.OnTouchUp += OnTouchUp;

        _cut.AddListener(OnCut);
    }

    private void OnDisable()
    {
        _activateTrigger.RemoveListener(EnableCutting);
        _deactivateTrigger.RemoveListener(DisableCutting);

        _inputReader.OnTouchDown -= OnTouchDown;
        _inputReader.OnTouchMove -= OnTouchMove;
        //_inputReader.OnTouchUp -= OnTouchUp;

        _cut.RemoveListener(OnCut);
    }

    Vector2 _initialTouch;
    void OnTouchDown(Vector2 position) => _initialTouch = position;

    void OnTouchMove(Vector2 position)
    {
        if (!_enabled) 
            return;

        float xMove = (position - _initialTouch).x;

        if (Mathf.Abs(xMove) < _inputTreshold)
            return;

        _initialTouch = position;
        int rotateStep = Mathf.FloorToInt(xMove / _inputTreshold);

        int cnt = Mathf.RoundToInt(360f / _sliceSize);
        _currentCut.Value = (_currentCut.Value + rotateStep) % cnt;
    }

    //void OnTouchUp(Vector2 position)
    //{

    //}

    void OnCut()
    {
        int curCut = _currentCut;
        int cnt = Mathf.RoundToInt(360f / _sliceSize);
        int halfLen = cnt / 2;

        if (_currentCut < 0)
        {
            int curCnt = _currentCut.Value % cnt;

            curCut = cnt + curCnt;

            if (curCut >= halfLen)
                curCut -= halfLen;
        }

        if (_cuts.Exists(c => c == curCut || c == curCut + halfLen))
            return;

        _cuts.Add(curCut);
    }

    void EnableCutting()
    {
        _enabled = true;
    }

    void DisableCutting()
    {
        enabled = false;

        if (_cuts.Count == 0)
            return;

        var slices = _cake.Slice(_cuts.ToArray());
        _sliceHolder.SetList(slices);
    }
}
