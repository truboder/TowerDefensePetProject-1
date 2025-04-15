using System.Collections;
using UnityEngine;

public interface ICoroutineRunService
{
    Coroutine StartCoroutine(IEnumerator routine);
    void StopCoroutine(Coroutine routine);
}