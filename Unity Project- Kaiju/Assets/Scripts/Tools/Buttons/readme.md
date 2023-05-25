# Easy buttons for the Unity default inspector
> created from https://github.com/madsbangh/EasyButtons <br>
> slightly changed some files and naming fixed to use jetbrains rider's naming convention 
> <br>

> These tiny scripts add the ability to quickly show a button in the inspector for any method.

## How to use:
1. Add the Button attribute to a method.

   ```csharp
   using EasyButtons; // 1. Import the namespace
   using UnityEngine;
   
   public class ButtonsExample : MonoBehaviour
   {
       // 2. Add the Button attribute to any method.
   	[Button]
   	public void SayHello()
       {
           Debug.Log("Hello");
       }
   }
   ```
You should now see a button at the bottom of the component with the same name as the method:

3. Add the Button attribute to a method with parameters.

   ```csharp
   using EasyButtons;
   using UnityEngine;
   
   public class Test : MonoBehaviour
   {
       [Button]
       public void ButtonWithParameters(string message, int number)
       {
           Debug.Log($"Your message #{number}: '{message}'");
       }
   }
   ```
You can now enter parameter values and invoke the method in the inspector:

## Attribute Options

The `Button` attribute has different options that allow customizing the button look.

***Mode*** - indicates when the button is enabled. You can choose between the following options:

- AlwaysEnabled - the button is enabled in edit mode and play mode.

- EnabledInPlayMode - the button is only enabled in play mode.

- DisabledInPlayMode - the button is only enabled in edit mode.

***Expanded*** - whether to expand the parameters foldout by default. It only works with buttons that have parameters.

## Custom Editors

If you want to show buttons in a custom editor, you can use the **ButtonsDrawer** class

Instantiate ButtonsDrawer in OnEnable if possible, then draw the buttons with help of the DrawButtons method, as in the example:

```csharp
[CustomEditor(typeof(Example))]
public class CustomEditor : ObjectEditor
{   
    private ButtonsDrawer _buttonsDrawer;

    private void OnEnable()
    {
        _buttonsDrawer = new ButtonsDrawer(target);
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        _buttonsDrawer.DrawButtons(targets);
    }
}
```

You can also draw only specific buttons:

```csharp
// Draw only the button called "Custom Editor Example"
_buttonsDrawers.Buttons.First(button => button.DisplayName == "Custom Editor Example").Draw(targets);
```

You can search for a specific button using its ***DisplayName*** or ***Method*** (MethodInfo object the button is attached to.)

