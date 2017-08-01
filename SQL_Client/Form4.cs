using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SQL_Client
{
    public partial class Form4 : Form
    {
        public bool[] isExist { get; set; }
        public string[] path{ get; set; }
        public SQL_Structure.StlMaterial[] material{ get; set; }
        
        public Form4()
        {
            InitializeComponent();
            isExist = new bool[5];
            path = new string[5];
            material = new SQL_Structure.StlMaterial[5];
        }
    }
}
