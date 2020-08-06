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
    class AddDoctorViewModel : BaseViewModel
    {
        AddDoctor addDoctor;
        Service service = new Service();

        #region Constructor
        /// <summary>
        /// Constructor with the add doctor info window opening
        /// </summary>
        /// <param name="addDoctorOpen">opends the add doctor window</param>
        public AddDoctorViewModel(AddDoctor addDoctorOpen)
        {
            doctor = new tblDoctor();
            addDoctor = addDoctorOpen;
            DoctorList = service.GetAllDoctors().ToList();
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
        /// Cheks if its possible to execute the add doctor command
        /// </summary>
        private bool isUpdateDoctor;
        public bool IsUpdateDoctor
        {
            get
            {
                return isUpdateDoctor;
            }
            set
            {
                isUpdateDoctor = value;
            }
        }
        #endregion

        #region Commands
        /// <summary>
        /// Command that tries to save the new doctor
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
                service.AddDoctor(Doctor);
                IsUpdateDoctor = true;

                Login login = new Login();

                addDoctor.Close();
                login.Show();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
            }
        }

        /// <summary>
        /// Checks if its possible to save the doctor
        /// </summary>
        protected bool CanSaveExecute
        {
            get
            {
                return Doctor.IsValid;
            }
        }

        /// <summary>
        /// Command that closes the add doctor window
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
                addDoctor.Close();
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
