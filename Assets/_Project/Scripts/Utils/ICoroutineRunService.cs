using System.Collections;
using UnityEngine;

namespace Utils
{
    public interface ICoroutineRunService
    {
        Coroutine StartCoroutine(IEnumerator routine);
    
        void StopCoroutine(Coroutine routine);
    }
}