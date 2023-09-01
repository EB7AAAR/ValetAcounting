using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using ValetAcounting.Model;
using ValetAcounting.Services;

namespace ValetAcounting.ViewModel
{
    public partial class RecordsViewModel : BaseViewModel
    {
        public ObservableCollection<Record> Records { get; } = new();
        
        [ObservableProperty]
        string income;
        [ObservableProperty]
        string workers;
        [ObservableProperty]
        string dailyExp;
        [ObservableProperty]
        string dailyNet;
        [ObservableProperty]
        string tip;
        [ObservableProperty]
        DateTime date = DateTime.Now;
        [ObservableProperty]
        bool isDeliverable;
        [ObservableProperty]
        DateTime minDate;
        [ObservableProperty]
        DateTime maxDate;

        [ObservableProperty]
        string displayName;


        FireBaseService fireBaseService;
        IConnectivity connectivity;

        public RecordsViewModel(IConnectivity connectivity)
        {
            this.Title = "Records";
            fireBaseService = new FireBaseService(Records);
            this.connectivity = connectivity;
           
            MaxDate = new DateTime(Date.Year, Date.Month, Date.Day);
            MinDate = maxDate.AddDays(-1);

        }

        [RelayCommand]
        async Task SendRecordAsync()
        {
            //TODO:1- add number of employees
            //TODO:2- prohibit adding more than one record for the same day
            //TODO:3- make all days show on the day view remove charts
            //TODO:4- remove tips from chart
            //TODO:5- apple apk
            //TODO:6- dateTime picker to only show months
            //TODO:7- design pages for easier access
            //TODO:8- decimal for numbers
            //TODO:9- Ex/in for days 
            //TODO:10- pie charts .. 2 colors ,, writing inside -- remove tips 
            //TODO:11- colors styles and themes 
            //TODO:12- tabs instead of buttons

            //dotnet publish -c Release -f:net7.0-android

            if (connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("Connection failure","No internet available", "Ok");
                return;
            }
            //await SendSampleRecords();

            Record rec = new()
            {
                ID = DisplayName,
                Date = Date.Date + DateTime.Now.TimeOfDay,
                Income = Convert.ToDouble(Income),
                Workers = Convert.ToInt32(Workers),
                DailyExp = Convert.ToDouble(DailyExp),
                DailyNet = Convert.ToDouble(DailyNet),
                Tip = Convert.ToDouble(Tip)
            };

            bool result = await fireBaseService.CheckSingleRecord(rec);
            if (result)
            {
                await Shell.Current.DisplayAlert("Record Exists", "Record for this day already exist", "Ok");
                return;
            }

            await fireBaseService.SendRecords(rec);
            await Shell.Current.DisplayAlert("Sending Data", "Data Sent Thank You", "Ok");
            Workers = "";
            DailyExp = "";
            DailyNet = "";
            Income = "";
            Tip = "";
            IsDeliverable = false;
        }
    }
}
