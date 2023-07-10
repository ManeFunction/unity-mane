1.7.3:
    - Added Enum.IsObsolete check.

1.7.2:
    - Serialized List: Added feature to get access to the list content from the runtime.

1.7.1:
    - Improved errors handling in the Serialized List attribute.

1.7.0:
    - Finally finished new Serialized List feature. Based on Scriptable Objects now.

1.6.47:
    - Another test... Sadly, I can't reproduce the issue in the test project.

1.6.46:
    - Trying to resolve strange issues that appears in real projects, but not reproducible in the test.

1.6.45:
    - Decomposed some code to ease debug process.

1.6.44:
    - Fixed compilation issues caused with the last update. Improved Serialized List attribute workflow.

1.6.43:
    - Added Serialized List attribute, to serialize string or int lists from the custom asset lists. Amazing feature for GD people to expand enum-like dropdown fields without using any code.

1.6.42:
    - Added TransformCopierWindow (Mane/Transform Copier) to copy transform data from one bunch of object to another.

1.6.41:
    - Added IsInsideRectangle() extensions to Vector2.

1.6.40:
    - Added GetWorldCoordinates() extension to RectTransform.

1.6.39:
    - Added AnimationEventInvoker to invoke methods on animation events.

1.6.38:
    - Made PrefabList IEnumerable<T> to allow for foreach loops and LINQ.

1.6.37:
    - Added RectTransform.Set...Offset() extension methods.

1.6.36:
    - Added SafePlus(), SafeMinus() and SafeMultiply() int extensions to avoid overflows. 

1.6.35:
    - Added ClearElements() method to PrefabList. 

1.6.34:
    - Fixed wrong privacy modifier on the last extension.

1.6.33:
    - Added string.GetCountedString extension to select a string from a list based on a counter.

1.6.32:
    - Added AnimationEventParticleSystemInvoker to invoke ParticleSystem.Play() on animation events.
    - Changed Mane.StateMachine namespace to Mane.AnimatorStateMachine.

1.6.31:
    - Added math operators to MimMax classes. 
    - Fixed some Random methods summary.

1.6.30:
    - Added int and float MinMax serializable classes with Random value generator.
    - Added Random.SelectFrom and Random.TrySelectFrom methods to select random elements from a list of sorted odds.

1.6.29:
    - Added Count and iterator to the PrefabList class.

1.6.28:
    - Added string.ColorizeRich() extension to add colors to your text.

1.6.27:
    - Replaced old string.FormatColored() extensions with more universal substrings versions.

1.6.26:
    - Added item index to PrefabList initialization process.

1.6.25:
    - Change some string.FormatColored() overrides signatures.

1.6.24:
    - Added PrefabList<T> to easy UI list managing.
    - Fixed some formatting issues in string.FormatColored().

1.6.23:
    - Added string.FormatColored() extensions that duplicate base Format method, but with rich text coloring for float and int values.

1.6.22:
    - Negate Camera Follower exception if no camera is found.

1.6.21:
    - Made Sprite Camera Follower works in Editor mode as well.

1.6.20:
    - Added Sprite Camera Follower to face sprite to camera. Override it to change target camera.

1.6.19:
    - Allow screenshoter to work in editor time.

1.6.18:
    - Fixed compilation error from 1.6.17.

1.6.17:
    - Renamed SetActiveRecursively to SetActiveStateRecursively to avoid conflicts with Unity's own SetActiveRecursively.

1.6.16:
    - Added GameObject.SetActiveRecursively extension to set active states recursively.
    - Added GameObject.DoRecursively extension to invoke method foreach child.

1.6.15:
    - Prefab saver: Create a property field instead of path string.

1.6.14:
    - Added int.RoundTo(int) extension.

1.6.13:
    - Added float.RoundTo(int) extension to round ints to 5th, for example.

1.6.12:
    - Added Status Event Sender mono behaviour that send events when object is enabling/disabling.
    - Moved all "prefab" menus to the "Prefab" submenu.

1.6.11:
    - Renamed CapitalizeFirstLetter() to ToUpperFirst().

1.6.10:
    - Added CapitalizeFirstLetter() string extension.

1.6.9:
    - Changed some default options in ArrayOdds property.

1.6.8:
    - Added custom labeling system to the new ArrayOdds property drawer.

1.6.7:
    - Added new ArrayOdds property attribute similar to EnumArrayOdds but for the plain arrays.

1.6.6:
    - Fixed exceptions in TimeSpanExtensions.

1.6.5:
    - Made EditorTools.CreateDirectoryFromAssetPath public.

1.6.4:
    - Moved random colors getters from Color and Color32 extensions to Random.

1.6.3:
    - Made GameObject.SaveToPrefab extension public.

1.6.2:
    - Improved work with prefabs editor features.

1.6.1:
    - Moved path settings from the shared settings to project and machine specific prefs.

