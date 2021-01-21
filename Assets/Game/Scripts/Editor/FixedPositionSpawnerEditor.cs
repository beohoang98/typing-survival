using System.Linq;
using Game.Scripts.Game;
using UnityEditor;
using UnityEngine;

namespace Game.Scripts.Editor
{
    [CustomEditor(typeof(FixedPositionSpawner))]
    [CanEditMultipleObjects]
    public class FixedPositionSpawnerEditor : UnityEditor.Editor
    {
        private FixedPositionSpawner spawner;

        private void OnEnable()
        {
            spawner = (FixedPositionSpawner) target;
        }

        private void OnSceneGUI()
        {
            float penSize = HandleUtility.GetHandleSize(spawner.transform.position) * 0.25f;
            Handles.CapFunction capFunction = Handles.SphereHandleCap;
            foreach (var index in Enumerable.Range(0, spawner.spawnPosition.Count))
            {
                Vector2 newPos =
                    Handles.FreeMoveHandle(spawner.spawnPosition[index], Quaternion.identity, penSize, Vector3.one,
                        capFunction);
                if (spawner.spawnPosition[index] != newPos)
                {
                    Undo.RecordObject(spawner, "Move point");
                    spawner.spawnPosition[index] = newPos;
                }
            }
        }
    }
}