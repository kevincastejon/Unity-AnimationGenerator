# EditorToolbox

A set of Unity Editor tools.<BR/>
This package contains several custom attributes, windows and components. They are all standalone and can be imported and used separately.

[Get the UnityPackage!](https://github.com/kevincastejon/EditorToolbox/releases/download/1.0.0/KevinCastejon.EditorToolbox.unitypackage)

[Documentation](https://kevincastejon.github.io/EditorToolbox/)

## What's in the box?

#### Custom Attributes


- [**Icon**](/Assets/KevinCastejon/EditorToolbox/Documentation/Attributes/Icon/readme.md)<BR/>
Custom *Inspector* property icon.

- [**LabelPlus**](/Assets/KevinCastejon/EditorToolbox/Documentation/Attributes/LabelPlus/readme.md)<BR/>
Custom *Inspector* property label that allows using an icon, a custom label text and a custom label color.

- [**HeaderPlus**](/Assets/KevinCastejon/EditorToolbox/Documentation/Attributes/HeaderPlus/readme.md)<BR/>
Custom *Inspector* property header that allows using an icon, a custom header label text and a custom header label color.

- [**ReadOnly**](/Assets/KevinCastejon/EditorToolbox/Documentation/Attributes/ReadOnly/readme.md)<BR/>
Prevents a property from being edited on the *Inspector*.

- [**ReadOnlyOnPlay**](/Assets/KevinCastejon/EditorToolbox/Documentation/Attributes/ReadOnlyOnPlay/readme.md)<BR/>
Prevents a property from being edited on the *Inspector* in *PlayMode*. The behaviour can be inverted so the property is editable only in *PlayMode*.

- [**ReadOnlyOnPrefab**](/Assets/KevinCastejon/EditorToolbox/Documentation/Attributes/ReadOnlyOnPrefab/readme.md)<BR/>
Prevents a property from being edited on the *Inspector* in *PrefabMode*. The behaviour can be inverted so the property is editable only in *PrefabMode*.

- [**HideOnPlay**](/Assets/KevinCastejon/EditorToolbox/Documentation/Attributes/HideOnPlay/readme.md)<BR/>
Hides the property in *PlayMode*. The behaviour can be inverted with the 'invert' parameter so the property is visible only in *PlayMode*.

- [**HideOnPrefab**](/Assets/KevinCastejon/EditorToolbox/Documentation/Attributes/HideOnPrefab/readme.md)<BR/>
Hides the property in *PrefabMode*. The behaviour can be inverted with the 'invert' parameter so the property is visible only in *PrefabMode*.

- [**Tag**](/Assets/KevinCastejon/EditorToolbox/Documentation/Attributes/Tag/readme.md)<BR/>
Displays a dropdown list of available Tags (must be used with a 'string' typed property).

- [**Scene**](/Assets/KevinCastejon/EditorToolbox/Documentation/Attributes/Scene/readme.md)<BR/>
Displays a dropdown list of available build settings Scenes (must be used with a 'string' typed property).

#### Automatization


- [**AnimationGenerator**](/Assets/KevinCastejon/EditorToolbox/Documentation/Automatization/AnimationGenerator/readme.md)<BR/>
Generate **AnimationClip** assets from a **Texture2D** spritesheet asset, save and reuse the sprites mapping configuration.

- [**CustomEditorGenerator**](/Assets/KevinCastejon/EditorToolbox/Documentation/Automatization/CustomEditorGenerator/readme.md)<BR/>
Generate a base **Editor** script for any **MonoBehaviour** script.

- [**StateMachineGenerator**](/Assets/KevinCastejon/EditorToolbox/Documentation/Automatization/StateMachineGenerator/readme.md)<BR/>
Generate **StateMachine** scripts squeletons with a few clicks.

- [**TexturesMaxSizesSetter**](/Assets/KevinCastejon/EditorToolbox/Documentation/Automatization/TexturesMaxSizesSetter/readme.md)<BR/>
Automatically set the *Max Texture Size* on multiple **Texture2D** assets according to their real sizes.

#### Components


- [**Cone**](/Assets/KevinCastejon/EditorToolbox/Documentation/Components/Cone/readme.md)<BR/>
Generate a cone mesh, renderer and collider, on the fly.

- [**GameObjectsToggler**](/Assets/KevinCastejon/EditorToolbox/Documentation/Components/GameObjectsToggler/readme.md)<BR/>
Switch the enable state between two GameObjects and fire events on switching.

- [**TriggerNotifier**](/Assets/KevinCastejon/EditorToolbox/Documentation/Components/TriggerNotifier/readme.md)<BR/>
Bind triggers message methods to **UnityEvents**.
