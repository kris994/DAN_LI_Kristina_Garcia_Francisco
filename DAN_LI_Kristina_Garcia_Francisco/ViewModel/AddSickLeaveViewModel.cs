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
    class AddSickLeaveViewModel : BaseViewModel
    {
        AddSickLeave addSickLeave;
        Service service = new Service();

        #region Constructor
        /// <summary>
        /// Constructor with the add sick leave info window opening
        /// </summary>
        /// <param name="addSickLeaveOpen">opends the add sick leave window</param>
        public AddSickLeaveViewModel(AddSickLeave addSickLeaveOpen)
        {
            sickLeave = new tblSickLeave();
            addSickLeave = addSickLeaveOpen;
            SickLeaveList = service.GetAllSickLeavesFromCurrentPatient(LoggedUser.CurrentUser.UserID).ToList();
        }
        #endregion

        #region Properties
        /// <summary>
        /// List of all Sick Leaves
        /// </summary>
        private List<tblSickLeave> sickLeaveList;
        public List<tblSickLeave> SickLeaveList
        {
            get
            {
                return sickLeaveList;
            }
            set
            {
                sickLeaveList = value;
                OnPropertyChanged("SickLeaveList");
            }
        }        

        /// <summary>
        /// Specific Sick Leave
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
        /// Cheks if its possible to execute the add and edit commands
        /// </summary>
        private bool isUpdateSickLeave;
        public bool IsUpdateSickLeave
        {
            get
            {
                return isUpdateSickLeave;
            }
            set
            {
                isUpdateSickLeave = value;
            }
        }
        #endregion

        #region Commands
        /// <summary>
        /// Command that tries to save the new sick leave
        /// </summary>
        private ICommand save;
        public ICommand Save
        {
            get
            {
                if (save == null)
                {
                    save = new RelayCommand(param => SaveExecute(), param => CanSaveExecute());
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
                service.AddSickLeave(SickLeave);
                IsUpdateSickLeave = true;

                addSickLeave.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
            }
        }

        /// <summary>
        /// Checks if its possible to save the sick leave
        /// </summary>
        protected bool CanSaveExecute()
        {
            return true;
        }

        /// <summary>
        /// Command that closes the add sick leave windo
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
                addSickLeave.Close();
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
