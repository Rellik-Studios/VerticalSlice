using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Himanshu
{
    public static class Utility
    {
        public static void Invoke(this MonoBehaviour _monoBehaviour, Action _action, float _delay)
        {
            _monoBehaviour.StartCoroutine(InvokeRoutine(_action, _delay));
        }
 
        private static IEnumerator InvokeRoutine(System.Action _action, float _delay)
        {
            yield return new WaitForSeconds(_delay);
            _action();
        }
        
        public static IEnumerator FillBar(this Image _fillImage, float _time, int _dir = 1, float _waitTime = 0f)
        {
            yield return new WaitForSeconds(_waitTime);
            var time = 0f;

            while (time < _time)
            {
                time += Time.deltaTime;
                _fillImage.fillAmount += _dir * Time.deltaTime / _time;
                yield return null;
            }
        }
    }
}