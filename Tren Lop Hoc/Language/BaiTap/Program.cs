namespace BaiTap1
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var sinhVien = new
            {
                MSSV = "CE180905",
                TenSV = "Le Anh Tuan",
                NamSinh = 2004
            };
            Console.WriteLine($"MSSV: {sinhVien.MSSV}");
            Console.WriteLine($"Ten sinh vien: {sinhVien.TenSV}");
            Console.WriteLine($"Nam sinh: {sinhVien.NamSinh}");
        }
    }
}
