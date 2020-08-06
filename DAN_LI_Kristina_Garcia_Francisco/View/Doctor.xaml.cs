using DAN_LI_Kristina_Garcia_Francisco.ViewModel;
using System.Windows;

namespace DAN_LI_Kristina_Garcia_Francisco.View
{
    /// <summary>
    /// Interaction logic for Doctor.xaml
    /// </summary>
    public partial class Doctor : Window
    {
        /// <summary>
        /// Doctor window
        /// </summary>
        public Doctor()
        {
            InitializeComponent();
            this.DataContext = new DoctorViewModel(this);
        }
    }
}