1.6.0:
    - Fixed an issue when applying prefab changes can take a loooooot of time.
    - Changed package name back to com.manefunction.utils to fix updates.

1.5.38:
    - Separate PrefabsTools from AssetsTolls.
    - Added Save to Prefab(s) context menu to save a bunch of prefabs at once.
    - Added Preferences page (Mane Settings). Contains a path for prefabs saving.
    - Changed package name to com.manefunction.tools

1.5.37:
    - Improved EnumArrayOdds Attribute to work with public sum.

1.5.36:
    - Added ClampMin and ClampMax extensions for IComparable values.

1.5.35:
    - Fixed issues in IReadOnlyDictionary.Convert.

1.5.34:
    - Moved Dictionary.Convert extension to IReadOnlyDictionary.Convert.

1.5.33:
    - Added Convert extension for Dictionary.

1.5.32:
    - Fixed exceptions from Apply Prefab... checkers.
    - Made some Editor Tools public.

1.5.31:
    - Renamed from Mane Utils to Mane Tools.
    - Added availability checkers for Apply Prefab... methods.
    - Refactored all the code.

1.5.30:
    - Added GameObject.IterateChildren extension to fast children iteration (recursive or not).

1.5.29:
    - Fixed NaN result if we Map() from range starting with zero to the range starting with zero either. 

1.5.28:
    - Children Transform Freezer: Added ability to keep nav mesh obstacles on root object in place.

1.5.27:
    - Children Transform Freezer: Added ability to keep colliders on root object in place. 

1.5.26:
    - Children Transform Freezer: Added ability to affect disabled children.

1.5.25:
    - Fixed an issue when pasting global doesn't mark object as changed.

1.5.24:
    - Added copy/paste menus for global Transform values (position and rotation only).

1.5.23:
    - Added Missing Reference Finder to search for missing references, lost components and deleted prefabs.

1.5.22:
    - Renamed TextMesh to ManeText for proper work of Add/GetComponent.

1.5.21:
    - Added Clamp01 for float, double and decimal types.

1.5.20:
    - Dramatically optimise prefab transform changes application.
    - Added menu entries for fast prefabs changes application.

1.5.19:
    - Improved History Cache: Allowed to create your own caches.
    - Added Force assets re-serialiser for only selected objects.
    - Added ability to apply transform changes to prefab for a bunch of objects at once. 

1.5.18:
    - Fixed an issue in ChildrenTransformFreezer with nested children.

1.5.17:
    - Added ChildrenTransformFreezer to keep children w/o changes while parent is transforming.
    - Added Transform.RotateAround using Quaternion.

1.5.16:
    - Just a version up to fix an NPM package issues. 

1.5.15:
    - Added IEnumerable.Any version, but with entries count.
    - Optimise Shuffle with elements decomposition.

1.5.14:
    - Added MonoBehaviour.TryKillCoroutine extension with ref clearing.

1.5.13:
    - Fixed an exception when ArrayElements got an empty object reference.

1.5.12:
    - Added EnumArrayOdds drawer to show odds from weights array.

1.5.11:
    - Added Random.Range01().

1.5.10:
    - Added Editor for the new Area component.

1.5.9:
    - Added Area component to define area and get random positions from it.

1.5.8:
    - Added Vector.Divide methods to apply a per-component division.

1.5.7:
    - Added TakeLast() LINQ extension.

1.5.6:
    - Added Random.Vector2/3 methods with from-to input.

1.5.5:
    - Fixed old Unity versions compatibility.
    - Added Clear methods for HistoryCaches.

1.5.4:
    - Added missed Mane namespace for HistoryCache classes.

1.5.3:
    - Added Vector3..., Vector2... and Float... HistoryCaches to store values changes and calculate normalizations. 

1.5.2:
    - Added List.InitWith, used capacity as a count.

1.5.1:
    - Added feature to the Force Reserealize Assets from the menu.
    - Improved GO state switching (F4) to work from multiple objects.

1.5:
    - A lot of helper classes was moved from Mane.Extensions to Mane namespace.
    - Animator helpers was moved from Mane to Mane.StateMachine namespace.
    - All inspector attributes was moved from Mane.Extensions to Mane.Inspector namespace.
    - Added EnumArray property drawer to bind enum values to array elements in inspector.

1.4.3:
    - Moved Random from Mane.Extensions.Random to Mane.Random.

1.4.2:
    - Added Collection.IsNullOrEmpty checker.
    - Renamed Tools.GetEnumValues<T> to Enum.GetValues<T>.

1.4.1:
    - Added Animator Randomizer to randomize animation clips inside the Mecanim state machine.
    - Added Parameter Resetter to reset any Paremeter directly from the Mecanim state machine.

1.4.0:
    - Added new Text Mesh component with Outline and Shadow support.
    - Replaced ReadOnlyIf attribute with AvailableIf. It's more flexible, can work with methods and Property getters, and able to hide fields completely.
    - Suppress build time warning in Sphere Gizmo.

