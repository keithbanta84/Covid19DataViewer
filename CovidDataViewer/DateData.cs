using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CovidDataViewer
{
    public class DateData : BaseVM
    {
        private bool projectedData = false;
        public bool ProjectedData
        {
            get { return projectedData; }
            set
            {
                projectedData = value;
                OnPropertyChanged();
            }
        }

        private DateTime dataDateTime;
        public DateTime DataDateTime
        {
            get { return dataDateTime; }
            set
            {
                dataDateTime = value;
                OnPropertyChanged();
            }
        }

        private long confirmedCases;
        public long ConfirmedCases
        {
            get { return confirmedCases; }
            set
            {
                confirmedCases = value;
                OnPropertyChanged();
            }
        }

        private long deaths;
        public long Deaths
        {
            get { return deaths; }
            set
            {
                deaths = value;
                OnPropertyChanged();
            }
        }

        private long recovered;
        public long Recovered
        {
            get { return recovered; }
            set
            {
                recovered = value;
                OnPropertyChanged();
            }
        }

    }
}
