using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServices.Models
{
    public class EmailSetting
    {
        public string NoReplyMail { get; set; } = null!;
        public string NoReplyPassword { get; set; } = null!;
    }
}
