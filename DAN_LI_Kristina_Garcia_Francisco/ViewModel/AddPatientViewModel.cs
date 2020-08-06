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
    class AddPatientViewModel : BaseViewModel
    {
        AddPatient addPatient;
        Service service = new Service();

        #region Constructor
        /// <summary>
        /// Constructor with the add patient info window opening
        /// </summary>
        /// <param name="addPatientOpen">opends the add patient window</param>
        public AddPatientViewModel(AddPatient addPatientOpen)
        {
            patient = new tblUser();
            addPatient = addPatientOpen;
            PatientList = service.GetAllUsers().ToList();
        }
        #endregion

        #region Property
        /// <summary>
        /// List of patients
        /// </summary>
        private List<tblUser> patientList;
        public List<tblUser> PatientList
        {
            get
            {
                return patientList;
            }
            set
            {
                patientList = value;
                OnPropertyChanged("PatientList");
            }
        }

        /// <summary>
        /// Specific Patient
        /// </summary>
        private tblUser patient;
        public tblUser Patient
        {
            get
            {
                return patient;
            }
            set
            {
                patient = value;
                OnPropertyChanged("Patient");
            }
        }

        /// <summary>
        /// Cheks if its possible to execute the add patient command
        /// </summary>
        private bool isUpdatePatient;
        public bool IsUpdatePatient
        {
            get
            {
                return isUpdatePatient;
            }
            set
            {
                isUpdatePatient = value;
            }
        }
        #endregion

        #region Commands
        /// <summary>
        /// Command that tries to save the new patient
        /// </summary>
        private ICommand save;
        public ICommand Save
        {
            get
            {
                if (save == null)
                {
                    save = new RelayCommand(param => SaveExecute(), param => this.CanSaveExecute);
                }
                return save;
            }
        }

        /// <summary>
        /// Tries the execute the save command
        /// </summary>
        private void SaveExecute()
        {
            try
            {
                service.AddUser(Patient);
                IsUpdatePatient = true;
                Login login = new Login();

                addPatient.Close();
                login.Show();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
            }
        }

        /// <summary>
        /// Checks if its possible to save the patient
        /// </summary>
        protected bool CanSaveExecute
        {
            get
            {
                return Patient.IsValid;
            }
        }

        /// <summary>
        /// Command that closes the add patient window
        /// </summary>
        private ICommand cancel;
        public ICommand Cancel
        {
            get
            {
                if (cancel == null)
                {
                    cancel = new RelayCommand(param => CancelExecute(), param => CanCancelExecute());
                }
                return cancel;
            }
        }

        /// <summary>
        /// Executes the close command
        /// </summary>
        private void CancelExecute()
        {
            try
            {
                addPatient.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Checks if its possible to execute the close command
        /// </summary>
        /// <returns>true</returns>
        private bool CanCancelExecute()
        {
            return true;
        }
        #endregion
    }
}
