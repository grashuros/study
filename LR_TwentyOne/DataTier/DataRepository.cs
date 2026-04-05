using System;
using System.Collections.Generic;
using System.IO;
using System.Globalization;

namespace DataTier
{
    public static class DataRepository
    {
        public static List<Teacher> GetTeachers(string filePath)
        {
            var list = new List<Teacher>();
            if (!File.Exists(filePath)) return list;

            foreach (var line in File.ReadAllLines(filePath))
            {
                var parts = line.Split('|');
                if (parts.Length == 4)
                {
                    list.Add(new Teacher {
                        FIO = parts[0].Trim(),
                        Position = parts[1].Trim(),
                        Department = parts[2].Trim(),
                        Salary = decimal.Parse(parts[3].Trim(), CultureInfo.InvariantCulture)
                    });
                }
            }
            return list;
        }
    }
}