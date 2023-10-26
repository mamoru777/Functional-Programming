open System
open System.Drawing

let rec lSystemIteration (iterations: int) (start: string) =
    let rec iterate (iteration: int) (current: seq<char>) =
        if iteration = iterations then
            String(current |> Seq.toArray)
        else
            let nextIteration = 
                current
                |> Seq.collect (fun c ->
                    match c with
                    | 'F' -> "+FF-FF-FF+"
                    | _ -> string c
                )

            iterate (iteration + 1) nextIteration
    iterate 0 (start.ToCharArray() |> Seq.toList)

let iterations = 3 // Количество итераций
let startString = "+FF-FF-FF+"

let result = lSystemIteration iterations startString
printfn "Результат L-системы после %d итераций: %s" iterations result

// Создание изображения для отрисовки графика
let width = 800
let height = 600
let image = new Bitmap(width, height)
use graphics = Graphics.FromImage(image)
let x = ref (float32 (width / 2))
let y = ref (float32 (height - 50))
let angle = ref 0.0

// Интерпретация команд и рисование графика
for command in result do
    match command with
    | 'F' ->
        let x1 = !x + cos (!angle * (float32)Math.PI / 180.0)
        let y1 = !y + sin (!angle * (float32)Math.PI / 180.0)
        let point1 = new PointF(!x, !y)
        let point2 = new PointF(x1, y1)
        graphics.DrawLine(Pens.Black, point1, point2)
        x := x1
        y := y1
    | '+' -> angle := !angle + 60.0
    | '-' -> angle := !angle - 60.0
    | _ -> ()

// Сохранение изображения в файл
let imagePath = "LSystem.png"
image.Save(imagePath)

Console.WriteLine("Изображение сохранено в файл: %s" imagePath