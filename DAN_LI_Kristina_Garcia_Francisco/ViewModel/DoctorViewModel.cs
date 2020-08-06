using DAN_LI_Kristina_Garcia_Francisco.Commands;
using DAN_LI_Kristina_Garcia_Francisco.Model;
using DAN_LI_Kristina_Garcia_Francisco.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace DAN_LI_Kristina_Garcia_Francisco.ViewModel
{
    class DoctorViewModel : BaseViewModel
    {
        Doctor doctorWindow;
        Service service = new Service();

        #region Constructor
        /// <summary>
        /// Constructor with DoctorViewModel param
        /// </summary>
        /// <param name="Doctor">opens the Doctor window</param>
        public DoctorViewModel(Doctor doctorOpen)
        {
            doctorWindow = doctorOpen;
            AllCurrentSickLeaves = service.GetAllSickLeavesFromCurrentDoctor(LoggedDoctor.CurrentDoctor.DoctorID).ToList();
        }
        #endregion

        #region Property
        /// <summary>
        /// List of current sick leaves
        /// </summary>
        private List<tblSickLeave> allCurrentSickLeaves;
        public List<tblSickLeave> AllCurrentSickLeaves
        {
            get
            {
                return allCurrentSickLeaves;
            }
            set
            {
                allCurrentSickLeaves = value;
                OnPropertyChanged("AllCurrentSickLeaves");
            }
        }

        /// <summary>
        /// Specific sick leave
        /// </summary>
        private tblSickLeave sickLeave;
        public tblSickLeave SickLeave
        {
            get
            {
                return sickLeave;
            }
            set
            {
                sickLeave = value;
                OnPropertyChanged("SickLeave");
            }
        }
        #endregion

        #region Commands
        /// <summary>
        /// Command that logs off the user
        /// </summary>
        private ICommand logoff;
        public ICommand Logoff
        {
            get
            {
                if (logoff == null)
                {
                    logoff = new RelayCommand(param => LogoffExecute(), param => CanLogoffExecute());
                }
                return logoff;
            }
        }

        /// <summary>
        /// Executes the logoff command
        /// </summary>
        private void LogoffExecute()
        {
            try
            {
                Login login = new Login();
                doctorWindow.Close();
                login.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Checks if its possible to logoff
        /// </summary>
        /// <returns>true</returns>
        private bool CanLogoffExecute()
        {
            return true;
        }
        #endregion
    }
}
