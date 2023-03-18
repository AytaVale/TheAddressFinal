using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheAddress.BLL.Exceptions
{
    public class CustomException : Exception
    {
        private string ExcMessage { get; set; }
        public CustomException() : base() { }

        public CustomException(string msj) : base()

        {
            ExcMessage = msj;
        }

        public override string Message
        {
            get
            {
                return ExcMessage;
            }
        }

        public override string StackTrace
        {
            get
            {
                return "";
            }
        }



    }
}

