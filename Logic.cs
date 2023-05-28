using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace kursova
{
    public class Logic
    {
        private double xmin, xmax, dx;
        private double q, a;
        private Random random = new Random();
        private delegate double Formula(double x);

        public double Q
        {
            get { return q; }
            set { q = value; }
        }
        public double A
        {
            get { return a; }
            set { a = value; }
        }
        public Logic()
        {
            xmin = 0;
            xmax = 0;
            dx = 0;
            q = 1;
            a = 1;
        }

        private double calculationF1(double x)
        {
            return Math.Log(q * Math.Sin(a - x)) / q + x;
        }
        private double calculationF2(double x)
        {
            return Math.Pow(q - a * x, (1.0 / 4));
        }
        
        public void newQAndA()
        {
            q = Math.Round(random.NextDouble(), 3);
            a = Math.Round(random.Next() % 1000 + random.NextDouble(), 3) * (random.Next(2) == 0 ? -1 : 1); 
        }

        public int calculationAns(ref ListBox myListBox, int whatFormula, ref List <double> list, ref bool flag)
        {
            Formula formula;
            int count = 0;
            double f;
            double x = xmin;

            if (whatFormula == 1)  formula = new Formula(calculationF1);
            else formula = new Formula(calculationF2);

            while (x <= xmax)
            {
                f = formula(x);
                
                if (double.IsNaN(f) || double.IsInfinity(f))
                {
                    flag = true;
                    list.Add(x);
                    myListBox.Items.Add($"x = {Math.Round(x, 5),10} " + " \t f(x) = -");
                }
                else
                {
                    count++;
                    myListBox.Items.Add($"x = {Math.Round(x, 5),10} " + " \tf(x) = " + Math.Round(f, 5).ToString());
                }
                x += dx;
            }
            return count;
        }

        public string stringNoSolution(List <double> list)
        {
            bool flag = true;
            string noSolution = $"[{Math.Round(list[0], 5)}";
            for (int i = 1; i < list.Count; i++)
            {
                if (list[i] != list[i - 1] + dx)
                {
                    flag = true;
                    noSolution += $"; {Math.Round(list[i - 1], 5)}] U [{Math.Round(list[i], 5)}";
                }
                else if (i == list.Count - 1)
                {
                    flag = false;
                    noSolution += $"; {Math.Round(list[i], 5)}]";
                }
            }
            if (flag)
            {
                noSolution += "]";
            }
            return noSolution;
        }

        public void chackValue(string xmin, string xmax, string dx)
        {
            if (!double.TryParse(xmin, out this.xmin))
            {
                throw new FormatException("Xmin");
            }
            else if (!double.TryParse(xmax, out this.xmax))
            {
                throw new FormatException("Xmax");
            }
            else if (this.xmin > this.xmax)
            {
                throw new ArgumentException();
            }
            else if (!double.TryParse(dx, out this.dx))
            {
                throw new FormatException("dx");
            }
            else if (this.dx <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }

        }
    }
}


