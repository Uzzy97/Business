using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Data
{
    public class NoteTake
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ID { get; set; }
        //public object CreatedDate { get; internal set; }

        public NoteTake()
        {
            CreatedDate = DateTime.Now;
            ID = Guid.NewGuid().ToString();
        }

        public override string ToString() => $"{Title} {CreatedDate}";
        
    }
}
