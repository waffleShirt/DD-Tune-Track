using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms; 

namespace DDTuneTrack
{
    /// <summary>
    /// Author: Tom Burridge
    /// Manages the collection of ChargeList objects that are associated with
    /// the application. The manager stores the list of ChargeList objects that
    /// can be displayed in the application and provides the functionality to
    /// load in all existing ChargeLists at application start and write out all
    /// applications to disk at application close. The manager is a singleton 
    /// class. 
    /// </summary>
    class ChargeListManager
    {
        private static ChargeListManager s_instance = null;
        private List<ChargeList> mChargeLists; 

        /// <summary>
        /// Returns the single ChargeListManager instance. 
        /// </summary>
        /// <returns>Manager instance</returns>
        public static ChargeListManager GetInstance()
        {
            if (s_instance == null)
            {
                s_instance = new ChargeListManager(); 
            }

            return s_instance; 
        }

        /// <summary>
        /// Private Constructor. Hidden as part of the Singleton Pattern. 
        /// </summary>
        private ChargeListManager() 
        {
            mChargeLists = new List<ChargeList>(); 
        }
        
        /// <summary>
        /// Adds a new ChargeList to the storage list. If a ChargeList with the
        /// same date as an existing ChargeList is found the two will be 
        /// merged. 
        /// </summary>
        /// <param name="cl"></param>
        public void AddNewChargeList(ChargeList cl)
        {
            // Look for an existing tune list with the same
            // date as the one being passed in. If one exists
            // we add the data to the existing tune list. 
            // !!!! Note that if someone has already marked the list charged and adds new values then they probably won't get charged, human error, blah blah not interested in fixing that right now
            // Could maybe just mark which items have been charged, new ones will show as uncharged, get rid of big label that says charged/not charged?
            for (int i = 0; i < mChargeLists.Count; ++i)
            {
                if (mChargeLists[i].GetDate().Date == cl.GetDate().Date)
                {
                    mChargeLists[i].MergeChargeLists(cl);
                    return; 
                }
            }

            mChargeLists.Add(cl);            
        }

        /// <summary>
        /// Returns the string representation of a charge list for a given 
        /// date. If no charge list for the date is found nothing happens. 
        /// </summary>
        /// <param name="date">ChargeList date</param>
        /// <returns>String representation of ChargeList data</returns>
        public string GetChargeListTextForDate(DateTime date)
        {
            foreach (ChargeList cl in mChargeLists)
            {
                if (cl.GetDate().Date == date.Date)
                {
                    return cl.ToString();
                }
            }

            return "No charge list found for " + date.ToShortDateString(); 
        }

        /// <summary>
        /// Returns the charged status for a ChargeList on a particular date.
        /// If no charge list for the date is found nothing happens. 
        /// </summary>
        /// <param name="date">Date of ChargeList</param>
        /// <returns>True if charged, False if not charged</returns>
        public bool GetChargedListChargedStatusForDate(DateTime date)
        {
            foreach (ChargeList cl in mChargeLists)
            {
                if (cl.GetDate().Date == date.Date)
                {
                    return cl.GetCharged(); 
                }
            }

            return false; 
        }

        /// <summary>
        /// Flips the charged status for ChargeList on a given date. If no 
        /// charge list for the date is found nothing happens. 
        /// </summary>
        /// <param name="date"></param>
        public void ToggleChargedStatusForDate(DateTime date)
        {
            foreach (ChargeList cl in mChargeLists)
            {
                if (cl.GetDate().Date == date.Date)
                {
                    cl.ToggleCharged(); 
                }
            }
        }

        /// <summary>
        /// Builds a ChargeList from a collection for data rows from a 
        /// TuneList. 
        /// </summary>
        /// <param name="tl">TuneList to build from</param>
        /// <returns>Newly created ChargeList</returns>
        public ChargeList BuildChargeList(TuneList tl)
        {
            ChargeList cl = new ChargeList();
            DataGridViewRowCollection tuneRows = tl.GetAllTuneRows(); 

            foreach (DataGridViewRow row in tuneRows)
            {
                string noteCell = row.Cells["colNotes"].Value.ToString();
                if (noteCell != string.Empty)
                {
                    cl.AddTune(row.Cells["colTuneType"].Value.ToString(), row.Cells["colAssetNumber"].Value.ToString() + ": " + noteCell);
                }
                else
                {
                    cl.AddTune(row.Cells["colTuneType"].Value.ToString(), string.Empty);
                }
            }

            return cl;
        }

