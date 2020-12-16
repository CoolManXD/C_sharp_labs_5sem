using HotelAppWPF.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using HotelAppWPF.Commands;

namespace HotelAppWPF.ViewModel
{
    public enum Races : byte
    {
        Orc = 0,
        Elf
    }
    public class ApplicationViewModel : INotifyPropertyChanged
    {
        
        private ObservableCollection<HotelModel> hotels = new ObservableCollection<HotelModel>();
        private HotelModel selectedHotel;
        private RelayCommand addHotelCommand;
        private RelayCommand deleteHotelCommand;
        private HotelModel newHotel = new HotelModel { Name = "", Address = ""};
        public ObservableCollection<HotelModel> Hotels
        {
            get { return hotels; }
            set
            {
                hotels = value;
                OnPropertyChanged(nameof(Hotels));
            }
        }
        public HotelModel SelectedHotel
        {
            get { return selectedHotel; }
            set
            {
                selectedHotel = value;
                OnPropertyChanged(nameof(SelectedHotel));
            }
        }
        public HotelModel NewHotel
        {
            get { return newHotel; }
            set
            {
                newHotel = value;
                OnPropertyChanged(nameof(NewHotel));
            }
        }
        public RelayCommand AddHotelCommand
        {
            get
            {
                return addHotelCommand ??
                  (addHotelCommand = new RelayCommand(obj =>
                  {
                      HotelModel hotel = obj as HotelModel;
                      if (hotel is null)
                          return;
                      using (var client = new HttpClient())
                      {
                          var response = client.PostAsJsonAsync("https://localhost:44364/api/hotels", hotel).Result;
                          if (response.StatusCode == System.Net.HttpStatusCode.Created)
                          {
                              var result = response.Content.ReadAsStringAsync().Result;
                              HotelModel something = JsonConvert.DeserializeObject<HotelModel>(result);
                              if (something != null)
                                  Hotels.Add(something);
                              NewHotel = new HotelModel { Name = "", Address = "" };
                          }

                      }
                  }, 
                  obj => (obj as HotelModel)?.IsValid ?? false));
            }
        }
        public RelayCommand DeleteHotelCommand
        {
            get
            {
                return deleteHotelCommand ??
                  (deleteHotelCommand = new RelayCommand(obj =>
                  {
                      HotelModel hotel = obj as HotelModel;
                      if (hotel is null)
                          return;
                      int hotelId = (int) hotel.HotelId;

                      using (var client = new HttpClient())
                      {
                          string path = "https://localhost:44364/api/hotels/" + hotelId.ToString();
                          var response = client.DeleteAsync(path).Result;
                          if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                              hotels.Remove(hotel);
                      }
                  },
                  obj => (obj as HotelModel)?.IsValid ?? false));
            }
        }
        public ApplicationViewModel()
        {          
            GetHotels();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        private void GetHotels()
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync("https://localhost:44364/api/hotels?keyword=" + "").Result;
                var result = response.Content.ReadAsStringAsync().Result;
                IEnumerable<HotelModel> something = JsonConvert.DeserializeObject<IEnumerable<HotelModel>>(result);
                foreach(var temp in something)
                {
                    hotels.Add(temp);
                }
            }
        }
    }
}