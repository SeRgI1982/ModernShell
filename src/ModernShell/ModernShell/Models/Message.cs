using System;

namespace ModernShell.Models
{
    public class Message
    {
        public Message(int id, int contactId, string info, DateTime date)
        {
            Id = id;
            ContactId = contactId;
            Info = info;
            Date = date;
        }

        public int Id { get; }

        public int ContactId { get; }

        public string Info { get; }

        public DateTime Date { get; }
    }
}