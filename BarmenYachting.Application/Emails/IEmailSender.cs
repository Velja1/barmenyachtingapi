using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarmenYachting.Application.Emails
{
    public interface IEmailSender
    {
        void Send(MessageDto message);
    }


}
