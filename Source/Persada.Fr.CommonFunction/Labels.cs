using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persada.Fr.CommonFunction
{
    public class Labels
    {
        public static string ButtonCancelForm()
        {
            return "Back";
        }

        public static string ButtonBrowseFile()
        {
            return "Browse";
        }

        public static string ButtonLabels(string mode)
        {
            string label = "Submit";
            switch (mode)
            {
                case "Edit":
                    label = "Edit";
                    break;
                case "Login":
                    label = "Login";
                    break;
                default:
                    label = "Submit";
                    break;
            }
            return label; 
        }

        public static string ButtonClass(string mode)
        {
            string label = "Submit";
            switch (mode)
            {
                case "Create":
                    label = "btn btn-sm btn-white btn-success btn-round";
                    break;
                case "Edit":
                    label = "btn btn-sm btn-white btn-default btn-round";
                    break;
                case "Delete":
                    label = "btn btn-sm btn-white btn-warning btn-round";
                    break;
                case "Exit":
                    label = "btn btn-sm btn-white btn-danger btn-round";
                    break;
                case "Close":
                    label = "btn btn-sm btn-white btn-danger btn-round";
                    break;
                case "Back":
                    label = "btn btn-sm btn-white btn-danger btn-round";
                    break;
                default:
                    label = "Submit";
                    break;
            }
            return label; 
        }

        public static string ButtonSaveForm(string mode)
        {
            string label = "Submit";
            switch (mode)
            {
                case "Edit":
                    label = "Edit";
                    break;
                case "Create":
                    label = "Create";
                    break;
                case "Delete":
                    label = "Delete";
                    break;
                case "Approve":
                    label = "Approve";
                    break;
                case "Reject":
                    label = "Reject";
                    break;
                case "Accept":
                    label = "Accept";
                    break;
                default:
                    label = "Submit";
                    break;
            }
            return label;
        }

        public static string IconWidget(string mode)
        {
            string label = "<i class='icon-plus-sign'></i>";
            switch (mode)
            {
                case "Edit":
                    label = "<i class='ace-icon fa fa-pencil icon-on-right bigger-110'></i>";
                    break;
                case "Create":
                    label = "<i class='ace-icon fa fa-arrow-right icon-on-right bigger-110'>";
                    break;
                case "Approval":
                    label = "<i class='ace-icon fa fa-arrow-right icon-on-right bigger-110'>";
                    break;
                case "List":
                    label = "<i class='ace-icon fa fa-list-alt icon-on-right bigger-110''></i>";
                    break;
                case "Details":
                    label = "<i class='ace-icon fa fa-search-plus icon-on-right bigger-110'></i>";
                    break;
                default:
                    label = "<i class='ace-icon fa fa-search-plus icon-on-right bigger-110'></i>";
                    break;
            }
            return label;
        }

        public static string IconAction(string mode)
        {
            string label = "<i class='icon-plus-sign'></i>";
            switch (mode)
            {
                case "Detail":
                    label = "<i class='glyphicon glyphicon-search'></i>";
                    break;
                case "Edit":
                    label = "<i class='glyphicon glyphicon-edit'></i>";
                    break;
                case "Delete":
                    label = "<i class='glyphicon glyphicon-trash'></i>";
                    break;
                default:
                    label = "<i class='ace-icon fa fa-search-plus bigger-130'></i>";
                    break;
            }
            return label;
        }

        public static string ButtonForm(string mode)
        {
            string label = "<i class='icon-plus-sign'></i>";
            switch (mode)
            {
                case "Edit":
                    label = "<button type='submit' class='btn btn-sm btn-success btn-white btn-round'><i class='ace-icon fa fa-floppy-o bigger-110 green'></i>Save</button>";
                    break;
                case "Delete":
                    label = "<button type='submit' class='btn btn-sm btn-success btn-white btn-round'><i class='ace-icon fa fa-floppy-o bigger-110 green'></i>Save</button>";
                    break;
                case "Create":
                    label = "<button type='submit' class='btn btn-sm btn-success btn-white btn-round'><i class='ace-icon fa fa-floppy-o bigger-110 green'></i>Save</button>";
                    break;
                case "Close":
                    label = "<button class='btn btn-sm btn-danger btn-white btn-round'><i class='ace-icon fa fa-times bigger-110 red2'></i> Close</button>";
                    break;
                case "Exit":
                    label = @"<button type='button' class='close' data-dismiss='modal' aria-hidden='True'>< span class='white'>&times;</span></button>";
                    break;
                default:
                    label = "<i class='ace-icon fa fa-search-plus bigger-130'></i>";
                    break;
            }
            return label;
        }

        public static string LongString(string value, int min)
        {
            string result = value.Length >= min ? value.Substring(0, (min - 3)) + "..." : value;
            return result;
        }
    }
}
