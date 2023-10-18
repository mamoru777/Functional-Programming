let replaceWithZeroIfOddRight (arr: int[]) =
    let mutable modified = false
    let length = Array.length arr
    for i = 0 to length - 2 do
        if arr.[i + 1] % 2 <> 0 then
            arr.[i] <- 0
            modified <- true
    if modified then
        arr
    else
        arr.[length - 1] <- 0
        arr

// Пример использования
let arr = [|1; 3; 4; 5; 6; 7; 9|]
let firstarr = [|1; 3; 4; 5; 6; 7; 9|]
let modifiedArr = replaceWithZeroIfOddRight arr
printfn "Исходный массив: %A" firstarr
printfn "Измененный массив: %A" modifiedArr
