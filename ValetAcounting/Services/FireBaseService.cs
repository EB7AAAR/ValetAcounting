using Firebase.Database;
using Firebase.Database.Query;
using System.Collections.ObjectModel;
using ValetAcounting.Model;

namespace ValetAcounting.Services
{
    public class FireBaseService
    {
        FirebaseClient firebaseClient;
        public FireBaseService(
            ObservableCollection<Record> records)
        {
            this.Records = records;
            Connect();
        }

        public ObservableCollection<Record> Records { get; }

        async void Connect()
        {
            var auth = "VpHKztdyUC6A8omWOdUsvsBJGWOcNluSttprQrc6"; // your app secret
            firebaseClient = new FirebaseClient(
              "https://valet-9349d-default-rtdb.firebaseio.com/",
              new FirebaseOptions
              {
                  AuthTokenAsyncFactory = () => Task.FromResult(auth)
              });
        }
        public async Task<bool> CheckSingleRecord(Record record)
        {
            var x =
                (record.Date.Year).ToString() +
                "-" +
                (record.Date.Month).ToString() +
                "-" +
                (record.Date.Day).ToString() +
                "-" +
                record.ID;

            if( await firebaseClient
                .Child("PermanentRecords")
                .Child(x)
                .OnceSingleAsync<Record>() is not null )
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task SendRecords(Record rec)
        {
            var x =
                (rec.Date.Year).ToString() +
                "-" +
                (rec.Date.Month).ToString() +
                "-" +
                (rec.Date.Day).ToString() +
                "-"+
                rec.ID;
                
            await firebaseClient
                .Child("Records")
                .Child(x)
                .PutAsync(rec);
            await firebaseClient
                .Child("PermanentRecords")
                .Child(x)
                .PutAsync(rec);
        }

    }
}
