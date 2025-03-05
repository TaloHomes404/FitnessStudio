using System.Windows.Controls;
using FitnessStudio.MVVM.ViewModel.HomeContents;

namespace FitnessStudio.MVVM.View.HomeContents
{
    /// <summary>
    /// Logika interakcji dla klasy ToolsContent.xaml
    /// </summary>
    public partial class ToolsContent : UserControl
    {
        public ToolsContent()
        {
            InitializeComponent();
            DataContext = new ToolsContentViewModel();
        }
    }
}
