using System.Collections;
using UnityEngine;

namespace _Project.Scripts.Utils
{
    public interface ICoroutineRunService
    {
        Coroutine StartCoroutine(IEnumerator routine);
    
        void StopCoroutine(Coroutine routine);
    }
}