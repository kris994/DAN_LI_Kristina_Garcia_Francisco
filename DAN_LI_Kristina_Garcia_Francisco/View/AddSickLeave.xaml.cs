using DAN_LI_Kristina_Garcia_Francisco.ViewModel;
using System.Windows;

namespace DAN_LI_Kristina_Garcia_Francisco.View
{
    /// <summary>
    /// Interaction logic for AddSickLeave.xaml
    /// </summary>
    public partial class AddSickLeave : Window
    {
        public AddSickLeave()
        {
            InitializeComponent();
            this.DataContext = new AddSickLeaveViewModel(this);
        }
    }
}
