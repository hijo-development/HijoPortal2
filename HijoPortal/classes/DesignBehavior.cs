using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HijoPortal.classes
{
    public class DesignBehavior
    {
        public static void SetBehaviorGrid(ASPxGridView grid)
        {
            if (grid.IsEditing || grid.IsNewRowEditing)
            {
                grid.SettingsBehavior.AllowSort = false;
                grid.SettingsBehavior.AllowAutoFilter = false;
                grid.SettingsBehavior.AllowHeaderFilter = false;
            }
            else
            {
                grid.SettingsBehavior.AllowSort = true;
                grid.SettingsBehavior.AllowAutoFilter = true;
                grid.SettingsBehavior.AllowHeaderFilter = true;
            }
        }

        public static void VisibilityRevDesc(ASPxGridView grid, string entitycode)
        {
            if (entitycode == Constants.TRAIN_CODE())
                grid.Columns["RevDesc"].Visible = true;
            else
                grid.Columns["RevDesc"].Visible = false;


        }
    }
}