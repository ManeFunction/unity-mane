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