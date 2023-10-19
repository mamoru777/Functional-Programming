// Интерфейс для сотрудников деканата
type IDekanatEmployee =
    abstract member GetName : unit -> string
    abstract member GetPosition : unit -> string

// Базовый класс для сотрудников деканата
type DekanatEmployee(name: string, position: string) =
    interface IDekanatEmployee with
        member this.GetName () = name
        member this.GetPosition () = position

// Класс для студентов
type Student(name: string, major: string) =
    inherit DekanatEmployee(name, "Студент")
    member this.GetMajor () = major
    //member this.GetName () = name

// Класс для преподавателей
type Teacher(name: string, department: string) =
    inherit DekanatEmployee(name, "Преподаватель")
    member this.GetDepartment () = department

// Пример использования
let student = new Student("Иван", "Математика")
let teacher = new Teacher("Анна", "Физика")

printfn "Студент: %s, Отделение: %s" (student.GetName ()) (student.GetMajor ())
printfn "Преподаватель: %s, Отделение: %s" (teacher.GetName ()) (teacher.GetDepartment ())
