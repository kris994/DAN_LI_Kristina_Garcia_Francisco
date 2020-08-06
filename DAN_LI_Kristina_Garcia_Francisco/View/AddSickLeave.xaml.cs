using DAN_LI_Kristina_Garcia_Francisco.ViewModel;
using System.Windows;

namespace DAN_LI_Kristina_Garcia_Francisco.View
{
    /// <summary>
    /// Interaction logic for AddSickLeave.xaml
    /// </summary>
    public partial class AddSickLeave : Window
    {
        /// <summary>
        /// Adds a sick leave window
        /// </summary>
        public AddSickLeave()
        {
            InitializeComponent();
            this.DataContext = new AddSickLeaveViewModel(this);
        }
    }
}
