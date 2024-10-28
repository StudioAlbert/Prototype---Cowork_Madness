using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GesturesManager : MonoBehaviour
{
    
    [SerializeField] PlayerInputController _inputController;

    [SerializeField] private Animator _animator;
    [SerializeField] [Range(0, 11)] private int _gestureIndex = 0;
    [SerializeField] [Range(0, 3)] private int _saluteIndex = 0;
    [SerializeField] private bool _isChating;

    [SerializeField] private float _minTempo = 0.4f;
    [SerializeField] private float _maxTempo = 0.7f;

    private Coroutine _playGestureCo;

    //private bool _saluteLock = false;

    private void Start()
    {
        
    }

    void OnEnable()
    {
        StartPlayGesture();
    }
    private void OnDisable()
    {
        StopPlayGesture();
    }

    // Update is called once per frame
    void Update()
    {
        if(_inputController.Salute)
        {
            Salute();
        }        
    }

    #region SALUTE
    public void Salute()
    {
        StopCoroutine(_playGestureCo);
        _animator.ResetTrigger("Salute");
        _animator.SetTrigger("Salute");
        // pick a different salute
        _saluteIndex = Random.Range(0, 4);
        _animator.SetInteger("SalutePosture", _saluteIndex);
        
        
    }
    void OnSaluteEnd()
    {
        Debug.Log("Salute End");
        _animator.ResetTrigger("Salute");
    }
    void OnSaluteBegin()
    {
        Debug.Log("Salute Begin");
    }
    
    #endregion

    #region GESTURES

    private void StartPlayGesture()
    {
        StopPlayGesture();
    }

    private void StopPlayGesture()
    {
        if (_playGestureCo == null)
        {
            _playGestureCo = StartCoroutine(PlayGesture());
        }
    }

    private IEnumerator PlayGesture()
    {
        while (true)
        {
            //_animator.ResetTrigger("NewGesture");
            yield return new WaitForSeconds(Random.Range(_minTempo, _maxTempo));
            //_animator.SetInteger("GestureIndex", Random.Range(0,11));
            _animator.SetFloat("GestureWeight", Random.Range(0f, 1f));
            _animator.SetTrigger("NewGesture");
            yield return new WaitForEndOfFrame();
            _animator.ResetTrigger("NewGesture");

            _gestureIndex++;
            if (_gestureIndex > 11)
                _gestureIndex = 0;

        }
    }

    #endregion

}