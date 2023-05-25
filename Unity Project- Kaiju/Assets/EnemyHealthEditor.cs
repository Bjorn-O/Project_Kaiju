using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EnemyHealth))]
[CanEditMultipleObjects]
public class EnemyHealthEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (!GUILayout.Button("Enemy Death")) return;
        foreach (var targetObject in targets)
        {
            var enemyHealth = (EnemyHealth)targetObject;
            enemyHealth.EnemyDeath();
        }
    }
}
