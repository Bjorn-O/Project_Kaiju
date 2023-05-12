using System;
using JetBrains.Annotations;

namespace Toolbox.Attributes
{
    public enum ButtonMode
    {
        AlwaysEnabled,
        EnabledInPlayMode,
        DisabledInPlayMode
    }
    
    /// <summary>
    /// Attribute to create a button in the inspector for calling the method it is attached to.
    /// The method must have no arguments.
    /// </summary>
    /// <example><code>
    /// [Button]
    /// public void MyMethod()
    /// {
    ///     Debug.Log("Clicked!");
    /// }
    /// </code></example>
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public sealed class ButtonAttribute : Attribute
    {
        /// <summary> Custom name of a button or <c>null</c> if not set. </summary>
        public readonly string Name;

        public ButtonAttribute() { }
        public ButtonAttribute(string name) => this.Name = name;

        /// <summary>
        /// A mode that indicates when the button must be enabled.
        /// Defaults to <see cref="ButtonMode.AlwaysEnabled"/>.
        /// </summary>
        [PublicAPI]
        public ButtonMode Mode { get; set; } = ButtonMode.AlwaysEnabled;

        /// <summary>
        /// Whether to expand the parameters foldout by default. Has no effect on buttons with no parameters.
        /// Defaults to <c>false</c>.
        /// </summary>
        [PublicAPI]
        public bool Expanded { get; set; }
    }
}
