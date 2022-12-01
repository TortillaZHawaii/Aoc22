printfn "Hello from F#"

let getElvesInventoryFromFile path =
    let lines = System.IO.File.ReadAllLines path
    let emptyLineIndexes = lines |> Array.indexed |> Array.filter (fun (i, line) -> line = "") |> Array.map fst
    let splitByEmptyLines = emptyLineIndexes[.. emptyLineIndexes.Length - 2] |> Array.indexed |> Array.map (fun (i, _) -> lines.[i..(emptyLineIndexes.[i + 1] - 1)])
    // map result to int
    splitByEmptyLines |> Array.map (fun elf -> elf |> Array.map (fun s -> int s))
 
let findResult1 (elves : int[][]) =
    elves |> Array.map (fun elf -> elf |> Array.sum) |> Array.max

