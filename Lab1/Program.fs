type IItem = 
    abstract member Trade : unit -> unit

type IOwner = 
    abstract member Wage : int -> unit
    abstract member Employ : unit -> unit
    abstract member Sack : unit -> unit

type Game(price : int) =
    let price = price
    interface IItem with
        member this.Trade() = printf "Игра продана по стоимости %d" price

type Assistant(FIO : string) =
    let FIO = FIO
    do printfn "%s зашел в магазин " FIO

    interface IOwner with
        member this.Wage salary = printfn "Продавец %s получил зарплату %d" FIO salary
        member this.Employ() = printfn "%s получил работу" FIO
        member this.Sack() = printfn "Продавец %s был уволен" FIO

    abstract member StartWork : unit -> unit

    override this.StartWork() = printfn "Продавец %s начал работу" FIO
    
    member this.SellItem item = (item :> IItem).Trade()
let assistant = Assistant("Евгений Борисович")
    
(assistant :> IOwner).Employ()
    
assistant.StartWork()   
    
let game = Game(4599)
assistant.SellItem game
0