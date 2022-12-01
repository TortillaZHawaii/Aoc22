printfn "Hello from F#"

let getElvesInventoryFromFile path =
    let lines = System.IO.File.ReadAllLines path
    let emptyLineIndexes = lines |> Array.indexed |> Array.filter (fun (i, line) -> line = "") |> Array.map fst
    let emptyLineIndexes = Array.concat [ [| 0 |]; emptyLineIndexes; [| lines.Length |] ]
    let splitByEmptyLines = emptyLineIndexes[.. emptyLineIndexes.Length - 2] |> Array.indexed |> Array.map (fun (i, _) -> lines.[emptyLineIndexes.[i]..emptyLineIndexes.[i + 1]])
    // map result to int
    splitByEmptyLines |> Array.map (fun elf -> elf |> Array.map (fun s -> int s))
 
let findResult1 (elves : int[][]) =
    elves |> Array.map (fun elf -> elf |> Array.sum) |> Array.max

"input.txt" |> getElvesInventoryFromFile |> findResult1 |> printfn "Result 1: %d"
