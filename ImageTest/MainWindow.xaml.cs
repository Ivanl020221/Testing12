using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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

namespace ImageTest
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        
        public MainWindow()
        {
            InitializeComponent();
            Colorlist.ItemsSource =  Enum.GetNames(typeof(KnownColor)).Select(i => new { color = i }).OrderByDescending(i =>i.color).ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Изрображение(*.png, *.jpeg)|*.png; *.jpeg; *.jpg";
            if ((bool)openFile.ShowDialog())
            {
                if (LenghtFile(openFile.FileName))
                {
                    if (OrentationFile(openFile.FileName))
                    {
                        if (HeightXWidth(openFile.FileName))
                        {
                            Image.Source = new BitmapImage(new Uri(openFile.FileName));
                        }
                        else
                        {
                            MessageBox.Show("Соотношение с торон должно быть 3 на 4", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Фото должно быть расположено вертрикально", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Фото весит больше 2х мегабайт", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private bool LenghtFile(string path)
        {
            FileInfo fileInfo = new FileInfo(path);
            return fileInfo.Length < 2097152 ? true : false;
        }

        private bool OrentationFile(string path)
        {
            BitmapImage image = new BitmapImage(new Uri(path));
            return image.Height > image.Width;
        }

        private bool HeightXWidth(string path)
        {
            BitmapImage image = new BitmapImage(new Uri(path));
            double OneInterval = image.Width / 3;
            double Height = OneInterval * 4;
            return Height <= (image.Height + 10) && Height >= (image.Height - 10);
           
        }
    }
}
