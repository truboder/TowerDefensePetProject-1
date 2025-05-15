using System.Collections;
using UnityEngine;

namespace Common.Coroutines
{
    public interface ICoroutineRunService
    {
        Coroutine StartCoroutine(IEnumerator routine);
    
        void StopCoroutine(Coroutine routine);
    }
}