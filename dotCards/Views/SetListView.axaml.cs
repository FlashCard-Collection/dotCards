using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace dotCards.Views
{
    public class SetListView : UserControl
    {
        public SetListView()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
