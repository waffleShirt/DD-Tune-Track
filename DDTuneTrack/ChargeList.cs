using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DDTuneTrack
{
    class ChargeList
    {
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

        public ChargeList()
        {
            mListDate = DateTime.Now;
            mTuneRecords = new List<TuneRecord>();
            mNotes = new List<string>(); 
        }

        public ChargeList(DateTime date, bool charged, List<TuneRecord> tuneRecords, List<String> notes)
        {
            mListDate = date;
            mCharged = charged;
            mTuneRecords = tuneRecords;
            mNotes = notes; 
        }

        public DateTime GetDate()
        {
            return mListDate;
        }

        public bool GetCharged()
        {
            return mCharged;
        }

        public List<string> GetNotes()
        {
            return mNotes;
        }

        public List<TuneRecord> GetTuneRecords()
        {
            return mTuneRecords;
        }

        public void ToggleCharged()
        {
            mCharged = !mCharged; 
        }

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

        public void AddTune(string tuneType, string notes)
        {
            for (int i = 0; i < mTuneRecords.Count; ++i)
            {
                if (mTuneRecords[i].mTuneType == tuneType)
                {
                    mTuneRecords[i].mCount += 1;
                    mNotes.Add(notes); 
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

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Charge List Date: " + mListDate.ToShortDateString());
            sb.AppendLine(); 
            foreach (TuneRecord tr in mTuneRecords)
            {
                sb.AppendLine(tr.mTuneType + ": " + tr.mCount);  
            }

            sb.AppendLine();
            sb.AppendLine("Notes and Parts"); 

            foreach (string note in mNotes)
            {
                sb.AppendLine(note); 
            }

            return sb.ToString(); 
        }

        public void MarkCharged(bool charged)
        {
            mCharged = charged;
        }
    }
}
