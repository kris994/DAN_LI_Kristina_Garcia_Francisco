using DAN_LI_Kristina_Garcia_Francisco.Commands;
using DAN_LI_Kristina_Garcia_Francisco.Model;
using DAN_LI_Kristina_Garcia_Francisco.View;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace DAN_LI_Kristina_Garcia_Francisco.ViewModel
{
    /// <summary>
    /// Patient Window view model
    /// </summary>
    class PatientViewModel : BaseViewModel
    {
        User patientWindow;
        Service service = new Service();

        #region Constructor
        /// <summary>
        /// Constructor with PatientViewModel param
        /// </summary>
        /// <param name="patientOpen">opens the all User window</param>
        public PatientViewModel(User patientOpen)
        {
            patientWindow = patientOpen;
            DoctorList = service.GetAllDoctors().ToList();
            AllCurrentSickLeaves = service.GetAllSickLeavesFromCurrentPatient(LoggedUser.CurrentUser.UserID).ToList();
            ShowAllDoctors();
            InfoText();
        }
        #endregion

        #region Property
        /// <summary>
        /// List of doctors
        /// </summary>
        private List<tblDoctor> doctorList;
        public List<tblDoctor> DoctorList
        {
            get
            {
                return doctorList;
            }
            set
            {
                doctorList = value;
                OnPropertyChanged("DoctorList");
            }
        }

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

        /// <summary>
        /// Specific Doctor
        /// </summary>
        private tblDoctor doctor;
        public tblDoctor Doctor
        {
            get
            {
                return doctor;
            }
            set
            {
                doctor = value;
                OnPropertyChanged("Doctor");
            }
        }

        /// <summary>
        /// Show all doctors visibility
        /// </summary>
        private Visibility showDoctors;
        public Visibility ShowDoctors
        {
            get
            {
                return showDoctors;
            }
            set
            {
                showDoctors = value;
                OnPropertyChanged("ShowDoctors");
            }
        }

        /// <summary>
        /// Login info label
        /// </summary>
        private string infoLabel;
        public string InfoLabel
        {
            get
            {
                return infoLabel;
            }
            set
            {
                infoLabel = value;
                OnPropertyChanged("InfoLabel");
            }
        }
        #endregion

        public void ShowAllDoctors()
        {
            if (LoggedUser.CurrentUser.DoctorID == null)
            {
                ShowDoctors = Visibility.Visible;
            }
            else
            {
                ShowDoctors = Visibility.Collapsed;
            }
        }

        public void InfoText()
        {
            if (!DoctorList.Any())
            {
                InfoLabel = "No available doctors right now";
            }
            else
            {
                InfoLabel = "";
            }
        }

        #region Commands
        /// <summary>
        /// Command that tries to choose a doctor
        /// </summary>
        private ICommand selectDoctor;
        public ICommand SelectDoctor
        {
            get
            {
                if (selectDoctor == null)
                {
                    selectDoctor = new RelayCommand(param => SelectDoctorExecute(), param => CanSelectDoctorExecute());
                }
                return selectDoctor;
            }
        }

        /// <summary>
        /// Tries the execute the SelectDoctor command
        /// </summary>
        private void SelectDoctorExecute()
        {
            // Checks if the user really wants to delete the song
            var result = MessageBox.Show("Are you sure you want to choose this doctor?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    if (Doctor != null)
                    {
                        service.ChooseDoctor(LoggedUser.CurrentUser, Doctor.DoctorID);
                        ShowAllDoctors();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                return;
            }
        }

        /// <summary>
        /// Checks if its possible to select doctor
        /// </summary>
        protected bool CanSelectDoctorExecute()
        {
            return true;
        }

        /// <summary>
        /// Command that tries to add a new sick leave
        /// </summary>
        private ICommand addNewSickLeave;
        public ICommand AddNewSickLeave
        {
            get
            {
                if (addNewSickLeave == null)
                {
                    addNewSickLeave = new RelayCommand(param => AddNewSickLeaveExecute(), param => CanAddNewSickLeaveExecute());
                }
                return addNewSickLeave;
            }
        }

        /// <summary>
        /// Executes the add Add Sick Leave command
        /// </summary>
        private void AddNewSickLeaveExecute()
        {
            try
            {
                AddSickLeave addSickLeave = new AddSickLeave();
                addSickLeave.ShowDialog();
                if ((addSickLeave.DataContext as AddSickLeaveViewModel).IsUpdateSickLeave == true)
                {
                    AllCurrentSickLeaves = service.GetAllSickLeavesFromCurrentPatient(LoggedUser.CurrentUser.UserID).ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Checks if its possible to add the new sick leave
        /// </summary>
        /// <returns>true</returns>
        private bool CanAddNewSickLeaveExecute()
        {
            if(LoggedUser.CurrentUser.DoctorID == null)
            {
                return false;
            }
            else
            {
                return true;
            }          
        }

        /// <summary>
        /// Command that tries to delete the sick leave
        /// </summary>
        private ICommand deleteSickLeave;
        public ICommand DeleteSickLeave
        {
            get
            {
                if (deleteSickLeave == null)
                {
                    deleteSickLeave = new RelayCommand(param => DeleteSickLeaveExecute(), param => CanDeleteSickLeaveExecute());
                }
                return deleteSickLeave;
            }
        }

        /// <summary>
        /// Executes the delete command
        /// </summary>
        public void DeleteSickLeaveExecute()
        {
            // Checks if the user really wants to delete the song
            var result = MessageBox.Show("Are you sure you want to delete the sick leave?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    if (SickLeave != null)
                    {
                        int sickLeaveID = SickLeave.SickLeaveID;
                        service.DeleteSickLeave(sickLeaveID);
                        AllCurrentSickLeaves = service.GetAllSickLeavesFromCurrentPatient(LoggedUser.CurrentUser.UserID).ToList();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                return;
            }
        }

        /// <summary>
        /// Checks if the sick leave can be deleted
        /// </summary>
        /// <returns>true if possible</returns>
        public bool CanDeleteSickLeaveExecute()
        {
            if (SickLeave == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

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
                patientWindow.Close();
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
