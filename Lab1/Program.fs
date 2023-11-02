open System.IO 
open System.Threading.Tasks 
open System.Linq 
open System.Collections.Generic 
open System 
open System.Text 
 
let readFileAsync (filepath : string) : Async<KeyValuePair<KeyValuePair<string, string>, int>> = 
    async { 
        try 
            let wordFrequency = Dictionary<string, int>() 
 
            use stream = new StreamReader(Path.Join(__SOURCE_DIRECTORY__, filepath), Encoding.UTF8) 
            while not stream.EndOfStream do 
                let line = stream.ReadLine() 
                if (line <> null) then 
                    for word in line.Split([|' '; '\t'|], StringSplitOptions.RemoveEmptyEntries) do 
                        let lowerWord = word.ToLower() 
                        if wordFrequency.ContainsKey(lowerWord) then 
                            wordFrequency.[lowerWord] <- wordFrequency.[lowerWord] + 1 
                        else 
                            wordFrequency.Add(lowerWord, 1) 
             
            let leastFrequentWord = wordFrequency |> Seq.minBy (fun kvp -> kvp.Value) 
            let innerPair = new KeyValuePair<string, string>(filepath, leastFrequentWord.Key) 
            return new KeyValuePair<KeyValuePair<string, string>, int>(innerPair, leastFrequentWord.Value) 
        with 
            | :? FileNotFoundException as ex -> 
                printfn "File not found: %s" ex.FileName 
                return KeyValuePair<KeyValuePair<string, string>, int>(KeyValuePair<string, string>("ошибка", "ошибка"), 0) 
            | :? IOException as ex -> 
                printfn "IO error: %s" ex.Message 
                return KeyValuePair<KeyValuePair<string, string>, int>(KeyValuePair<string, string>("ошибка", "ошибка"), 0) 
            | ex -> 
                printfn "An unexpected error occurred: %s" ex.Message 
                return KeyValuePair<KeyValuePair<string, string>, int>(KeyValuePair<string, string>("ошибка", "ошибка"), 0) 
    } 
 
let findLeastFrequentWordAsync (filepaths : string list) : Async<unit> = 
    async { 
        let! wordFrequencies = Async.Parallel(filepaths |> List.map readFileAsync) 
 
        let outputPath = "result.txt" 
        use writer = new StreamWriter(outputPath) 
 
        for wf in wordFrequencies do 
            writer.WriteLine($"{Path.GetFileName(wf.Key.Key)} - {wf.Key.Value} - {wf.Value}") 
 
        Console.WriteLine("Результаты записаны в файл: " + outputPath) 
    } 
 
     
let filepaths = [ "harry_potter.txt"; "voyna-i-mir-tom-x8.txt"; "vlastelin-kolec.txt" ] 
 
let leastFrequentWord = Async.RunSynchronously (findLeastFrequentWordAsync filepaths) 
