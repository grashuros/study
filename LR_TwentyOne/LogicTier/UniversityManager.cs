using DataTier;
using System.Collections.Generic;
using System.Linq;

namespace LogicTier
{
    public class UniversityManager
    {
        public List<TeacherVM> Teachers { get; }
        public string Title => "Учет преподавателей";

        public UniversityManager(string path)
        {
            Teachers = DataRepository.GetTeachers(path)
                .Select(t => new TeacherVM(t)).ToList();
        }

        public decimal TotalSalaryFund => Teachers.Sum(t => t.Salary);

        // Средняя зарплата по кафедрам (вернем строку для вывода)
        public List<string> AvgSalaryByDept => Teachers
            .GroupBy(t => t.Department)
            .Select(g => $"{g.Key}: {g.Average(t => t.Salary):F2}")
            .ToList();
    }
}