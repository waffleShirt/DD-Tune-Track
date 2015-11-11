using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization; 

namespace DDTuneTrack
{
    class CultureHelper
    {
        private CultureInfo mSystemCulture;
        private string mDefaultDateFormatString;

        private static CultureHelper s_instance;

        /// <summary>
        /// Returns singleton CultureHelper instance.
        /// </summary>
        /// <returns>Singleton CultureHelper instance</returns>
        public static CultureHelper GetInstance()
        {
            if (s_instance == null)
            {
                s_instance = new CultureHelper();
            }
            return s_instance; 
        }

        /// <summary>
        /// Private constructor as part of Singleton Pattern.
        /// </summary>
        private CultureHelper() 
        {
            SetupCultureInformation(); 
        }

        /// <summary>
        /// Rturns the default date format string for the current system 
        /// culture. 
        /// </summary>
        /// <returns>Default date format string for current culture</returns>
        public string GetDefaultDateFormatString()
        {
            return mDefaultDateFormatString; 
        }

        /// <summary>
        /// Sets the culture information for the current user. The current
        /// culture is fetched and the default date format string for the 
        /// culture is generated. 
        /// </summary>
        private void SetupCultureInformation()
        {
            // Get the current culture
            mSystemCulture = CultureInfo.CurrentCulture;

            // Set the default date format based on the current culture
            switch (mSystemCulture.ToString())
            {
                case "en-AU":
                    mDefaultDateFormatString = "dd/MM/yyyy";
                    break;
                case "en-CA":
                case "en-US":
                    mDefaultDateFormatString = "MM/dd/yyyy";
                    break;
                default:
                    mDefaultDateFormatString = "MM/dd/yyyy";
                    break;
            }
        }
    }
}
