using System;

namespace Chatter.Application.ViewModels
{
    public class LogViewModel
    {
        public string Message { get; set; }
        public int Type { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public DateTime Created { get; set; }
    }
}