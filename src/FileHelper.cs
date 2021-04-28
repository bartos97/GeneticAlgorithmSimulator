using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmSimulator
{
    public static class FileHelper
    {
        public static string CreateCsvFileNameTimestamp(string prefix) =>
            prefix + DateTime.Now.ToString("yyyyMMddTHHmmss") + ".csv";

        public static void SaveCollecionToCsv<T>(IEnumerable<T> collection, string filePath, string additionalText = "")
        {
            Type itemType = typeof(T);
            var props = itemType.GetFields();

            using var writer = new StreamWriter(filePath);
            writer.WriteLine(additionalText);
            writer.WriteLine(string.Join(";", props.Select(p => p.Name)));

            foreach (var item in collection)
            {
                writer.WriteLine(string.Join(";", props.Select(p => p.GetValue(item))));
            }
        }
    }
}
