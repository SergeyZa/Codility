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
    [ExportMetadata("Name", nameof(Class1))]
    public class Class1 : ISolution
    {
        public string solution(int X)
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

            var res = (from D in data
                       join P in data on D.ParentCategoryId equals P.CategoryId
                       where D.CategoryId == X
                       select new
                       {
                           D.ParentCategoryId,
                           D.Name,
                           Keywords = string.IsNullOrEmpty(D.Keywords) ? P.Keywords : D.Keywords,
                       }).FirstOrDefault();
            if (null == res)
                return string.Empty;
            var parentId = res.ParentCategoryId;
            var Keywords = res.Keywords;
            while (string.IsNullOrEmpty(Keywords))
            {
                var pData = data.FirstOrDefault(d => d.CategoryId == parentId);
                if (null == pData)
                    break;
                Keywords = pData.Keywords;
                parentId = pData.ParentCategoryId;
            }
            return $"{nameof(res.ParentCategoryId)}={res.ParentCategoryId}, {nameof(res.Name)}={res.Name}, {nameof(Keywords)}={Keywords}";
        }

        public bool Test()
        {
            Console.WriteLine(solution(201));
            Console.WriteLine(solution(202));
            return true;

        }
    }
}