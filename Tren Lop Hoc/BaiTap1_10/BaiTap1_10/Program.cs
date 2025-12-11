using System.Text;

namespace BaiTap1_10
{
    class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Major { get; set; }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            // Tạo danh sách sinh viên
            List<Student> students = new List<Student>
            {
                new Student{ Id = 1, Name = "An", Age = 19, Major = "IT"},
                new Student{ Id = 2, Name = "Bình", Age = 22, Major = "Business"},
                new Student{ Id = 3, Name = "Chi", Age = 21, Major = "IT"},
                new Student{ Id = 4, Name = "Dũng", Age = 20, Major = "Design"},
                new Student{ Id = 5, Name = "Hạnh", Age = 23, Major = "IT"},
                new Student{ Id = 6, Name = "Lan", Age = 18, Major = "Business"}
            };

            // --- Method Syntax ---
            var tuoiHon20_Method = students.Where(s => s.Age > 20).ToList();
            var chuyenNganhIT_Method = students.Where(s => s.Major == "IT").ToList();
            var sapXepTheoTen_Method = students.OrderBy(s => s.Name).ToList();

            // --- Query Syntax ---
            var tuoiHon20_Query = (from s in students
                                   where s.Age > 20
                                   select s).ToList();

            var chuyenNganhIT_Query = (from s in students
                                       where s.Major == "IT"
                                       select s).ToList();

            var sapXepTheoTen_Query = (from s in students
                                       orderby s.Name
                                       select s).ToList();

            // Xuất kết quả
            Console.WriteLine("=== Method Syntax ===");
            Console.WriteLine("Sinh viên tuổi > 20: " + string.Join(", ", tuoiHon20_Method.Select(s => s.Name)));
            Console.WriteLine("Sinh viên ngành IT: " + string.Join(", ", chuyenNganhIT_Method.Select(s => s.Name)));
            Console.WriteLine("Sinh viên sắp xếp theo tên: " + string.Join(", ", sapXepTheoTen_Method.Select(s => s.Name)));

            Console.WriteLine("\n=== Query Syntax ===");
            Console.WriteLine("Sinh viên tuổi > 20: " + string.Join(", ", tuoiHon20_Query.Select(s => s.Name)));
            Console.WriteLine("Sinh viên ngành IT: " + string.Join(", ", chuyenNganhIT_Query.Select(s => s.Name)));
            Console.WriteLine("Sinh viên sắp xếp theo tên: " + string.Join(", ", sapXepTheoTen_Query.Select(s => s.Name)));
        }
    }
}
