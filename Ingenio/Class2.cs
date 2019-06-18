using Solution;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenio
{
    [Export(typeof(ISolution))]
    [ExportMetadata("Name", nameof(Class2))]
    class Class2 : ISolution
    {
        public int[] solution(int X)
        {
            var data = new[]
            {
                new
                {
                    CategoryId = 100,
                    ParentCategoryId = -1,
                    Name = "Business",
                    Keywords = "Money",
                },
                new
                {
                    CategoryId = 200,
                    ParentCategoryId = -1,
                    Name = "Tutoring",
                    Keywords = "Teaching",
                },
                new
                {
                    CategoryId = 101,
                    ParentCategoryId = 100,
                    Name = "Accounting",
                    Keywords = "Taxes",
                },
                new
                {
                    CategoryId = 102,
                    ParentCategoryId = 100,
                    Name = "Taxation",
                    Keywords = string.Empty,
                },
                new
                {
                    CategoryId = 201,
                    ParentCategoryId = 200,
                    Name = "Computer",
                    Keywords = string.Empty,
                },
                new
                {
                    CategoryId = 103,
                    ParentCategoryId = 101,
                    Name = "Corporate Tax",
                    Keywords = string.Empty,
                },
                new
                {
                    CategoryId = 202,
                    ParentCategoryId = 201,
                    Name = "Operating System",
                    Keywords = string.Empty,
                },
                new
                {
                    CategoryId = 109,
                    ParentCategoryId = 101,
                    Name = "Small Business Tax",
                    Keywords = string.Empty,
                },
            };
            var categories = new[] { -1 };
            for (int i = 0; i < X && categories.Any(); i++)
            {
                categories = data.Join(categories, cid => cid.ParentCategoryId, pid => pid, (cid, pid) => cid.CategoryId).ToArray();
            }
            Array.Sort(categories);
            return categories;
        }

        public bool Test()
        {
            Console.WriteLine(string.Join(",", solution(2)));
            Console.WriteLine(string.Join(",", solution(3)));
            return true;

        }
    }
}
