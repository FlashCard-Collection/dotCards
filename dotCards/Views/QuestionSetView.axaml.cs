using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.IO;
using System.Windows;
using System.Windows.Markup;

namespace dotCards.Views
{
    public class QuestionSetView : UserControl
    {

        public QuestionSetView()
        {
            this.InitializeComponent();


        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);

        }

    }
}
