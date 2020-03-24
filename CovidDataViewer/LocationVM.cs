using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CovidDataViewer
{
    public class LocationVM : BaseVM
    {

        private ObservableCollection<DateData> dateData = new ObservableCollection<DateData>();
        public ObservableCollection<DateData> DateData
        {
            get { return dateData; }
            set
            {
                dateData = value;
                OnPropertyChanged();
            }
        }

        private string provinceName;
        public string ProvinceName
        {
            get { return provinceName; }
            set
            {
                provinceName = value;
                OnPropertyChanged();
            }
        }

        private string countryName;
        public string CountryName
        {
            get { return countryName; }
            set
            {
                countryName = value;
                OnPropertyChanged();
            }
        }

        private long barScale;
        public long BarScale
        {
            get { return barScale; }
            set
            {
                barScale = value;
                OnPropertyChanged();
            }
        }

        private long totalConfirmedForLocation;
        public long TotalConfirmedForLocation
        {
            get { return totalConfirmedForLocation; }
            set
            {
                totalConfirmedForLocation = value;
                OnPropertyChanged();
            }
        }

        private long totalDeathsForLocation;
        public long TotalDeathsForLocation
        {
            get { return totalDeathsForLocation; }
            set
            {
                totalDeathsForLocation = value;
                OnPropertyChanged();
            }
        }

        private long totalRecoveredForLocation;
        public long TotalRecoveredForLocation
        {
            get { return totalRecoveredForLocation; }
            set
            {
                totalRecoveredForLocation = value;
                OnPropertyChanged();
            }
        }

        private double confirmedToDeathRatio;
        public double ConfirmedToDeathRatio
        {
            get { return confirmedToDeathRatio; }
            set
            {
                confirmedToDeathRatio = value;
                OnPropertyChanged();
            }
        }


        private double latitude;
        public double Latitude
        {
            get { return latitude; }
            set
            {
                latitude = value;
                OnPropertyChanged();
            }
        }

        private double longitude;
        public double Longitude
        {
            get { return longitude; }
            set
            {
                longitude = value;
                OnPropertyChanged();
            }
        }

        public void RefreshTotalsByLocation()
        {
            long highestConfirmedTotal = 0;
            long highestDeathTotal = 0;
            long highestRecoveredTotal = 0;

            foreach (DateData dateData in DateData)
            {
                if (dateData.ConfirmedCases > highestConfirmedTotal)
                {
                    highestConfirmedTotal = dateData.ConfirmedCases;
                }
                if (dateData.Deaths > highestDeathTotal)
                {
                    highestDeathTotal = dateData.Deaths;
                }
                if (dateData.Recovered > highestRecoveredTotal)
                {
                    highestRecoveredTotal = dateData.Recovered;
                }

            }

            TotalConfirmedForLocation = highestConfirmedTotal;
            TotalDeathsForLocation = highestDeathTotal;
            TotalRecoveredForLocation = highestRecoveredTotal;
            BarScale = TotalConfirmedForLocation;

            ConfirmedToDeathRatio = (double)TotalDeathsForLocation / totalConfirmedForLocation;
        }

        public void ProjectData(int daysOutToProject, bool worstCase)
        {
            List<double> allConfirmedIncreases = new List<double>();
            List<double> allDeathIncreases = new List<double>();
        }


    }
}
