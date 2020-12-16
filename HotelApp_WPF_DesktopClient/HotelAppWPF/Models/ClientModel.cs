using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace HotelAppWPF.Models
{
    public class ClientModel : INotifyPropertyChanged, IDataErrorInfo
    {
        private int clientId;
        private string firstName;
        private string lastName;
        private string phoneNumber;
        private List<ActiveOrderModel> activeOrders  = new List<ActiveOrderModel>();

        public int ClientId
        {
            get
            {
                return clientId;
            }
            set
            {
                clientId = value;
                OnPropertyChanged(nameof(ClientId));
            }
        }
        public string FirstName
        {
            get
            {
                return firstName;
            }
            set
            {
                firstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }
        public string LastName
        {
            get
            {
                return lastName;
            }
            set
            {
                lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }
        public string PhoneNumber
        {
            get
            {
                return phoneNumber;
            }
            set
            {
                phoneNumber = value;
                OnPropertyChanged(nameof(PhoneNumber));
            }
        }
        public List<ActiveOrderModel> ActiveOrders
        {
            get
            {
                return activeOrders;
            }
            set
            {
                activeOrders = value;
                OnPropertyChanged(nameof(ActiveOrders));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        // Валидация 
        Dictionary<string, string> errors = new Dictionary<string, string>();
        public string this[string columnName] => errors.ContainsKey(columnName) ? errors[columnName] : null;

        // Если все тексты ошибок null - данные валидные
        public bool IsValid => !errors.Values.Any(x => x != null);
        public string Error
        {
            get
            {
                return null;
            }
        }
    }
}
