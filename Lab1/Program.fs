open System
open SkiaSharp
open System.Collections

type LSystemCommand =
    | Forward
    | TurnLeft
    | TurnRight
    | Push
    | Pop

type State = { X: float32; Y: float32; Angle: float32 }

let lSystemRules (input: char) =
    match input with
    | 'F' -> "F+F--F+F"
    | '+' -> "+"
    | '-' -> "-"
    | '[' -> "["
    | ']' -> "]"
    | _   -> ""

let rec applyLSystemRules (input: string) (depth: int) =
    if depth = 0 then
        input
    else
        let mutable result = ""
        for i = 0 to input.Length - 1 do
            result <- result + lSystemRules (input.[i])
        applyLSystemRules result (depth - 1)

let executeLSystem (input: string) (maxDepth: int) =
    let finalString = applyLSystemRules input maxDepth
    let mutable state = { X = 0.0f; Y = 0.0f; Angle = 0.0f }
    let mutable stateStack = []
    let mathPi1 = float32 Math.PI
    let mathPi2 = float32 Math.PI
    // Создаем изображение
    let surface = new SKBitmap(800, 600)
    use canvas = new SKCanvas(surface)

    for i = 0 to finalString.Length - 1 do
        match finalString.[i] with
        | 'F' -> 
            let newX = state.X + 10.0f * cos state.Angle
            let newY = state.Y + 10.0f * sin state.Angle
            canvas.DrawLine(state.X, state.Y, newX, newY, new SKPaint())
            state <- { state with X = newX; Y = newY }
        | '+' -> state <- { state with Angle = state.Angle + (mathPi1 / 3.0f) }
        | '-' -> state <- { state with Angle = state.Angle - (mathPi2 / 3.0f) }
        | '[' -> stateStack <- state :: stateStack
        | ']' -> 
            match stateStack with
            | [] -> ()  // Handle empty stack
            | hd :: tl ->
                state <- hd
                stateStack <- tl
        | _ -> ()

    // Сохраняем изображение
    use imageStream = new SKFileWStream("LSystem.png")
    surface.Encode(imageStream, SKEncodedImageFormat.Png, 100)

executeLSystem "F+F--F+F" 4