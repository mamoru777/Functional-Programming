type IItem = 
    abstract member Trade : unit -> unit
    abstract member Buy : unit -> unit

type IOwner = 
    abstract member Wage : int -> unit
    abstract member Employ : unit -> unit
    abstract member Sack : unit -> unit

type Game(price : int) =
    let price = price
    interface IItem with
        member this.Trade() = printfn "Игра продана по стоимости %d" price
         member this.Buy() = printfn "Игра куплена покупателем по стоимости %d" price

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

type Buyer(FIO : string) =
    let FIO = FIO
    do printfn "покупатель %s зашел в магазин " FIO
    member this.BuyItem item = (item :> IItem).Buy()

let assistant = Assistant("Евгений Борисович")
  
(assistant :> IOwner).Employ()
    
assistant.StartWork()   

let buyer = Buyer("Александр Евгеньевич")      
let game = Game(4599)
assistant.SellItem game
buyer.BuyItem game
