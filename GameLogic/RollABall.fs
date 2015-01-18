// Because project is so small, only one script file is needed.
namespace RollABall
open UnityEngine

type PlayerController() =
    inherit MonoBehaviour()

    [<SerializeField>]
    let mutable speed = 500.0f
    [<SerializeField>]
    let countText = Unchecked.defaultof<GUIText>
    [<SerializeField>]
    let winText = Unchecked.defaultof<GUIText>
    let mutable count = 0

    let ``set count text`` () =
        countText.text <- "Count: " + count.ToString()
        if count >= 12 then
            winText.text <- "YOU WIN!"

    member x.Start () =
        count <- 0
        ``set count text`` ()
        winText.text <- ""

    member x.FixedUpdate () =
        let ``move horizontal`` = Input.GetAxis("Horizontal");
        let ``move vertical`` = Input.GetAxis("Vertical")

        let movement = Vector3(``move horizontal``, 0.0f, ``move vertical``)
        movement * speed * Time.deltaTime
        |> x.rigidbody.AddForce

    member x.OnTriggerEnter (other:Collider) = 
        match other.gameObject.tag with
        | "PickUp" -> 
            other.gameObject.SetActive false
            count <- count + 1
            ``set count text`` ()
        | _ -> ()

type CameraController() =
    inherit MonoBehaviour()

    [<SerializeField>]
    let mutable player = Unchecked.defaultof<GameObject>
    let mutable offset = None

    member x.Start () =
        offset <- Some(x.transform.position)

    member x.LateUpdate () =
       match offset with 
       | Some(o) -> x.transform.position <- player.transform.position + o
       | None -> ()

type Rotator() =
    inherit MonoBehaviour()

    member x.Update () =
        Vector3(15.0f, 30.0f, 45.0f) * Time.deltaTime |> x.transform.Rotate