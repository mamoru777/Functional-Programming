open System.IO
open System.Text.RegularExpressions

// Функция для чтения текста из файла
let readTextFromFile (filePath: string) =
    use reader = File.OpenText(filePath)
    reader.ReadToEnd()

// Фопция для определения самого часто встречающегося слова в тексте
let findMostCommonWord (text: string) =
    let wordPattern = @"\b\w+\b" // Регулярное выражение для слов
    let matches = Regex.Matches(text, wordPattern)

    let wordCounts =
        matches
        |> Seq.cast<Match>
        |> Seq.map (fun m -> m.Value)
        |> Seq.groupBy id
        |> Seq.map (fun (word, group) -> (word, Seq.length group))
        |> Seq.sortByDescending (fun (_, count) -> count)
    
    let mostCommonWord, mostCommonCount = Seq.head wordCounts
    
    mostCommonWord, mostCommonCount

let textFileName = "text_file.txt"
let text = readTextFromFile textFileName
let mostCommonWord, mostCommonCount = findMostCommonWord text

printfn "Самое часто встречающееся слово: %s (встречается %d раз)" mostCommonWord mostCommonCount
