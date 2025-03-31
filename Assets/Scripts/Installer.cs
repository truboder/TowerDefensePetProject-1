using UnityEngine;

[DefaultExecutionOrder(-1)]
public class Installer : MonoBehaviour
{
    [SerializeField] private EnemyPath _enemyPath;

    private void Awake()
    {
        Container.Instance.Register<EnemyPath>(_enemyPath);
    }
}