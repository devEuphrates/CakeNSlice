using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Euphrates;
using System;

public class SliceDivider : MonoBehaviour
{
    [SerializeField] TriggerChannelSO _enableTrigger;
    [SerializeField] TriggerChannelSO _disableTrigger;
    [Space]
    [SerializeField] SliceHolderSO _sliceHolder;
    [SerializeField] ServicePlate _smallPlatePrefab;
    [SerializeField] float _plateOffset;

    List<ServicePlate> _plates = new List<ServicePlate>();

    private void OnEnable()
    {
        _sliceHolder.OnListSet += DivideSlices;
    }

    private void OnDisable()
    {
        _sliceHolder.OnListSet -= DivideSlices;
    }

    bool _moveEnabled = false;
    private void FixedUpdate()
    {
        if (!_moveEnabled)
            return;

        transform.position += Vector3.forward * 4f * Time.fixedDeltaTime;
    }

    void DivideSlices()
    {
        _moveEnabled = true;

        CreatePlates();

        for (int i = 0; i < _sliceHolder.Objects.Count; i++)
        {
            var slc = _sliceHolder.Objects[i];
            _plates[i].AddSlice(slc);
        }
    }

    void CreatePlates()
    {
        for (int i = 0; i < _sliceHolder.Objects.Count; i++)
        {
            Vector3 pos = transform.position + i * _plateOffset * Vector3.forward;

            ServicePlate sp = Instantiate(_smallPlatePrefab, pos, Quaternion.identity, transform);
            sp.name = $"Plate-{i}";

            _plates.Add(sp);
        }
    }
}
