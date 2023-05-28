using System;
using System.Collections.Generic;
using System.Windows;

namespace kursova
{
    public partial class MainWindow : Window
    {
        List<double> list;
        Logic logic = new Logic();
        double q = 1, a = 1;
        int count;

        public MainWindow()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            lableCountOfF1.Content = "Кількість обчислень f1(x) : ";
            lableCountOfF2.Content = "Кількість обчислень f2(x) : ";
            f1.Items.Clear();
            f2.Items.Clear();

            list = new List<double>();
            bool flag = false;

            try
            {
                logic.chackValue(TextBox1.Text, TextBox2.Text, TextBox3.Text);
                if (q > 0 && q <= 0.25)
                {
                    count = logic.calculationAns(ref f1, 1, ref list, ref flag);
                    lableCountOfF1.Content = "Кількість обчислень f1(x) : " + count;
                }
                else
                {
                    count = logic.calculationAns(ref f2, 2, ref list, ref flag);
                    lableCountOfF2.Content = "Кількість обчислень f2(x) : " + count;
                }
                if (flag)
                {
                    MessageBox.Show($"В результаті обчислення були значення при яких f є ∅ \nx є " + logic.stringNoSolution(list),
                        "Увага ", MessageBoxButton.OK, MessageBoxImage.Warning) ;
                }
            }
            catch (FormatException ex)
            {
                errorShow($"Ви ввели не число в рядку {ex.Message}", "Помилка");
            }
            catch (ArgumentOutOfRangeException)
            {
                errorShow($"Число dx не може бути меншим або рівим 0", "Помилка");
            }
            catch (ArgumentException)
            {
                errorShow($"Число xmax не може бути меншим за xmin", "Помилка");
            } 
        }

        private void errorShow(string text, string title)
        {
            MessageBox.Show(text, title, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public void newQ_Click(object sender, RoutedEventArgs e)
        {
            logic.newQAndA();
            q = logic.Q;
            a = logic.A;
            randomQ.Content = "Випадкове число q = " + q;
            randomA.Content = "Випадкове число a = " + a;
        }
    }
}


