using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class TextDataProvider
{
    public static IEnumerable<object[]> GetPasswordTestData()
    {
        // Navigate up to the project directory from bin output
        var projectDir = Directory.GetParent(AppContext.BaseDirectory).Parent.Parent.Parent.FullName;
        var path = Path.Combine(projectDir, "TextDataProvider.txt");
        var lines = File.ReadAllLines(path);


        foreach (var line in File.ReadAllLines(path))
        {
            // Safe split with quote removal
            var parts = line.Split(',');
            var password = parts[0].Trim('"');
            var statusCode = int.Parse(parts[1]);
            var message = parts[2].Trim('"');
            var expected = bool.Parse(parts[3]);

            yield return new object[] { password, statusCode, message, expected };
        }
    }
}

