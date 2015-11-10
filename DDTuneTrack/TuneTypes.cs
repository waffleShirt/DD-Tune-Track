using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO; 
using System.Windows.Forms; 

namespace DDTuneTrack
{
    /// <summary>
    /// Author: Tom Burridge
    /// Loads the tune types file and populates the tune types ComboBox in the
    /// main form. 
    /// </summary>
    public class TuneTypes
    {
        public static List<string> TuneTypesList = new List<string>();

        /// <summary>
        /// Loads the tune types file and populates a ComboBox with the loaded
        /// values. 
        /// </summary>
        /// <param name="tuneTypesComboBox">ComboBox to populate with loaded values.</param>
        public static void LoadTuneTypesList(ComboBox tuneTypesComboBox)
        {
            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader("tunetypes.txt"))
                {
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        TuneTypesList.Add(line); 
                        tuneTypesComboBox.Items.Add(line); 
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }
    }
}
