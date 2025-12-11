using System.Collections;
using System.Globalization;

namespace AnhDemon
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            Console.WriteLine("=== DEMO TAT CA CAC LOAI SORT TRONG C# ===\n");

            // === 1. ARRAY SORT ===
            Console.WriteLine("1. ARRAY SORT:");
            DemoArraySort();
            Console.WriteLine();

            // === 2. LIST SORT ===
            Console.WriteLine("2. LIST SORT:");
            DemoListSort();
            Console.WriteLine();

            // === 3. LINQ SORT ===
            Console.WriteLine("3. LINQ SORT:");
            DemoLinqSort();
            Console.WriteLine();

            // === 4. CUSTOM COMPARER SORT ===
            Console.WriteLine("4. CUSTOM COMPARER SORT:");
            DemoCustomComparerSort();
            Console.WriteLine();

            // === 5. ADVANCED SORTING ===
            Console.WriteLine("5. ADVANCED SORTING:");
            DemoAdvancedSorting();
            Console.WriteLine();

            // === 6. PERFORMANCE COMPARISON ===
            Console.WriteLine("6. PERFORMANCE COMPARISON:");
            DemoPerformanceComparison();
            Console.WriteLine();

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        // === ARRAY SORT METHODS ===
        static void DemoArraySort()
        {
            // Sort basic array
            int[] numbers = { 64, 34, 25, 12, 22, 11, 90 };
            Console.WriteLine($"Original: [{string.Join(", ", numbers)}]");
            
            Array.Sort(numbers);
            Console.WriteLine($"Array.Sort(): [{string.Join(", ", numbers)}]");

            // Sort with custom comparison
            string[] names = { "John", "Alice", "Bob", "Charlie", "Diana" };
            Console.WriteLine($"Names original: [{string.Join(", ", names)}]");
            
            Array.Sort(names, StringComparer.OrdinalIgnoreCase);
            Console.WriteLine($"Array.Sort(StringComparer): [{string.Join(", ", names)}]");

            // Sort descending
            Array.Sort(numbers, (x, y) => y.CompareTo(x));
            Console.WriteLine($"Descending: [{string.Join(", ", numbers)}]");

            // Sort by key and value
            string[] keys = { "C", "A", "B" };
            int[] values = { 3, 1, 2 };
            Array.Sort(keys, values);
            Console.WriteLine($"Sort by keys: Keys[{string.Join(", ", keys)}], Values[{string.Join(", ", values)}]");
        }

        // === LIST SORT METHODS ===
        static void DemoListSort()
        {
            var numbers = new List<int> { 64, 34, 25, 12, 22, 11, 90 };
            Console.WriteLine($"Original: [{string.Join(", ", numbers)}]");

            // Basic sort
            numbers.Sort();
            Console.WriteLine($"List.Sort(): [{string.Join(", ", numbers)}]");

            // Sort with comparison
            numbers.Sort((x, y) => y.CompareTo(x));
            Console.WriteLine($"Descending: [{string.Join(", ", numbers)}]");

            // Sort with IComparer
            var students = new List<Student>
            {
                new("Alice", 85),
                new("Bob", 92),
                new("Charlie", 78),
                new("Diana", 96)
            };
            
            Console.WriteLine("Students by name:");
            students.Sort(new StudentNameComparer());
            foreach (var s in students)
                Console.WriteLine($"  {s}");

            Console.WriteLine("Students by grade:");
            students.Sort(new StudentGradeComparer());
            foreach (var s in students)
                Console.WriteLine($"  {s}");
        }

        // === LINQ SORT METHODS ===
        static void DemoLinqSort()
        {
            var numbers = new[] { 64, 34, 25, 12, 22, 11, 90 };
            var students = new[]
            {
                new Student("Alice", 85),
                new Student("Bob", 92),
                new Student("Charlie", 78),
                new Student("Diana", 96)
            };

            // OrderBy / OrderByDescending
            var ascending = numbers.OrderBy(x => x);
            var descending = numbers.OrderByDescending(x => x);
            Console.WriteLine($"OrderBy: [{string.Join(", ", ascending)}]");
            Console.WriteLine($"OrderByDescending: [{string.Join(", ", descending)}]");

            // ThenBy for multiple criteria
            var sortedStudents = students
                .OrderBy(s => s.Grade)
                .ThenBy(s => s.Name);
            Console.WriteLine("OrderBy Grade, ThenBy Name:");
            foreach (var s in sortedStudents)
                Console.WriteLine($"  {s}");

            // Sort with custom key selector
            var byNameLength = students.OrderBy(s => s.Name.Length).ThenBy(s => s.Name);
            Console.WriteLine("OrderBy Name Length, ThenBy Name:");
            foreach (var s in byNameLength)
                Console.WriteLine($"  {s}");
        }

        // === CUSTOM COMPARER SORT ===
        static void DemoCustomComparerSort()
        {
            var products = new List<Product>
            {
                new("Laptop", 1500.99m, "Electronics"),
                new("Book", 25.50m, "Education"),
                new("Phone", 899.99m, "Electronics"),
                new("Pen", 2.99m, "Office"),
                new("Tablet", 499.99m, "Electronics")
            };

            Console.WriteLine("Original products:");
            products.ForEach(p => Console.WriteLine($"  {p}"));

            // Sort by multiple criteria using custom comparer
            products.Sort(new ProductComparer());
            Console.WriteLine("\nSorted by Category, then Price:");
            products.ForEach(p => Console.WriteLine($"  {p}"));

            // Using Comparison delegate
            products.Sort((p1, p2) =>
            {
                int categoryComparison = p1.Category.CompareTo(p2.Category);
                return categoryComparison != 0 ? categoryComparison : p2.Price.CompareTo(p1.Price);
            });
            Console.WriteLine("\nSorted by Category, then Price (desc):");
            products.ForEach(p => Console.WriteLine($"  {p}"));
        }

        // === ADVANCED SORTING ===
        static void DemoAdvancedSorting()
        {
            // Stable sort vs unstable sort
            var data = new List<(string Name, int Score, int Id)>
            {
                ("Alice", 85, 1),
                ("Bob", 85, 2),
                ("Charlie", 85, 3),
                ("Diana", 90, 4)
            };

            Console.WriteLine("Stable sort (LINQ OrderBy - maintains relative order):");
            var stableSort = data.OrderBy(x => x.Score).ToList();
            stableSort.ForEach(x => Console.WriteLine($"  {x}"));

            // Custom stable sort implementation
            Console.WriteLine("\nManual stable sort by Score, maintaining ID order:");
            var manualStableSort = data
                .Select((item, index) => new { Item = item, OriginalIndex = index })
                .OrderBy(x => x.Item.Score)
                .ThenBy(x => x.OriginalIndex)
                .Select(x => x.Item)
                .ToList();
            manualStableSort.ForEach(x => Console.WriteLine($"  {x}"));

            // Sort with null values
            var nullableStrings = new[] { "banana", null, "apple", "cherry", null, "date" };
            var sortedWithNulls = nullableStrings
                .OrderBy(x => x, Comparer<string>.Create((x, y) =>
                {
                    if (x == null && y == null) return 0;
                    if (x == null) return -1; // nulls first
                    if (y == null) return 1;
                    return x.CompareTo(y);
                }));
            Console.WriteLine($"\nWith nulls first: [{string.Join(", ", sortedWithNulls.Select(x => x ?? "null"))}]");

            // Culture-sensitive sort
            var words = new[] { "café", "cave", "résumé", "resume", "naïve", "naive" };
            Console.WriteLine("\nCulture-sensitive sorting:");
            var culturalSort = words.OrderBy(x => x, StringComparer.Create(CultureInfo.CurrentCulture, true));
            Console.WriteLine($"Cultural: [{string.Join(", ", culturalSort)}]");
        }

        // === PERFORMANCE COMPARISON ===
        static void DemoPerformanceComparison()
        {
            const int size = 100000;
            var random = new Random(42); // Fixed seed for consistent results

            // Generate test data
            var arrayData = Enumerable.Range(0, size).Select(_ => random.Next(1000)).ToArray();
            var listData = arrayData.ToList();

            var sw = System.Diagnostics.Stopwatch.StartNew();

            // Array.Sort
            var arrayCopy = (int[])arrayData.Clone();
            sw.Restart();
            Array.Sort(arrayCopy);
            Console.WriteLine($"Array.Sort({size:N0} elements): {sw.ElapsedMilliseconds}ms");

            // List.Sort
            var listCopy = new List<int>(listData);
            sw.Restart();
            listCopy.Sort();
            Console.WriteLine($"List.Sort({size:N0} elements): {sw.ElapsedMilliseconds}ms");

            // LINQ OrderBy
            sw.Restart();
            var linqResult = arrayData.OrderBy(x => x).ToArray();
            Console.WriteLine($"LINQ OrderBy({size:N0} elements): {sw.ElapsedMilliseconds}ms");

            // ParallelQuery (PLINQ)
            sw.Restart();
            var plinqResult = arrayData.AsParallel().OrderBy(x => x).ToArray();
            Console.WriteLine($"PLINQ OrderBy({size:N0} elements): {sw.ElapsedMilliseconds}ms");

            Console.WriteLine("\nNote: Array.Sort và List.Sort thường nhanh nhất cho dữ liệu lớn");
            Console.WriteLine("LINQ linh hoạt hơn nhưng có overhead");
            Console.WriteLine("PLINQ tốt cho dữ liệu rất lớn và phép so sánh phức tạp");
        }
    }

    // === SUPPORT CLASSES ===
    public record Student(string Name, int Grade)
    {
        public override string ToString() => $"{Name}: {Grade}";
    }

    public record Product(string Name, decimal Price, string Category)
    {
        public override string ToString() => $"{Name} - ${Price:F2} ({Category})";
    }

    // === CUSTOM COMPARERS ===
    public class StudentNameComparer : IComparer<Student>
    {
        public int Compare(Student? x, Student? y)
        {
            if (x == null && y == null) return 0;
            if (x == null) return -1;
            if (y == null) return 1;
            return x.Name.CompareTo(y.Name);
        }
    }

    public class StudentGradeComparer : IComparer<Student>
    {
        public int Compare(Student? x, Student? y)
        {
            if (x == null && y == null) return 0;
            if (x == null) return -1;
            if (y == null) return 1;
            return y.Grade.CompareTo(x.Grade); // Descending by grade
        }
    }

    public class ProductComparer : IComparer<Product>
    {
        public int Compare(Product? x, Product? y)
        {
            if (x == null && y == null) return 0;
            if (x == null) return -1;
            if (y == null) return 1;
            
            // First by category, then by price ascending
            int categoryComparison = x.Category.CompareTo(y.Category);
            return categoryComparison != 0 ? categoryComparison : x.Price.CompareTo(y.Price);
        }
    }
}
