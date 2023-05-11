using System;
using System.Reflection;
using UnityEngine;

namespace Toolbox.Attributes
{
    internal class NoScriptFieldEditor : UnityEditor.Editor
    {
        private static readonly MethodInfo RemoveLogEntriesByMode;
        private static readonly string[] PropertiesToExclude = { "m_Script" };

        static NoScriptFieldEditor()
        {
            const string logEntryClassName = "UnityEditor.LogEntry";
            const string removeLogMethodName = "RemoveLogEntriesByMode";

            var editorAssembly = Assembly.GetAssembly(typeof(UnityEditor.Editor));
            Type logEntryType = editorAssembly.GetType(logEntryClassName);
            RemoveLogEntriesByMode = logEntryType.GetMethod(removeLogMethodName, BindingFlags.NonPublic | BindingFlags.Static);

            if (RemoveLogEntriesByMode == null)
            {
                Debug.LogError($"Could not find the {logEntryClassName}.{removeLogMethodName}() method. " +
                               "Please submit an issue and specify your Unity version: https://github.com/EmileDavidson/Unity-Toolbox/issues/new");
            }
        }

        public override void OnInspectorGUI()
        {
            DrawPropertiesExcluding(serializedObject, PropertiesToExclude);
        }

        public void ApplyModifiedProperties()
        {
            if ( ! serializedObject.hasModifiedProperties)
                return;

            serializedObject.ApplyModifiedPropertiesWithoutUndo();
            RemoveNoScriptWarning();
        }

        private static void RemoveNoScriptWarning()
        {
            // The warning doesn't appear in edit mode.
            if ( ! Application.isPlaying)
                return;

            // The "No Script asset for ..." log has a unique identifier that can be used to remove the warning.
            const int noScriptAssetMode = 262144;
            RemoveLogEntriesByMode?.Invoke(null, new object[] { noScriptAssetMode });
        }
    }
}