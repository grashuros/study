using System.Windows;
using LogicTier;

namespace PresentationTier
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            // Укажите путь к вашему файлу данных
            string path = "teachers.txt"; 
            DataContext = new UniversityManager(path);
            InitializeComponent();
        }
    }
}