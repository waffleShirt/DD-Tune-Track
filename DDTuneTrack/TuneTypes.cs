using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO; 
using System.Windows.Forms; 

namespace DDTuneTrack
{
    /******************************************************
    * Class: Tune Types
    * Author: Tom Burridge
    * Date: 5/11/2015
    */

    public class TuneTypes
    {
        public static List<string> TuneTypesList = new List<string>();

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
