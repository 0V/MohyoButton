using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace MohyoButton
{
    /// <summary>
    /// WpfMessageBox.xaml の相互作用ロジック
    /// </summary>
    public partial class WpfMessageBox : Window
    {
        public MessageBoxResult Result { get; set; }
        public string Message { get; set; }

        public WpfMessageBox(string messageBoxText, MessageBoxButton button = MessageBoxButton.OK)
        {
            InitializeComponent();
            this.Message = messageBoxText;
            MessageText.Text = this.Message;
            MakeButton(button);
        }

        private void MakeButton(MessageBoxButton button)
        {
            switch (button)
            {
                case MessageBoxButton.OK:
                    SetButton("OK", 0, 3, 2);
                    break;
                case MessageBoxButton.OKCancel:
                    SetButton("OK", 0, 3);
                    SetButton("Cancel", 3, 1);
                    break;
                case MessageBoxButton.YesNo:
                    SetButton("Yes", 0, 3);
                    SetButton("No", 1, 3);
                    break;
                case MessageBoxButton.YesNoCancel:
                    SetButton("Yes", 0, 3);
                    SetButton("No", 1, 3);
                    break;
                default:
                    break;
            }
        }

        private void SetButton(string buttonName, int culumn, int row, int culumnSpan = 1, int rowSpan = 1)
        {
            var button = new Button();
            button.Name = buttonName + "Button";
            button.Content = buttonName;
            button.Margin = new Thickness(20);
            button.Click += new RoutedEventHandler(Button_Click);
            button.SetValue(Grid.ColumnProperty, culumn);
            button.SetValue(Grid.RowProperty, row);
            button.SetValue(Grid.ColumnSpanProperty, culumnSpan);
            button.SetValue(Grid.RowSpanProperty, rowSpan);
            MessageBoxGrid.Children.Add(button);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                var name = button.Name;
                Result = name == "CancelButton" ? MessageBoxResult.Cancel :
                        name == "YesButton" ? MessageBoxResult.Yes :
                        name == "NoButton" ? MessageBoxResult.No :
                        name == "OKButton" ? MessageBoxResult.OK : MessageBoxResult.None;
                this.Close();
            }
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}