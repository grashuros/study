using DataTier;

namespace LogicTier
{
    public class TeacherVM
    {
        private Teacher _teacher;
        public TeacherVM(Teacher t) => _teacher = t;

        public string FullInfo => $"{_teacher.FIO} ({_teacher.Position})";
        public string Department => _teacher.Department;
        public decimal Salary => _teacher.Salary;
    }
}