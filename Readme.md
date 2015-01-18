## Unity tutorial: Roll-a-ball F-Sharp

Unity is a multi-platform (3d) game engine.

This repository is a tutorial from Unity.
http://unity3d.com/learn/tutorials/projects/roll-a-ball/

The only difference is that F# programming language is used.
No neat functional source code, just plain translation.

It seems that Unity doesn't lock the dlls, so you can build new versions on the fly and still Unity keeps bindings which is great.

You have two options, a) either start from scratch or b) run this solution.
 
### How to make a similar solution from scratch

 * Install some version of Unity and a Visual Studio (or MonoDevelop Add-in F# language binding)
 * Create a new Unity project as usual
   (Instead of desktop I recommend to put the project something like c:\git\Roll-a-ball )
   [http://unity3d.com/learn/tutorials/projects/roll-a-ball/set-up] (http://unity3d.com/learn/tutorials/projects/roll-a-ball/set-up)
   But before proceeding to the second video...
 * With Visual Studio (or MonoDevelop or your favourite editor...) create a new F#-library under your Unity's project path. Un-tick the "Create directory for solution" to save one directory. (I used c:\git\Roll-a-ball and created project called GameLogic.) 
 * Add references to UnityEngine.dll (I did have it in C:\Program Files (x86)\Unity\Editor\Data\Managed\ )
 * From project properties, set the build "Output path" to some folder under Unity's Assets-folder, for example: ..\Assets\bin\
 * It seems that Unity 4.x still uses .NET 3.5 (or 2.0) so it is better to change the target framework to .NET 3.5. (I don't know what is the MonoBleedingEdge-folder under Unity, maybe they will update soon...)
 * If you want to be sure to use same components as Unity, replace also mscorlib.dll, System.dll, System.Core.dll from the ones that are found under Unity's folder structure (I have those in C:\Program Files (x86)\Unity\Editor\Data\Mono\lib\mono\2.0\ )
 * For FSharp.Core, the copy-local should be "true" as Unity doesn't have it. These other core-level dll's should have that "false".
 * Write some script, e.g. like this:

```

namespace RollABall
open UnityEngine

type PlayerController() =
    inherit MonoBehaviour()

    [<SerializeField>]
    let mutable speed = 6.0f

    member x.FixedUpdate () =
        let ``move horizontal`` = Input.GetAxis("Horizontal");
        let ``move vertical`` = Input.GetAxis("Vertical")

        let movement = Vector3(``move horizontal``, 0.0f, ``move vertical``)
        movement * speed * Time.deltaTime
        |> x.rigidbody.AddForce

```

 * Now, build in Visual Studio, then switch to Unity and open bin-folder from Assets. From Project-window, you should see your fsharp-dll and a little arrow on its right side to extend the details. When you press the arrow, you see the class(es) inside the component and you can directly drag and drop your class to your game object (like the ball in this tutorial). 
 * Then just continue the tutorial, but instead of creating every snippet a C#-file, just code F#, build the solution (in VS) and Unity notices the new modifications on the fly.

### How to get this repository code running

 * Install Unity (4.6.1-...)
 * Install Visual Studio 2015 (CTP)
 * Open and build GameLogic\GameLogic.sln with Visual Studio. (Leave VS open...)
 * Open project with Unity: Roll-a-ball-FSharp (Yes, the whole folder).
 * Open Scene: _Scenes\MiniGame.unity
 * For some reason Unity may have lost the references. So you have to map those once: from Hierarcy-tab select Player, then from Inspector-tab under Player Controller (Script) select the small circular button next to Script-textbox. A dialog opens then select PlayerController from the list. Do the same for (Hierarchy-tab again) Main Camera, associate it (from Inspector-tab again) to CameraController. Then from Project-tab under Assets go to Prefabs and select PickUp (Cube-icon). From the Inspector-tab select Rotator (Script) and assign it to Rotator. That's it.
 
### Links

 * [Unity] (http://unity3d.com)
 * [How to use F# libraries with Unity] https://github.com/eriksvedang/FSharp-Unity
