using System.Collections.Generic;
using System.Reflection;
using JetBrains.Annotations;
using UnityEditor;

namespace Toolbox.Attributes
{
    /// <summary>
    /// A class that holds information about a button and can draw it in the inspector.
    /// </summary>
    public abstract class Button
    {
        /// <summary> Display name of the button. </summary>
        [PublicAPI] public readonly string DisplayName;

        /// <summary> MethodInfo object the button is attached to. </summary>
        [PublicAPI] public readonly MethodInfo Method;
        
        private readonly bool _disabled;

        protected Button(MethodInfo method, ButtonAttribute buttonAttribute)
        {
            DisplayName = string.IsNullOrEmpty(buttonAttribute.Name)
                ? ObjectNames.NicifyVariableName(method.Name)
                : buttonAttribute.Name;

            this.Method = method;

            bool inAppropriateMode = EditorApplication.isPlaying
                ? buttonAttribute.Mode == ButtonMode.EnabledInPlayMode
                : buttonAttribute.Mode == ButtonMode.DisabledInPlayMode;

            _disabled = ! (buttonAttribute.Mode == ButtonMode.AlwaysEnabled || inAppropriateMode);
        }

        public void Draw(IEnumerable<object> targets)
        {
            using (new EditorGUI.DisabledScope(_disabled))
            {
                DrawInternal(targets);
            }
        }

        internal static Button Create(MethodInfo method, ButtonAttribute buttonAttribute)
        {
            var parameters = method.GetParameters();

            if (parameters.Length == 0) return new ButtonWithoutParams(method, buttonAttribute); 
            return new ButtonWithParams(method, buttonAttribute, parameters);
        }

        protected abstract void DrawInternal(IEnumerable<object> targets);
    }
}