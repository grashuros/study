using System;
using System.Drawing;
using System.Windows.Forms;

namespace LabWork1
{
    public class Form1 : Form
    {
        // Поля ввода для переменных g, q, o
        private TextBox txtG, txtQ, txtO;
        // Поля только для чтения для координат (W и e из формулы)
        private TextBox txtW, txtE_var;
        private Label lblStatus;

        public Form1()
        {
            // Настройки окна
            this.Text = "Вариант 12: Расчет по координатам";
            this.Size = new Size(400, 300);

            // Инициализация элементов
            txtG = new TextBox { Location = new Point(150, 20), Text = "1" };
            txtQ = new TextBox { Location = new Point(150, 50), Text = "1" };
            txtO = new TextBox { Location = new Point(150, 80), Text = "1" };
            
            txtW = new TextBox { Location = new Point(150, 110), ReadOnly = true };
            txtE_var = new TextBox { Location = new Point(150, 140), ReadOnly = true };

            lblStatus = new Label { Location = new Point(20, 180), Size = new Size(350, 30), Text = "Двигайте мышь для расчета" };

            // Добавляем подписи (Labels)
            this.Controls.Add(new Label { Text = "Переменная g:", Location = new Point(20, 20) });
            this.Controls.Add(new Label { Text = "Переменная q:", Location = new Point(20, 50) });
            this.Controls.Add(new Label { Text = "Переменная o:", Location = new Point(20, 80) });
            this.Controls.Add(new Label { Text = "Координата W (X):", Location = new Point(20, 110) });
            this.Controls.Add(new Label { Text = "Координата e (Y):", Location = new Point(20, 140) });

            // Добавляем сами поля на форму
            this.Controls.AddRange(new Control[] { txtG, txtQ, txtO, txtW, txtE_var, lblStatus });

            // ВАЖНО: Ручная подписка на событие MouseMove
            this.MouseMove += new MouseEventHandler(CalculateFormula);
        }

        private void CalculateFormula(object sender, MouseEventArgs e)
        {
            // Обновляем поля координат
            txtW.Text = e.X.ToString();
            txtE_var.Text = e.Y.ToString();

            try
            {
                // Считываем значения из полей
                double g = double.Parse(txtG.Text);
                double q = double.Parse(txtQ.Text);
                double o = double.Parse(txtO.Text);
                
                // Переменные из координат
                double W = e.X;
                double e_param = e.Y; // используем e_param, так как 'e' занято событием

                // Проверка на деление на ноль (W в знаменателе)
                if (W == 0) throw new Exception();

                // Формула варианта 12: 
                // t = W + cos(g*q)/W - e + |sin(e) + sqrt(|o|)|
                double t = W + (Math.Cos(g * q) / W) - e_param + 
                           Math.Abs(Math.Sin(e_param) + Math.Sqrt(Math.Abs(o)));

                // Вывод результата в заголовок окна по заданию
                this.Text = $"Результат: {t:F4}";
                lblStatus.Text = "Расчет выполнен успешно";
            }
            catch
            {
                // Если в полях не числа или W = 0
                this.Text = "ERROR";
                lblStatus.Text = "Ошибка: проверьте ввод данных";
            }
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}