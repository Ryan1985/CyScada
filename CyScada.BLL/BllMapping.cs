using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using CyScada.Common;
using CyScada.DAL;
using CyScada.Model;

namespace CyScada.BLL
{
    public class BllMapping
    {
        private DalMapping _dalMapping = new DalMapping();

        public DalMapping DalMapping
        {
            get { return _dalMapping; }
            set { _dalMapping = value; }
        }


        public IEnumerable<MappingTableModel> GetMappingList(string mappingType)
        {
            var dtMapping = _dalMapping.GetMappingList(mappingType);
            return dtMapping.AsEnumerable().Select(dr => new MappingTableModel
            {
                Id = dr["Id"].ToString(),
                Value = dr["Value"].ToString(),
                Text = dr["Text"].ToString(),
                MappingType = dr["MappingType"].ToString()
            });
        }



        public DataTable GetMappingList(string mappingType, string paramString)
        {
            switch (mappingType.ToLower())
            {
                case "usertheme":
                {
                    return GetUserTheme(paramString);
                }
                default: return null;
            }
        }

        private DataTable GetUserTheme(string userId)
        {
            var dtMapping = _dalMapping.GetMappingList(CommonUtil.MappingType.UserTheme.ToString());
            var drUserTheme =
                dtMapping.AsEnumerable().Where(dr => dr["Value"].ToString() == userId).Select(dr => dr).ToList();
            return drUserTheme.CopyToDataTable();

        }

        public void Save(MappingTableModel model)
        {
            var dtMapping = _dalMapping.GetMappingList(model.MappingType);
            if (dtMapping.AsEnumerable().Any(dr => dr["Value"].ToString() == model.Value))
            {
                _dalMapping.Modify(model.ToHashTable());
            }
            else
            {
                _dalMapping.Create(model.ToHashTable());
            }


        }

    }
}
