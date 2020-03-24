using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace CovidDataViewer
{
    public class MainViewModel : BaseVM
    {
        private ObservableCollection<LocationVM> locationData = new ObservableCollection<LocationVM>();
        public ObservableCollection<LocationVM> LocationData
        {
            get { return locationData; }
            set
            {
                locationData = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<string> fileNames = new ObservableCollection<string>();
        public ObservableCollection<string> FileNames
        {
            get { return fileNames; }
            set
            {
                fileNames = value;
                OnPropertyChanged();
            }
        }

        public void ReadDataFromAllCsv(List<string> files)
        {
            LocationData.Clear();
            foreach (string file in files)
            {
                ReadDataFromSingleCsv(file);
            }
        }

        public void ReadDataFromSingleCsv(string csvPath)
        {
            int counter = 0;
            string line;
            System.IO.StreamReader file = new System.IO.StreamReader(csvPath);
            while((line = file.ReadLine()) != null)
            {
                // Ignore the first line
                if (counter != 0)
                {
                    AddLineData(line);
                }
                counter++;
            }
            LocationData = new ObservableCollection<LocationVM>(LocationData.OrderBy(x => x.CountryName).ThenBy(x => x.ProvinceName));
            file.Close();
        }

        public void AddLineData(string line)
        {
            // Split the data out
            //var splitData = line.Split(',');
            var splitData = Regex.Split(line, ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");

            string province = string.Empty;
            string country = string.Empty;
            string lastUpdate = string.Empty;
            string confirmed = string.Empty;
            string deaths = string.Empty;
            string recovered = string.Empty;
            string latitude = string.Empty;
            string longitude = string.Empty;

            // Find what signature this matches
            // Sig 1: Province/State,Country/Region,Last Update,Confirmed,Deaths,Recovered
            //        Hubei,Mainland China,2020 - 02 - 29T12: 13:10,66337,2727,28993
            if (splitData.Length == 6)
            {
                province = splitData[0];
                country = splitData[1];
                lastUpdate = splitData[2];
                confirmed = splitData[3];
                deaths = splitData[4];
                recovered = splitData[5];
            }

            // Sig 2: Province/State,Country/Region,Last Update,Confirmed,Deaths,Recovered,Latitude,Longitude
            //        Hubei,China,2020-03-22T09:43:06,67800,3144,59433,30.9756,112.2707
            if (splitData.Length == 8)
            {
                province = splitData[0];
                country = splitData[1];
                lastUpdate = splitData[2];
                confirmed = splitData[3];
                deaths = splitData[4];
                recovered = splitData[5];
                latitude = splitData[6];
                longitude = splitData[7];
            }

            // Sig 3: FIPS,Admin2,Province_State,Country_Region,Last_Update,Lat,Long_,Confirmed,Deaths,Recovered,Active,Combined_Key
            //        45001,Abbeville,South Carolina,US,2020-03-23 23:19:34,34.22333378,-82.46170658,1,0,0,0,"Abbeville, South Carolina, US"
            if (splitData.Length == 12)
            {
                province = splitData[2];
                country = splitData[3];
                lastUpdate = splitData[4];
                confirmed = splitData[7];
                deaths = splitData[8];
                recovered = splitData[9];
                latitude = splitData[5];
                longitude = splitData[6];
            }




            var locationDataEntry = LocationData.Where(x => x.CountryName == country && x.ProvinceName == province).FirstOrDefault();
            if (locationDataEntry == null)
            {
                // Add the new location data
                LocationVM newLocationVM = new LocationVM
                {
                    CountryName = country,
                    ProvinceName = province,
                };
                if (latitude != string.Empty)
                {
                    newLocationVM.Latitude = double.Parse(latitude);
                    newLocationVM.Longitude = double.Parse(longitude);
                }
                LocationData.Add(newLocationVM);

                locationDataEntry = LocationData.Where(x => x.CountryName == country && x.ProvinceName == province).FirstOrDefault();
            }

            // The Location Data Entry should now exist, so read in the line
            try
            {
                DateData dateDataEntry = locationDataEntry.DateData.Where(x => x.DataDateTime == DateTime.Parse(lastUpdate)).FirstOrDefault();
                if (dateDataEntry == null)
                {
                    // If there is no entry, create it
                    DateData newDateData = new DateData();
                    newDateData.DataDateTime = DateTime.Parse(lastUpdate);
                    newDateData.ConfirmedCases = 0;
                    newDateData.Deaths = 0;
                    newDateData.Recovered = 0;
                    locationDataEntry.DateData.Add(newDateData);
                    dateDataEntry = locationDataEntry.DateData.Where(x => x.DataDateTime == DateTime.Parse(lastUpdate)).FirstOrDefault();
                }

                dateDataEntry.DataDateTime = DateTime.Parse(lastUpdate);

                if (confirmed != string.Empty)
                {
                    dateDataEntry.ConfirmedCases += long.Parse(confirmed);
                }

                if (deaths != string.Empty)
                {
                    dateDataEntry.Deaths += long.Parse(deaths);
                }

                if (recovered != string.Empty)
                {
                    dateDataEntry.Recovered += long.Parse(recovered);
                }


            }
            catch
            {
                //Ummm...
            }


        }



    }
}
