using UnityEngine;

[CreateAssetMenu(fileName = "EnemyScriptable", menuName = "Scriptable Objects/EnemyScriptable")]
public class EnemyScriptable : ScriptableObject
{
    [Header("Enemy Stats")]
    public int health = 1;
    public int damage = 1;
    public EnemyType enemyType;

    public enum EnemyType
    {
        Melee,
        Ranged
    }
}