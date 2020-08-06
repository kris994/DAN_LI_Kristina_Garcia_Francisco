namespace DAN_LI_Kristina_Garcia_Francisco.Model
{
    /// <summary>
    /// Current logged in doctor data
    /// </summary>
    public static class LoggedDoctor
    {
        /// <summary>
        /// Current user
        /// </summary>
        private static tblDoctor currentDoctor;
        public static tblDoctor CurrentDoctor
        {
            get { return currentDoctor; }
            set { currentDoctor = value; }
        }
    }
}
