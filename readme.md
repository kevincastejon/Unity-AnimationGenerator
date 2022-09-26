# Animation Generator

Generate **AnimationClip** assets from a **Texture2D** spritesheet asset, save and reuse the sprites mapping configuration with other similar spritesheets.

[Get it on the AssetStore](https://assetstore.unity.com/packages/tools/animation/animation-generator-229840)

[Get the Unity package](https://github.com/kevincastejon/Unity-AnimationGenerator/releases/tag/v1.0)

## How to use

- Create a Animation Spritesheet Configuration asset by using the **Create** menu then selecting **Animation Spritesheet Configuration**.
- Open it by doucle-clicking the asset or by using the **Open** button into the asset inspector.
- Select a **Texture2D** spritesheet asset with ordered sprites.
- Add/remove animations and modify animations names, lengths (number of sprites), framerates, looping parameters.
- You can preview each animation by selecting it on the list.
- You can add an ***internal path*** for your sprites (usefull if the **SpriteRenderer** component is on a **GameObject** that is a child of the **GameObject** holding the **Animator** component).
- Generate **AnimationClip** assets from the animation selected on the list by clicking the **Generate selected animation** button at the bottom of the window.
- Generate **AnimationClip** assets from all animations on the list by clicking the **Generate all animations** button at the bottom of the window.

## Tutorial

![Tuto0](https://kevincastejon.github.io/Unity-AnimationGenerator/Assets/KevinCastejon/AnimationGenerator/Documentation/Tuto1.png)
![Tuto1](https://kevincastejon.github.io/Unity-AnimationGenerator/Assets/KevinCastejon/AnimationGenerator/Documentation/Tuto2.png)
![Tuto2](https://kevincastejon.github.io/Unity-AnimationGenerator/Assets/KevinCastejon/AnimationGenerator/Documentation/Tuto3.png)
![Tuto3](https://kevincastejon.github.io/Unity-AnimationGenerator/Assets/KevinCastejon/AnimationGenerator/Documentation/Tuto4.png)
