using DAN_LI_Kristina_Garcia_Francisco.ViewModel;
using System.Windows;

namespace DAN_LI_Kristina_Garcia_Francisco.View
{
    /// <summary>
    /// Interaction logic for User.xaml
    /// </summary>
    public partial class User : Window
    {
        /// <summary>
        /// Patient window
        /// </summary>
        public User()
        {
            InitializeComponent();
            this.DataContext = new PatientViewModel(this);
        }
    }
}