1.3.18:
    - Added EditorDebug class for Logs only for Editor.

1.3.17:
    - Added Random Vector2 and Vector3 getters. 

1.3.16:
    - Added ReadOnlyIf attribute similar to the basic ReadOnly, but with the condition.

1.3.15:
    - Aligner: Added ability to init from a prefab.
    
1.3.14:
    - ArrayElements Attribute: Improved view for enums and fixed crash if there is no zero-valued enum item.

1.3.13:
    - Added ArrayElements Attribute to change "Element N" titles.
    - Tiny performance improvement for IsOdd / IsEven methods.

1.3.12:
    - Added Coroutine return to Delayed and DelayedFrames (to be able to cancel the exact operation).

1.3.11:
    - Added DelayedFrames MonoBehaviour extension to delay execution in frames.

1.3.10:
    - Added flag to Vector2Int.RandomBetween for inclusive range.

1.3.9:
    - Split BaseExtensions class to BaseClassesExtensions and Tools.
    - Added Delayed MonoBehaviour extension to delay actions.

1.3.8:
    - Added Aligner component to help you align item stacks with constant offset.

1.3.7:
    - Added workaround Extension GameObject.IsPrefab for the runtime.

1.3.6:
    - ScrollRect.SnapTo: Added offset for custom snapping + refactoring.

1.3.5:
    - Added SnapTo extension for the ScrollRect.

1.3.4:
    - ForEach collections extensions doesn't throw exceptions for null collections anymore.

1.3.3:
    - Added Random.FlipCoin() and Random.ThrowDice(d) methods for fast events randomization.

1.3.2:
    - Added RandomBetween extension for Vector2 and Vector2Int.

1.3.1:
    - Added SelfConcat collections extension that allows to concatenate collection w/ itself multiple times. 

1.3.0:
    - Added ToVectorN float extensions to ease vectors init with the same components.

1.2.8:
    - Added Postfix attribute (Property Drawer).

1.2.7:
    - Added npm packages.

1.2.6:
    - Renamed breakable ForEach() collection extension to ForEachCancellable() to prevent unnecessary calls 
        instead of plain ForEach() when action is an expression with single assignment 
        (what actually returns true for successful operation). 
    - Also reversed it to false!
    - CHANGELOG.md now contains full changelog, not only the last version. 
    
1.2.5: 
    - Vector2/3.Average() doesn't return NaN vectors anymore.
    
1.2.4:
    - Added option to IEnumerable.GetRandom() to allows getting extended lists with duplicates.
    
1.2.3:
    - Some improvements in Property Drawers.
    
1.2.2:
    - Added DropdownList attribute (Property Drawer). You can use with string or int values.
    
1.2.1:
    - Added fully-functional Layer attribute (Property Drawer).
    
1.2.0:
    - Added Average() extension for the Vectors collections.
    - Removed all C# 7.0+ expressions to make the code compatible with earlier version
        (full 7.0 translation will be applied later with Unity 2018 support drop) 
      
1.1.1:
    - Added Sphere Gizmo to help you position objects without graphics.
    
1.1.0:
    - Added ReadOnly attribute (Property Drawer) for inspector properties.
    
1.0.17:
    - Close polygon automatically in Vector3.IsInPolygon() extension without input points duplicating.
    
1.0.16:
    - Added Vector3.ClosestPointOnLine() extension.
    
1.0.15:
    - Added Vector3.IsInPolygon() extension to check is point inside convex polygon or not.
    
1.0.14:
    - Added float.Map() extension to remap value from one range to another.
    
1.0.13:
    - Added Gizmos class with Plane gizmo.
    
1.0.12:
    - Added Vector2.AddZ() extension, returning new Vector3.
    
1.0.11:
    - Added string Extension to Reverse() text.
    
1.0.10:
    - Added simplified ForeEach() extension loop for IEnumerable (w/o break).
    
1.0.9:
    - Fixed rotation issue within Transform.Reset() extension.
    
1.0.8:
    - Added Clear() method for arrays, that fills array with default values.
    
1.0.7:
    - Added Duplicate() method for GameObject itself, not only the Component.
    
1.0.6:
    - Added separate Translate() methods for Vector extensions.
    
1.0.5:
    - Added Duplicate() method for Unity Components (similar to Cmd+D but for scripts).
    
1.0.4:
    - Added RandomOrDefault<T>() method for IEnumerable<T>.
    
1.0.3:
    - Added GetRandom() and GetIndexOf() extensions for collection types.
        GetRandom() also includes variation for getting random sub-arrays without duplicates.
    - Added legacy support for Unity 2018.x and earlier (different branch in repo)
        
1.0.2:
    - You can count this as an INIT version.
    - Rearrange project depends on Unity guidelines.
    
1.0.1:
    - Basically the test version increase to check Package Manager things.
    
1.0.0:
    - Init repo + some stuff.