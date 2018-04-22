using System;
using Chatter.Domain.Core.Events;

namespace Chatter.Domain.Topics.Events
{
    public class AddedPostEvent : Event
    {
        public AddedPostEvent(int id, int idUsuario, int idTopico, string text, DateTime created)
        {
            Id = id;
            IdUsuario = idUsuario;
            IdTopico = idTopico;
            Text = text;
            Created = created;  
        }

        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public int IdTopico { get; set; }
        public string Text { get; set; }
        public DateTime Created { get; set; }
    }
}