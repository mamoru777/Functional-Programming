let isNotMultipleOf3AndMultipleOf5 (n: int) =
    n % 3 <> 0 && n % 5 = 0

let numbers = seq { 1..100 }

let result =
    numbers
    |> Seq.filter isNotMultipleOf3AndMultipleOf5
    |> Seq.sum

printfn "Сумма чисел, некратных 3 и кратных 5, от 1 до 100: %d" result