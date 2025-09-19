using System;
using System.Collections.Generic;
using System.Linq;

class Student
{
    public string Id { get; set; }
    public string Name { get; set; }
    public int Semester { get; set; }
    public string CourseName { get; set; }
}

class Program
{
    static List<Student> students = new List<Student>();
    static readonly string[] allowedCourses = { "Java", ".Net", "C/C++" };

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("\n      MENU     ");
            Console.WriteLine("1. Nhap danh sach sinh vien");
            Console.WriteLine("2. Tim kiem sinh vien");
            Console.WriteLine("3. Sua thong tin sinh vien");
            Console.WriteLine("4. Xoa sinh vien");
            Console.WriteLine("5. Thong ke dang ky khoa hoc");
            Console.WriteLine("0. Thoat");
            Console.Write("Chon chuc nang: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1": InputStudents(); break;
                case "2": SearchStudent(); break;
                case "3": UpdateStudent(); break;
                case "4": DeleteStudent(); break;
                case "5": Report(); break;
                case "0": return;
                default: Console.WriteLine("Lua chon khong hop le!"); break;
            }
        }
    }
    static void InputStudents()
    {
        Console.Write("Nhap so luong sinh vien: ");
        int n = int.Parse(Console.ReadLine());
        for (int i = 0; i < n; i++)
        {
            Console.WriteLine($"\nNhap thong tin sinh vien thu {i + 1}:");
            Student st = new Student();

            Console.Write("ID: ");
            st.Id = Console.ReadLine();

            Console.Write("Name: ");
            st.Name = Console.ReadLine();

            Console.Write("Semester: ");
            st.Semester = int.Parse(Console.ReadLine());

        
            string course;
            do
            {
                Console.Write("Course (Java, .Net, C/C++): ");
                course = Console.ReadLine();
            } while (!allowedCourses.Contains(course));
            st.CourseName = course;

            students.Add(st);
        }
    }
    static void SearchStudent()
    {
        Console.Write("Nhap Id hoac Name can tim: ");
        string keyword = Console.ReadLine();

        var result = students.Where(s => s.Id == keyword || s.Name.Contains(keyword)).ToList();

        if (result.Count == 0) Console.WriteLine("Khong tim thay!");
        else
        {
            Console.WriteLine("\nDanh sach tim duoc:");
            foreach (var st in result)
                Console.WriteLine($"{st.Id} | {st.Name} | Semester {st.Semester} | {st.CourseName}");
        }
    }
    static void UpdateStudent()
    {
        Console.Write("Nhap Id sinh vien muon sua: ");
        string id = Console.ReadLine();
        var st = students.FirstOrDefault(s => s.Id == id);

        if (st == null) Console.WriteLine("Khong tim thay!");
        else
        {
            Console.Write("Name moi (bo trong neu giu nguyen): ");
            string name = Console.ReadLine();
            if (!string.IsNullOrEmpty(name)) st.Name = name;

            Console.Write("Semester moi (bo trong neu giu nguyen): ");
            string sem = Console.ReadLine();
            if (!string.IsNullOrEmpty(sem)) st.Semester = int.Parse(sem);

            Console.Write("Course moi (Java, .Net, C/C++ – bo trong neu giu nguyen): ");
            string course = Console.ReadLine();
            if (!string.IsNullOrEmpty(course) && allowedCourses.Contains(course)) st.CourseName = course;

            Console.WriteLine("Da cap nhat!");
        }
    }
    static void DeleteStudent()
    {
        Console.Write("Nhap Id sinh vien muon xoa: ");
        string id = Console.ReadLine();
        var st = students.FirstOrDefault(s => s.Id == id);

        if (st == null) Console.WriteLine("Khong tim thay!");
        else
        {
            students.Remove(st);
            Console.WriteLine("Da xoa sinh vien!");
        }
    }
    static void Report()
    {
        var report = students
            .GroupBy(s => new { s.Name, s.CourseName })
            .Select(g => new
            {
                StudentName = g.Key.Name,
                Course = g.Key.CourseName,
                Total = g.Count()
            });

        Console.WriteLine("\nStudent Name | Course | Total of Course");
        foreach (var r in report)
        {
            Console.WriteLine($"{r.StudentName} | {r.Course} | {r.Total}");
        }
    }
}