        /// <summary>
        /// Builds a ChargeList from a collection for data rows from a 
        /// TuneList. Also allows the creation date of the ChargeList to be set
        /// manually. 
        /// </summary>
        /// <param name="tl">TuneList to build from</param>
        /// <param name="date">Creation date of ChargeList</param>
        /// <returns>Newly created ChargeList</returns>
        public ChargeList BuildChargeList(TuneList tl, DateTime date)
        {
            ChargeList cl = new ChargeList(date);
            DataGridViewRowCollection tuneRows = tl.GetAllTuneRows();

            foreach (DataGridViewRow row in tuneRows)
            {
                string noteCell = row.Cells["colNotes"].Value.ToString();
                if (noteCell != string.Empty)
                {
                    cl.AddTune(row.Cells["colTuneType"].Value.ToString(), row.Cells["colAssetNumber"].Value.ToString() + ": " + noteCell);
                }
                else
                {
                    cl.AddTune(row.Cells["colTuneType"].Value.ToString(), string.Empty);
                }
            }

            return cl;
        }

        /// <summary>
        /// Writes all stored ChargeLists out to disk. 
        /// </summary>
        public void WriteChargeListsToDisk()
        {
            try
            {   
                // Open the text file using a stream reader.
                using (StreamWriter sw = new StreamWriter("chargelists.txt", false))
                {
                    foreach (ChargeList cl in mChargeLists)
                    {
                        sw.WriteLine(cl.GetDate().ToShortDateString());
                        sw.WriteLine(cl.GetCharged().ToString());

                        List<ChargeList.TuneRecord> tuneRecords = cl.GetTuneRecords();
                        foreach (ChargeList.TuneRecord tr in tuneRecords)
                        {
                            sw.WriteLine(tr.mTuneType + " " + tr.mCount); 
                        }

                        // Write a blank line to seperate the tunes and notes
                        sw.WriteLine(); 

                        // Write Notes
                        List<string> notes = cl.GetNotes();
                        foreach (string note in notes)
                        {
                            sw.WriteLine(note); 
                        }

                        // End of record, insert newline
                        sw.WriteLine(); 
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Reads in all stored ChargeLists on disk and stores them in the 
        /// managers internal storage list. 
        /// </summary>
        public void LoadChargeLists()
        {
            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader("chargelists.txt", false))
                {
                    while (!sr.EndOfStream)
                    {
                        string dateString = sr.ReadLine();
                        DateTime date = DateTime.ParseExact(dateString, CultureHelper.GetInstance().GetDefaultDateFormatString(), null);

                        bool charged = Boolean.Parse(sr.ReadLine());

                        List<ChargeList.TuneRecord> tuneRecords = new List<ChargeList.TuneRecord>();

                        string line;
                        while ((line = sr.ReadLine()) != string.Empty)
                        {
                            string tuneLine = line;
                            string tuneType = tuneLine.Substring(0, tuneLine.LastIndexOf(" "));
                            string tuneCount = tuneLine.Substring(tuneLine.LastIndexOf(" ") + 1, tuneLine.Length - tuneLine.LastIndexOf(" ") - 1);

                            ChargeList.TuneRecord tr = new ChargeList.TuneRecord();
                            tr.mTuneType = tuneType;
                            tr.mCount = Int32.Parse(tuneCount);

                            tuneRecords.Add(tr);
                        }

                        List<string> notes = new List<string>(); 
                        while ((line = sr.ReadLine()) != string.Empty)
                        {
                            notes.Add(line); 
                        }

                        ChargeList cl = new ChargeList(date, charged, tuneRecords, notes);
                        ChargeListManager.GetInstance().AddNewChargeList(cl); 
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
