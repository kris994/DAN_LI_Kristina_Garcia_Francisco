using System.Linq;
using DAN_LI_Kristina_Garcia_Francisco.Model;
using System.Collections.Generic;
using System;
using System.Diagnostics;

namespace DAN_LI_Kristina_Garcia_Francisco
{
    /// <summary>
    /// Class that includes all CRUD functions of the application
    /// </summary>
    class Service
    {
        public object GetAllSickLeavesFromCurrentPatientLoggedIn { get; internal set; }

        /// <summary>
        /// Gets all information about users
        /// </summary>
        /// <returns>a list of found users</returns>
        public List<tblUser> GetAllUsers()
        {
            try
            {
                using (HospitalDBEntities context = new HospitalDBEntities())
                {
                    List<tblUser> list = new List<tblUser>();
                    list = (from x in context.tblUsers select x).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        /// <summary>
        /// Gets all information about doctors
        /// </summary>
        /// <returns>a list of found doctors</returns>
        public List<tblDoctor> GetAllDoctors()
        {
            try
            {
                using (HospitalDBEntities context = new HospitalDBEntities())
                {
                    List<tblDoctor> list = new List<tblDoctor>();
                    list = (from x in context.tblDoctors select x).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        /// <summary>
        /// Gets all information about sick leaves
        /// </summary>
        /// <returns>a list of found sick leaves</returns>
        public List<tblSickLeave> GetAllSickLeaves()
        {
            try
            {
                using (HospitalDBEntities context = new HospitalDBEntities())
                {
                    List<tblSickLeave> list = new List<tblSickLeave>();
                    list = (from x in context.tblSickLeaves select x).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        /// <summary>
        /// Gets all information about sick leaves from current logged in patient
        /// </summary>
        /// <param name="userID">Gets the userID from current logged in user</param>
        /// <returns>a list of found sick leaves</returns>
        public List<tblSickLeave> GetAllSickLeavesFromCurrentPatient(int userID)
        {
            try
            {
                List<tblSickLeave> list = new List<tblSickLeave>();
                using (HospitalDBEntities context = new HospitalDBEntities())
                {
                    for (int i = 0; i < GetAllSickLeaves().Count; i++)
                    {
                        if (GetAllSickLeaves()[i].UserID == userID)
                        {
                            list.Add(GetAllSickLeaves()[i]);
                        }
                    }
                    return list;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        /// <summary>
        /// Gets all information about sick leaves from current logged in doctor
        /// </summary>
        /// <param name="doctorID">Gets the doctorID from current logged in doctor</param>
        /// <returns>a list of found sick leaves</returns>
        public List<tblSickLeave> GetAllSickLeavesFromCurrentDoctor(int doctorID)
        {
            try
            {
                List<tblSickLeave> list = new List<tblSickLeave>();
                using (HospitalDBEntities context = new HospitalDBEntities())
                {
                    for (int j = 0; j < GetAllUsers().Count; j++)
                    {
                        // Searches for the users that contain dioctors ID
                        if (GetAllUsers()[j].DoctorID == doctorID)
                        {
                            for (int i = 0; i < GetAllSickLeaves().Count; i++)
                            {
                                // Gets all the sick leaves from that user
                                if (GetAllSickLeaves()[i].UserID == GetAllUsers()[j].UserID)
                                {
                                    list.Add(GetAllSickLeaves()[i]);
                                }
                            }
                        }
                    }

                    return list;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        /// <summary>
        /// Adds the patient
        /// </summary>
        /// <param name="user">the patient that is being added</param> 
        /// <returns>a new patient</returns>
        public tblUser AddUser(tblUser user)
        {
            try
            {
                using (HospitalDBEntities context = new HospitalDBEntities())
                {
                    if (user.UserID == 0)
                    {
                        tblUser newUser = new tblUser
                        {
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            JMBG = user.JMBG,
                            HealthIsuranceNumber = user.HealthIsuranceNumber,
                            Username = user.Username,
                            UserPassword = user.UserPassword,
                        };

                        context.tblUsers.Add(newUser);
                        context.SaveChanges();
                        user.UserID = newUser.UserID;

                        return newUser;
                    }
                    else
                    {
                        tblUser userToEdit = (from ss in context.tblUsers where ss.UserID == user.UserID select ss).First();

                        userToEdit.FirstName = user.FirstName;
                        userToEdit.LastName = user.LastName;
                        userToEdit.JMBG = user.JMBG;
                        userToEdit.HealthIsuranceNumber = user.HealthIsuranceNumber;
                        userToEdit.Username = user.Username;
                        userToEdit.UserPassword = user.UserPassword;
                        userToEdit.DoctorID = user.DoctorID;

                        userToEdit.UserID = user.UserID;
                        context.SaveChanges();

                        return user;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        /// <summary>
        /// Adds the doctor
        /// </summary>
        /// <param name="doctor">the doctor that is being added</param> 
        /// <returns>a new doctor</returns>
        public tblDoctor AddDoctor(tblDoctor doctor)
        {
            try
            {
                using (HospitalDBEntities context = new HospitalDBEntities())
                {
                    if (doctor.DoctorID == 0)
                    {
                        tblDoctor newDoctor = new tblDoctor
                        {
                            FirstName = doctor.FirstName,
                            LastName = doctor.LastName,
                            JMBG = doctor.JMBG,
                            BankAccount = doctor.BankAccount,
                            Username = doctor.Username,
                            UserPassword = doctor.UserPassword,
                        };

                        context.tblDoctors.Add(newDoctor);
                        context.SaveChanges();
                        newDoctor.DoctorID = newDoctor.DoctorID;

                        return newDoctor;
                    }
                    else
                    {
                        tblDoctor doctorToEdit = (from ss in context.tblDoctors where ss.DoctorID == doctor.DoctorID select ss).First();

                        doctorToEdit.FirstName = doctor.FirstName;
                        doctorToEdit.LastName = doctor.LastName;
                        doctorToEdit.JMBG = doctor.JMBG;
                        doctorToEdit.BankAccount = doctor.BankAccount;
                        doctorToEdit.Username = doctor.Username;
                        doctorToEdit.UserPassword = doctor.UserPassword;

                        doctorToEdit.DoctorID = doctor.DoctorID;
                        context.SaveChanges();

                        return doctor;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        /// <summary>
        /// Patient chooses a doctor
        /// </summary>
        /// /// <param name="user">current user</param>
        /// <param name="doctorID">the selected doctor id</param>
        /// <returns>the patient with the selected doctor</returns>
        public tblUser ChooseDoctor(tblUser user, int doctorID)
        {
            using (HospitalDBEntities context = new HospitalDBEntities())
            {
                tblUser userToEdit = (from ss in context.tblUsers where ss.UserID == user.UserID select ss).First();

                userToEdit.FirstName = user.FirstName;
                userToEdit.LastName = user.LastName;
                userToEdit.JMBG = user.JMBG;
                userToEdit.HealthIsuranceNumber = user.HealthIsuranceNumber;
                userToEdit.Username = user.Username;
                userToEdit.UserPassword = user.UserPassword;
                userToEdit.DoctorID = doctorID;

                userToEdit.UserID = user.UserID;
                context.SaveChanges();

                LoggedUser.CurrentUser.DoctorID = doctorID;
                return user;
            }
        }

        /// <summary>
        /// Adds the sick leave
        /// </summary>
        /// <param name="sickLeave">the sickLeave that is being added</param> 
        /// <returns>a new sickLeave</returns>
        public tblSickLeave AddSickLeave(tblSickLeave sickLeave)
        {
            try
            {
                using (HospitalDBEntities context = new HospitalDBEntities())
                {
                    if (sickLeave.SickLeaveID == 0)
                    {
                        tblSickLeave newSickLeave = new tblSickLeave
                        {
                            SickLeaveDate = sickLeave.SickLeaveDate,
                            Reason = sickLeave.Reason,
                            CompanyName = sickLeave.CompanyName,
                            EmergencyCase = sickLeave.EmergencyCase,
                            UserID = LoggedUser.CurrentUser.UserID
                        };

                        context.tblSickLeaves.Add(newSickLeave);
                        context.SaveChanges();
                        sickLeave.SickLeaveID = newSickLeave.SickLeaveID;

                        return newSickLeave;
                    }
                    else
                    {
                        tblSickLeave sickLeaveEdit = (from ss in context.tblSickLeaves where ss.SickLeaveID == sickLeave.SickLeaveID select ss).First();

                        sickLeaveEdit.SickLeaveDate = sickLeave.SickLeaveDate;
                        sickLeaveEdit.Reason = sickLeave.Reason;
                        sickLeaveEdit.CompanyName = sickLeave.CompanyName;
                        sickLeaveEdit.EmergencyCase = sickLeave.EmergencyCase;
                        sickLeaveEdit.UserID = sickLeave.UserID;

                        sickLeaveEdit.SickLeaveID = sickLeave.SickLeaveID;
                        context.SaveChanges();

                        return sickLeave;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }
    }
}
