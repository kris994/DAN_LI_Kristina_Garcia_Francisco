using DAN_LI_Kristina_Garcia_Francisco.Model;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace DAN_LI_Kristina_Garcia_Francisco.Helper
{
    /// <summary>
    /// Validates the user inputs
    /// </summary>
    class Validations
    {
        /// <summary>
        /// Checks if the Username is exists
        /// </summary>
        /// <param name="username">the username we are checking</param>
        /// <param name="id">for the specific user</param>
        /// <param name="type">type of jmbg</param>
        /// <returns>null if the input is correct or string error message if its wrong</returns>
        public string UsernameChecker(string username, int id, string type)
        {
            Service service = new Service();

            // Get all usernames in the database
            List<string> allUsernames = new List<string>();
            for (int k = 0; k < service.GetAllUsers().Count; k++)
            {
                allUsernames.Add(service.GetAllUsers()[k].Username);
            }
            for (int m = 0; m < service.GetAllDoctors().Count; m++)
            {
                allUsernames.Add(service.GetAllDoctors()[m].Username);
            }

            if (username == null)
            {
                return "Username cannot be empty.";
            }

            if (type == "patient")
            {
                // Check if the username already exists
                for (int i = 0; i < allUsernames.Count; i++)
                {
                    if (allUsernames[i] == username)
                    {
                        return "This Username already exists!";
                    }
                }
            }
            else if (type == "doctor")
            {
                // Check if the username already exists
                for (int i = 0; i < allUsernames.Count; i++)
                {
                    if (allUsernames[i] == username)
                    {
                        return "This Username already exists!";
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Calculates the date of birth for the given jmbg
        /// </summary>
        /// <param name="jmbg">given jmbg</param>
        /// <returns>the date of birth</returns>
        public DateTime CountDateOfBirth(string jmbg)
        {
            DateTime dt = default(DateTime);

            // Get the date of birth
            if (jmbg[4] == '0')
            {
                string date = jmbg.Substring(0, 2) + "/" + jmbg.Substring(2, 2) + "/" + "2" + jmbg.Substring(4, 3);
                try
                {
                    dt = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    return dt;
                }
                catch (FormatException)
                {
                    dt = default(DateTime);
                    return dt;
                }
            }
            if (jmbg[4] == '9')
            {
                string date = jmbg.Substring(0, 2) + "/" + jmbg.Substring(2, 2) + "/" + "1" + jmbg.Substring(4, 3);
                try
                {
                    dt = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    return dt;
                }
                catch (FormatException)
                {
                    dt = default(DateTime);
                    return dt;
                }
            }
            return dt;
        }

        /// <summary>
        /// Checks if the jmbg is correct
        /// </summary>
        /// <param name="jmbg">the jmbg we are checking</param>
        /// <param name="id">for the specific user</param>
        /// <param name="type">type of jmbg</param>
        /// <returns>jmbg if the input is correct or null if its wrong</returns>
        public string JMBGChecker(string jmbg, int id, string type)
        {
            Service service = new Service();
            DateTime dt = default(DateTime);

            // Get all jmbgs in the database
            List<string> allJMBG = new List<string>();
            for (int k = 0; k < service.GetAllUsers().Count; k++)
            {
                allJMBG.Add(service.GetAllUsers()[k].JMBG);
            }
            for (int m = 0; m < service.GetAllDoctors().Count; m++)
            {
                allJMBG.Add(service.GetAllDoctors()[m].JMBG);
            }

            if (jmbg == null)
            {
                return "JMBG cannot be empty.";
            }

            if (type == "patient")
            {
                // Check if the jmbg already exists
                for (int i = 0; i < service.GetAllUsers().Count; i++)
                {
                    if (allJMBG[i] == jmbg)
                    {
                        return "This JMBG already exists!";
                    }
                }
            }
            else if (type == "doctor")
            {
                // Check if the jmbg already exists
                for (int i = 0; i < allJMBG.Count; i++)
                {
                    if (allJMBG[i] == jmbg)
                    {
                        return "This JMBG already exists!";
                    }
                }
            }

            if (!(jmbg.Length == 13))
            {
                return "Please enter a number with 13 characters.";
            }

            // Get date
            dt = CountDateOfBirth(jmbg);

            if (dt == default(DateTime))
            {
                return "Incorrect JMBG Format.";
            }

            return null;
        }

        /// <summary>
        /// Checks if the account is correct
        /// </summary>
        /// <param name="account">the account we are checking</param>
        /// <param name="id">for the specific account</param>
        /// <returns>jmbg if the input is correct or null if its wrong</returns>
        public string BackAccount(string account, int id)
        {
            Service service = new Service();

            if (account == null || account.Length < 4)
            {
                return "Value has to be at least 4 characters long.";
            }

            // Check if the account already exists
            for (int i = 0; i < service.GetAllDoctors().Count; i++)
            {
                if (service.GetAllDoctors()[i].BankAccount == account)
                {
                    return "This Account already exists!";
                }
            }
            return null;
        }

        /// <summary>
        /// Checks if the health insurance is correct
        /// </summary>
        /// <param name="health">the health insurance we are checking</param>
        /// <param name="id">for the specific health insurance </param>
        /// <returns>jmbg if the input is correct or null if its wrong</returns>
        public string HealthNumber(string health, int id)
        {
            Service service = new Service();

            if (health == null || health.Length != 9) 
            {
                return "Health Insurance has to be 9 characters long.";
            }

            // Check if the health already exists
            for (int i = 0; i < service.GetAllUsers().Count; i++)
            {
                if (service.GetAllUsers()[i].HealthIsuranceNumber == health)
                {
                    return "This Health Insurance already exists!";
                }
            }
            return null;
        }
    }
}
