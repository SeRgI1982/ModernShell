using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace ModernShell.Models
{
    public class Contact : NotifyPropertyBase
    {
        private const string ProfilesFolder = @"../Assets/";

        private string _filteringRecord;
        private Statuses _status;
        private readonly ObservableCollection<Message> _messages;

        public Contact(int id, string firstName, string lastName, string icon)
        {
            _messages = new ObservableCollection<Message>();

            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Icon = ProfilesFolder + icon;
            
            FilteringRecord = BuildFilteringRecord();
        }

        public int Id { get; }

        public string FirstName { get; }

        public string LastName { get; }

        public string Icon { get; }

        public INotifyCollectionChanged Messages => _messages;

        public Statuses Status
        {
            get { return _status; }
            private set { SetProperty(ref _status, value); }
        }

        public string FilteringRecord
        {
            get { return _filteringRecord; }
            private set { SetProperty(ref _filteringRecord, value); }
        }

        public string LastMessage
        {
            get { return _messages.OrderByDescending(x => x.Date).Select(x => x.Info).FirstOrDefault(); }
        }
        
        public void AddMessages(Message[] messages)
        {
            if (messages != null && messages.Any())
            {
                _messages.AddRange(messages.Where(m => m.ContactId == Id && _messages.All(x => x.Id != m.Id)));
                FilteringRecord = BuildFilteringRecord();
            }
        }

        public void ChangeStatus(Statuses status)
        {
            Status = status;
        }

        private string BuildFilteringRecord()
        {
            return string.Join(";", new[] { FirstName, LastName }.Concat(_messages.Select(x => x.Info)).Where(x => !string.IsNullOrWhiteSpace(x))).ToLowerInvariant();
        }
    }
}
