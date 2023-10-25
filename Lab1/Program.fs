type IDekanatEmployee =
    abstract member GetName : unit -> string
    abstract member GetPosition : unit -> string

type DekanatEmployee(name: string, position: string) =
    interface IDekanatEmployee with
        member this.GetName () = name
        member this.GetPosition () = position

type Student(name: string, major: string) =
    inherit DekanatEmployee(name, "Студент")
    member this.GetMajor () = major

    interface IDekanatEmployee with
        member this.GetName () = name
        member this.GetPosition () = "Студент"

type Teacher(name: string, department: string) =
    inherit DekanatEmployee(name, "Преподаватель")
    member this.GetDepartment () = department

    interface IDekanatEmployee with
        member this.GetName () = name
        member this.GetPosition () = "Преподаватель"

// Пример использования
let student = new Student("Иван", "Математика")
let teacher = new Teacher("Анна", "Физика")

let printEmployeeInfo (employee: IDekanatEmployee) =
    printfn "Сотрудник: %s, Должность: %s" (employee.GetName ()) (employee.GetPosition ())

printEmployeeInfo student
printEmployeeInfo teacher