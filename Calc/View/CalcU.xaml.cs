﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calc.View
{
    /// <summary>
    /// Логика взаимодействия для CalcU.xaml
    /// </summary>
    public partial class CalcU : UserControl,INotifyPropertyChanged
    {
        public CalcU()
        {
            InitializeComponent();
            Dat.DataContext = this;
        }

        private string val1 = "0";
        private string val2 = "0";
        private string line="0";

        private string histLine;


        public string HistLine
        {
            get { return histLine; }
            set { histLine = value;
                OnPropertyChanged("HistLine");
            }
        }

        public string Line
        {
            get { return line; }
            set { line = value;
                OnPropertyChanged("Line");
            }
        }

        private string operat;
        private bool valueFir = true;
        private bool sch;

        public string Operat
        {
            get { return operat; }
            set { operat = value;
                OnPropertyChanged("Operat");
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button buttonClick = (Button)sender;
            string v = buttonClick.Content.ToString();
            if (valueFir)
            {
                Line = "0";
                valueFir = false;
            }
            if (line.Contains(",") && v == ",")
            {
                return;
            }
           
            if (line == "0" && v != ",")
            {
                Line = v;
            }
            else
            {
                Line += v;
            }

            if (String.IsNullOrEmpty(operat))
            {
                val1 = line;
            }
            else {
                val2 = line;
                sch = true;
            }


        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Button buttonClick = (Button)sender;
            string v = buttonClick.Content.ToString();

            
            val2 = Line;
            if (sch)
            {
                Calculate();
                sch = false;
            }
            
            
            Operat = v;
            if (!valueFir && !sch)
            {
                HistLine += val2 + " " + operat+" ";

            }
            else
            {
                HistLine = HistLine.Substring(0, HistLine.Length - 2) + operat + " ";
            }
            valueFir = true;
        }

        private void Calculate()
        {
            double value1 = Double.Parse(val1);
            double value2 = Double.Parse(val2);
            double res = 0;
            switch (operat)
            {
                case "+": res = value1 + value2; break;
                case "-": res = value1 - value2; break;
                case "/": res = value1 / value2; break;
                case "*": res = value1 * value2; break;
                default:
                    break;
            }
           
            Line = res.ToString();
            val1 = Line;

        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler propertyChanged = PropertyChanged;
            if (propertyChanged != null)
                propertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Calculate();
            sch = false;
            valueFir = true;
            operat = null;
            HistLine = "";
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            ChangedLineValue("0");
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            sch = false;
            valueFir = true;
            operat = null;
            Line = "0";
            val1 = "0";
            val2 = "0";
            HistLine = "";

        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            
            if (Line.Length > 1) {
                string temp = Line.Substring(0, Line.Length-1);
                ChangedLineValue(temp);
            }
            else
            {
                ChangedLineValue("0");
            }
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            if (Line[0] != '-')
            {
                if (String.IsNullOrEmpty(operat))
                {
                    Line = val1 = "-" + Line;
                }
                else {
                    Line = val2 = "-" + Line;
                }
            }
            else
            {
                string temp = Line.Substring(1, Line.Length-1);
                ChangedLineValue(temp);
            }
           
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {

            Button buttonClick = (Button)sender;
            string v = buttonClick.Content.ToString();
            double res= Double.Parse(Line);

            switch (v)
            {
                case "1/x":res = 1 / res; break;
                case "%": res = Double.Parse(val1)/100*Double.Parse(val2); break;
                case "√": res = Math.Sqrt(res); break;
                default:
                    break;
            }
           
            ChangedLineValue(res.ToString());
            valueFir = true;
        }

        

        private void ChangedLineValue(string newLine)
        {
            if (String.IsNullOrEmpty(operat))
            {
                Line = val1 = newLine;
            }
            else {
                Line = val2 = newLine;
            }
        }
    }
}
