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
    public class ResultStatus
    {
        #region Private Variables
        private int _status = -1;

        public int Status
        {
            get { return _status; }
            set { _status = value; }
        }

        private string _messageText;


        public string MessageText
        {
            get { return _messageText; }
            set { _messageText = value; }
        }

        #endregion

        #region Constructors/Destructors/Finalisers
        public ResultStatus()
        {
            //
            // TODO: Add constructor logic here
            //

        }
        #endregion

        #region Public Methods and Properties
        public bool IsSuccess
        {
            get
            {
                return Status == 0;
            }
        }


        public void SetSuccessStatus(string message)
        {
            Status = 0;
            _messageText = message;
        }

        public void SetSuccessStatus()
        {
            Status = 0;
        }

        public void SetErrorStatus(string message)
        {
            Status = -1;
            _messageText = message;
        }
        #endregion
    }
}