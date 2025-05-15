using System.Collections;
using UnityEngine;

namespace Common.Coroutines
{
    public class CoroutineRunService : ICoroutineRunService
    {
        private readonly CoroutineHolder _coroutineHolder;

        public CoroutineRunService()
        {
            var holderObject = new GameObject("CoroutineHolder");
            _coroutineHolder = holderObject.AddComponent<CoroutineHolder>();
        }

        public Coroutine StartCoroutine(IEnumerator routine) => _coroutineHolder.StartCoroutine(routine);

        public void StopCoroutine(Coroutine routine) => _coroutineHolder.StopCoroutine(routine);
    }
}