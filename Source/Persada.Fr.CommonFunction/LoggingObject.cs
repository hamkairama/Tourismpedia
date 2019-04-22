#region .Net Base C;ass Namespace Imports
using System;
using System.Data;
using System.Text;
#endregion

/// <summary>
/// Summary description for Result
/// </summary>
namespace Persada.Fr.CommonFunction
{
    [Serializable]
    public class LoggingObject
    {
        #region Private Variables
        private string url;
        private string request;
        private string respon;
        private string exceptionMessage;
        private string exceptionStack;

        public string Url
        {
            get
            {
                return url;
            }

            set
            {
                url = value;
            }
        }

        public string Request
        {
            get
            {
                return request;
            }

            set
            {
                request = value;
            }
        }

        public string Respon
        {
            get
            {
                return respon;
            }

            set
            {
                respon = value;
            }
        }

        public string ExceptionMessage
        {
            get
            {
                return exceptionMessage;
            }

            set
            {
                exceptionMessage = value;
            }
        }

        public string ExceptionStack
        {
            get
            {
                return exceptionStack;
            }

            set
            {
                exceptionStack = value;
            }
        }


        #endregion

        #region Constructors/Destructors/Finalisers
        public LoggingObject()
        {
            //
            // TODO: Add constructor logic here
            //

        }
        #endregion

        #region Public Methods and Properties
        #endregion
    }
}