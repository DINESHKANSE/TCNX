using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using TCNX.commonFunction;


namespace TCNX.commonFunction
{
    public class TreeView
    {
        private DataTable dt;

        SqlDblayer dal = new SqlDblayer();
        string Message = string.Empty;
        private UserInfo userInfo =  Common.CurrentUserInfo;

        public string PopulateMenuDataTable(string mode="")
        {
            string DOM = ""; 
            dal.ClearParameters();
           dal.AddParameter("@MEMB_CODE",userInfo.Memb_code,"IN");
            dal.AddParameter("@USERNAME",userInfo.username,"IN");
            dal.AddParameter("@MODE", mode, "IN");
            DataTable dt = dal.GetTable("SP_LEVEL_TREEVIEW", ref Message);
          
            DOM = GetDOMTreeView(dt);
            return DOM;
        }

        public string GetDOMTreeView(DataTable dt)
        {
            string DOMTreeView = "";

            DOMTreeView += CreateTreeViewOuterParent(dt);
            DOMTreeView += CreateTreeViewMenu(dt, "0");

            DOMTreeView += "</ul>";

            return DOMTreeView;
        }

        public string CreateTreeViewOuterParent(DataTable dt)
        {
            string DOMDataList = "";

            DataRow[] drs = dt.Select("JCODE = 0");

            foreach (DataRow row in drs)
            {
                //row[2], 2 is column number start with 0, which is the USERNAME
                DOMDataList = "<ul><li >" + row[2].ToString() + "</li>";
            }

            return DOMDataList;
        }

        public string CreateTreeViewMenu(DataTable dt, string PLAC_CODE)
        {
            string DOMDataList = "";

            string JCODE = "";
            string USERNAME = "";

            DataRow[] drs = dt.Select("PLAC_CODE = " + PLAC_CODE);

            foreach (DataRow row in drs)
            {
                JCODE = row[0].ToString();
                USERNAME = row[2].ToString();
               

                DOMDataList += "<li class='new_list1'>";
                DOMDataList += "<a href='#' ><label onmouseover='MouseEvents(this, event, " + JCODE.Trim() + " )' onmouseout='MouseEvents(this, event," + JCODE.Trim() + " )'>  "
                                + USERNAME  + "</label></a>";
                DataRow[] drschild = dt.Select("PLAC_CODE = " + JCODE);
                if (drschild.Count() != 0)
                {
                    DOMDataList += "<ul >";
                    DOMDataList += CreateTreeViewMenu(dt, JCODE).Replace("<li class='new_list2'>", "<li>");
                    DOMDataList += "</ul></li>";
                }
                else
                {
                    DOMDataList += "</li>";
                }
            }
            return DOMDataList;
        }
    }
}