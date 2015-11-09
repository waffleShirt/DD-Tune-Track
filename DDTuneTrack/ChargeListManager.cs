using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO; 

namespace DDTuneTrack
{
    class ChargeListManager
    {
        private static ChargeListManager s_instance = null;
        private List<ChargeList> mChargeLists; 

        public static ChargeListManager GetInstance()
        {
            if (s_instance == null)
            {
                s_instance = new ChargeListManager(); 
            }

            return s_instance; 
        }

        private ChargeListManager() 
        {
            mChargeLists = new List<ChargeList>(); 
        }
        
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

        public void LoadChargeLists()
        {
            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader("chargelists.txt", false))
                {
                    while (!sr.EndOfStream)
                    {
                        string dateString = sr.ReadLine();
                        DateTime date = DateTime.ParseExact(dateString, "d/MM/yyyy", null);

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

                        ChargeList cl = new ChargeList(date, charged, tuneRecords);
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
