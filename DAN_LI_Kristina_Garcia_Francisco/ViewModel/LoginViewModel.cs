using DAN_LI_Kristina_Garcia_Francisco.Commands;
using DAN_LI_Kristina_Garcia_Francisco.Model;
using DAN_LI_Kristina_Garcia_Francisco.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DAN_LI_Kristina_Garcia_Francisco.ViewModel
{
    class LoginViewModel : BaseViewModel
    {
        Login view;
        Service service = new Service();

        #region Constructor
        public LoginViewModel(Login loginView)
        {
            view = loginView;
            user = new tblUser();
            UserList = service.GetAllUsers().ToList();
            DoctorList = service.GetAllDoctors().ToList();
        }
        #endregion

        #region Property
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

        /// <summary>
        /// Used for saving the current user
        /// </summary>
        private tblUser user;
        public tblUser User
        {
            get
            {
                return user;
            }
            set
            {
                user = value;
                OnPropertyChanged("User");
            }
        }

        /// <summary>
        /// List of all users in the application
        /// </summary>
        private List<tblUser> userList;
        public List<tblUser> UserList
        {
            get
            {
                return userList;
            }
            set
            {
                userList = value;
                OnPropertyChanged("UserList");
            }
        }

        /// <summary>
        /// Used for saving the current doctor
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
        /// List of all doctors in the application
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
        #endregion

        #region Commands
        /// <summary>
        /// Command that tries to add a new patient
        /// </summary>
        private ICommand addNewPatient;
        public ICommand AddNewPatient
        {
            get
            {
                if (addNewPatient == null)
                {
                    addNewPatient = new RelayCommand(param => AddNewPatientExecute(), param => CanAddNewPatientExecute());
                }
                return addNewPatient;
            }
        }

        /// <summary>
        /// Executes the add patient command
        /// </summary>
        private void AddNewPatientExecute()
        {
            try
            {
                AddPatient addPatient = new AddPatient();               
                addPatient.ShowDialog();
                view.Close();
                if ((addPatient.DataContext as AddPatientViewModel).IsUpdatePatient == true)
                {
                    UserList = service.GetAllUsers().ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Checks if its possible to add the new patient
        /// </summary>
        /// <returns>true</returns>
        private bool CanAddNewPatientExecute()
        {
            return true;
        }

        /// <summary>
        /// Command that tries to add a new doctor
        /// </summary>
        private ICommand addNewDoctor;
        public ICommand AddNewDoctor
        {
            get
            {
                if (addNewDoctor == null)
                {
                    addNewDoctor = new RelayCommand(param => AddNewDoctorExecute(), param => CanAddNewDoctorExecute());
                }
                return addNewDoctor;
            }
        }

        /// <summary>
        /// Executes the add Doctor command
        /// </summary>
        private void AddNewDoctorExecute()
        {
            try
            {
                AddDoctor addDoctor = new AddDoctor();
                addDoctor.ShowDialog();
                view.Close();
                if ((addDoctor.DataContext as AddDoctorViewModel).IsUpdateDoctor == true)
                {
                    DoctorList = service.GetAllDoctors().ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Checks if its possible to add the new Doctor
        /// </summary>
        /// <returns>true</returns>
        private bool CanAddNewDoctorExecute()
        {
            return true;
        }

        /// <summary>
        /// Command used to log the user into the application
        /// </summary>
        private ICommand login;
        public ICommand Login
        {
            get
            {
                if (login == null)
                {
                    login = new RelayCommand(LoginExecute);
                }
                return login;
            }
        }

        /// <summary>
        /// Checks if its possible to login depending on the given username and password and saves the logged in user to a list
        /// </summary>
        /// <param name="obj"></param>
        private void LoginExecute(object obj)
        {
            string password = (obj as PasswordBox).Password;
            bool found = false;

            for (int i = 0; i < UserList.Count; i++)
            {
                if (User.Username == UserList[i].Username && password == UserList[i].UserPassword)
                {
                    LoggedUser.CurrentUser = new tblUser
                    {
                        UserID = UserList[i].UserID,
                        FirstName = UserList[i].FirstName,
                        LastName = UserList[i].LastName,
                        JMBG = UserList[i].JMBG,
                        HealthIsuranceNumber = UserList[i].HealthIsuranceNumber,
                        Username = UserList[i].Username,
                        UserPassword = UserList[i].UserPassword,
                        DoctorID = UserList[i].DoctorID
                    };
                    InfoLabel = "Logged in";
                    found = true;

                    User users = new User();
                    view.Close();
                    users.Show();
                    break;
                }
            }

            for (int i = 0; i < DoctorList.Count; i++)
            {
                if (User.Username == DoctorList[i].Username && password == DoctorList[i].UserPassword)
                {
                    LoggedDoctor.CurrentDoctor = new tblDoctor
                    {
                        DoctorID = DoctorList[i].DoctorID,
                        FirstName = DoctorList[i].FirstName,
                        LastName = DoctorList[i].LastName,
                        JMBG = DoctorList[i].JMBG,
                        BankAccount = DoctorList[i].BankAccount,
                        Username = DoctorList[i].Username,
                        UserPassword = DoctorList[i].UserPassword
                    };
                    InfoLabel = "Logged in";
                    found = true;

                    Doctor doctors = new Doctor();
                    view.Close();
                    doctors.Show();
                    break;
                }
            }

            if (found == false)
            {
                InfoLabel = "Wrong Username or Password";
            }
        }
        #endregion
    }
}
