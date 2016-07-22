using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using CyScada.DAL;

namespace CyScada.BLL
{
    public class BllMachineTagList
    {
        private DalMachineTag _dalMachineTag = new DalMachineTag();

        public DalMachineTag DalMachineTag
        {
            get { return _dalMachineTag; }
            set { _dalMachineTag = value; }
        }


        public DataTable GetAllTags()
        {
            return _dalMachineTag.GetTags();
        }



    }
}
