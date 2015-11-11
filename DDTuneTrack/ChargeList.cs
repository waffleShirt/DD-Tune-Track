using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DDTuneTrack
{
    /// <summary>
    /// Author: Tom Burridge
    /// A ChargeList is an encapsulation of all the information needed to 
    /// charge a list of tunes from a given day/days. A charge list holds the
    /// date the list was created, the number of each tune type and all the 
    /// notes attached to each tune which aids in charging parts. Charge lists
    /// can be merged in the event that two charge lists created on the same 
    /// date exist. 
    /// </summary>
    class ChargeList
    {
        /// <summary>
        /// Simple class to store the type and count for each tune record. 
        /// </summary>
        public class TuneRecord
        {
            public string mTuneType;
            public int mCount;  
        }

        // Charge list attributes
        private DateTime mListDate;
        private List<TuneRecord> mTuneRecords; 
        private bool mCharged = false;
        private List<string> mNotes; 

        /// <summary>
        /// ChargeList Constructor
        /// </summary>
        public ChargeList()
        {
            mListDate = DateTime.Now;
            mTuneRecords = new List<TuneRecord>();
            mNotes = new List<string>(); 
        }

        /// <summary>
        /// Overloaded ChargeList Constructor. Allows the creation date of a 
        /// charge list to be set manually. 
        /// </summary>
        /// <param name="date">ChargeList creation date</param>
        public ChargeList(DateTime date)
        {
            mListDate = date;
            mTuneRecords = new List<TuneRecord>();
            mNotes = new List<string>();
        }

        /// <summary>
        /// Overloaded ChargeList Constructor. Allows all internal data to be 
        /// set at creation time. Used to create charge lists that are loaded
        /// from file at application startup. 
        /// </summary>
        /// <param name="date">Charge List creation date</param>
        /// <param name="charged">Charge List charge status</param>
        /// <param name="tuneRecords">List of tune records in Charge List</param>
        /// <param name="notes">List of notes to be added to Charge List</param>
        public ChargeList(DateTime date, bool charged, List<TuneRecord> tuneRecords, List<String> notes)
        {
            mListDate = date;
            mCharged = charged;
            mTuneRecords = tuneRecords;
            mNotes = notes; 
        }

        /// <summary>
        /// Get creation date of Charge List. 
        /// </summary>
        /// <returns>Creation date</returns>
        public DateTime GetDate()
        {
            return mListDate;
        }

        /// <summary>
        /// Get charge status of Charge List. 
        /// </summary>
        /// <returns>Charge status, charged if True, not charged if False</returns>
        public bool GetCharged()
        {
            return mCharged;
        }

        /// <summary>
        /// Get list of notes from Charge List. 
        /// </summary>
        /// <returns>List of notes</returns>
        public List<string> GetNotes()
        {
            return mNotes;
        }

        /// <summary>
        /// Get list of tune records from Charge List.
        /// </summary>
        /// <returns>List of tune records</returns>
        public List<TuneRecord> GetTuneRecords()
        {
            return mTuneRecords;
        }

        /// <summary>
        /// Flips the charge status of a Charge List
        /// </summary>
        public void ToggleCharged()
        {
            mCharged = !mCharged; 
        }

        /// <summary>
        /// Merges two charge lists. For each tune type matching in both the
        /// charge lists the number of tunes are summed. For any tune type not
        /// found in both lists a new tune record is added to the one Charge 
        /// List that will be kept. 
        /// </summary>
        /// <param name="other">Other Charge List to merge from</param>
        public void MergeChargeLists(ChargeList other)
        {
            List<TuneRecord> otherTuneRecords = other.GetTuneRecords();

            // Merge notes first
            mNotes.AddRange(other.GetNotes()); 

            // Merge tune records
            for (int i = 0; i < otherTuneRecords.Count; ++i)
            {
                bool matchFound = false; 
                for (int j = 0; j < mTuneRecords.Count; ++j)
                {
                    if (otherTuneRecords[i].mTuneType == mTuneRecords[j].mTuneType)
                    {
                        // There is a matching tune type in both the new and old list, merge the counts
                        mTuneRecords[j].mCount += otherTuneRecords[i].mCount;
                        matchFound = true;
                        break; 
                    }
                }

                if (!matchFound)
                {
                    // No match for this record was found so just add it to the existing list
                    TuneRecord tr = new TuneRecord();
                    tr.mTuneType = otherTuneRecords[i].mTuneType;
                    tr.mCount = otherTuneRecords[i].mCount;
                    mTuneRecords.Add(tr);
                }
            }
        }

        /// <summary>
        /// Adds a new tune to the Charge List
        /// </summary>
        /// <param name="tuneType">Type of tune</param>
        /// <param name="notes">Any notes related to the tune</param>
        public void AddTune(string tuneType, string notes)
        {
            for (int i = 0; i < mTuneRecords.Count; ++i)
            {
                if (mTuneRecords[i].mTuneType == tuneType)
                {
                    mTuneRecords[i].mCount += 1;

                    if (notes != string.Empty)
                    {
                        mNotes.Add(notes);
                    }
                    
                    return; 
                }
            }

            // Tune type wasn't found in array. Add it
            TuneRecord tr = new TuneRecord();
            tr.mTuneType = tuneType;
            tr.mCount = 1;
            mTuneRecords.Add(tr);
            mNotes.Add(notes); 
        }

        /// <summary>
        /// Overridden ToString function. Formats the data from the Charge List
        /// and returns it so that it can be displayed elsewhere. 
        /// </summary>
        /// <returns>Formatted Charge List data</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Charge List Date: " + mListDate.ToString(CultureHelper.GetInstance().GetDefaultDateFormatString()));
            sb.AppendLine(); 
            foreach (TuneRecord tr in mTuneRecords)
            {
                sb.AppendLine(tr.mTuneType + ": " + tr.mCount);  
            }

            sb.AppendLine();
            sb.AppendLine("Notes and Parts");

            foreach (string note in mNotes)
            {
                if (note != string.Empty)
                {
                    sb.AppendLine(note);
                }
            }

            return sb.ToString(); 
        }

        /// <summary>
        /// Set the chare status of a charge list. 
        /// </summary>
        /// <param name="charged">Charged if True, not charged if False</param>
        public void MarkCharged(bool charged)
        {
            mCharged = charged;
        }
    }
}
